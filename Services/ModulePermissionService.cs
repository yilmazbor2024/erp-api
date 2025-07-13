using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Auth;
using ErpMobile.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ErpMobile.Api.Services
{
    public interface IModulePermissionService
    {
        Task<IEnumerable<ModulePermission>> GetAllPermissionsAsync();
        Task<IEnumerable<ModulePermission>> GetGroupPermissionsAsync(int groupId);
        Task<ModulePermission> GetPermissionByIdAsync(int id);
        Task<ModulePermission> GetPermissionByGroupAndModuleAsync(int groupId, string moduleName);
        Task<int> CreatePermissionAsync(ModulePermission permission, string createdBy);
        Task<bool> UpdatePermissionAsync(ModulePermission permission, string modifiedBy);
        Task<bool> DeletePermissionAsync(int id);
        Task<bool> HasPermissionAsync(string userId, string moduleName, string permissionType);
    }

    public class ModulePermissionService : IModulePermissionService
    {
        private readonly NanoServiceDbContext _context;

        public ModulePermissionService(NanoServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ModulePermission>> GetAllPermissionsAsync()
        {
            return await _context.Set<ModulePermission>()
                .Include(p => p.UserGroup)
                .ToListAsync();
        }

        public async Task<IEnumerable<ModulePermission>> GetGroupPermissionsAsync(int groupId)
        {
            return await _context.Set<ModulePermission>()
                .Where(p => p.UserGroupId == groupId)
                .ToListAsync();
        }

        public async Task<ModulePermission> GetPermissionByIdAsync(int id)
        {
            return await _context.Set<ModulePermission>()
                .Include(p => p.UserGroup)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ModulePermission> GetPermissionByGroupAndModuleAsync(int groupId, string moduleName)
        {
            return await _context.Set<ModulePermission>()
                .FirstOrDefaultAsync(p => p.UserGroupId == groupId && p.ModuleName == moduleName);
        }

        public async Task<int> CreatePermissionAsync(ModulePermission permission, string createdBy)
        {
            // Aynı grup ve modül için izin var mı kontrol et
            var existingPermission = await GetPermissionByGroupAndModuleAsync(
                permission.UserGroupId, permission.ModuleName);

            if (existingPermission != null)
            {
                // Varsa güncelle
                existingPermission.CanView = permission.CanView;
                existingPermission.CanCreate = permission.CanCreate;
                existingPermission.CanEdit = permission.CanEdit;
                existingPermission.CanDelete = permission.CanDelete;
                existingPermission.IsActive = permission.IsActive;
                existingPermission.ModifiedDate = DateTime.Now;
                existingPermission.ModifiedBy = createdBy;

                await _context.SaveChangesAsync();
                return existingPermission.Id;
            }

            // Yoksa yeni oluştur
            permission.CreatedDate = DateTime.Now;
            permission.CreatedBy = createdBy;
            permission.IsActive = true;

            _context.Set<ModulePermission>().Add(permission);
            await _context.SaveChangesAsync();
            return permission.Id;
        }

        public async Task<bool> UpdatePermissionAsync(ModulePermission permission, string modifiedBy)
        {
            var existingPermission = await _context.Set<ModulePermission>().FindAsync(permission.Id);
            if (existingPermission == null)
                return false;

            existingPermission.CanView = permission.CanView;
            existingPermission.CanCreate = permission.CanCreate;
            existingPermission.CanEdit = permission.CanEdit;
            existingPermission.CanDelete = permission.CanDelete;
            existingPermission.IsActive = permission.IsActive;
            existingPermission.ModifiedDate = DateTime.Now;
            existingPermission.ModifiedBy = modifiedBy;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePermissionAsync(int id)
        {
            var permission = await _context.Set<ModulePermission>().FindAsync(id);
            if (permission == null)
                return false;

            _context.Set<ModulePermission>().Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> HasPermissionAsync(string userId, string moduleName, string permissionType)
        {
            // Kullanıcının gruplarını bul
            var userGroups = await _context.Set<UserGroupMember>()
                .Where(m => m.UserId == userId && m.IsActive)
                .Select(m => m.UserGroupId)
                .ToListAsync();

            if (userGroups.Count == 0)
                return false;

            // Grupların modül izinlerini kontrol et
            var permission = await _context.Set<ModulePermission>()
                .Where(p => userGroups.Contains(p.UserGroupId) && 
                       p.ModuleName == moduleName && 
                       p.IsActive)
                .FirstOrDefaultAsync();

            if (permission == null)
                return false;

            // İzin tipine göre kontrol et
            switch (permissionType.ToLower())
            {
                case "view":
                    return permission.CanView;
                case "create":
                    return permission.CanCreate;
                case "edit":
                    return permission.CanEdit;
                case "delete":
                    return permission.CanDelete;
                default:
                    return false;
            }
        }
    }
}
