using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ErpMobile.Api.Models.Auth;
using ErpMobile.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ErpMobile.Api.Entities;

namespace ErpMobile.Api.Services
{
    public interface IUserGroupService
    {
        Task<IEnumerable<Models.Auth.UserGroup>> GetAllGroupsAsync();
        Task<Models.Auth.UserGroup> GetGroupByIdAsync(int id);
        Task<int> CreateGroupAsync(Models.Auth.UserGroup group, string createdBy);
        Task<bool> UpdateGroupAsync(Models.Auth.UserGroup group, string modifiedBy);
        Task<bool> DeleteGroupAsync(int id);
        Task<bool> AddUserToGroupAsync(int groupId, string userId, string createdBy);
        Task<bool> RemoveUserFromGroupAsync(int groupId, string userId);
        Task<IEnumerable<Models.Auth.UserGroupMember>> GetGroupMembersAsync(int groupId);
        Task<IEnumerable<Models.Auth.UserGroup>> GetUserGroupsAsync(string userId);
    }

    public class UserGroupService : IUserGroupService
    {
        private readonly NanoServiceDbContext _context;

        public UserGroupService(NanoServiceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Auth.UserGroup>> GetAllGroupsAsync()
        {
            var groups = await _context.UserGroups.ToListAsync();
            return groups.Select(g => new Models.Auth.UserGroup
            {
                Id = g.Id.GetHashCode(), // Guid'i int'e dönüştür
                GroupName = g.Name,
                Description = g.Description,
                IsActive = g.IsActive,
                CreatedDate = g.CreatedAt,
                CreatedBy = g.CreatedBy,
                ModifiedDate = g.ModifiedAt,
                ModifiedBy = g.ModifiedBy
            }).ToList();
        }

        public async Task<Models.Auth.UserGroup> GetGroupByIdAsync(int id)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id.GetHashCode() == id);
            if (group == null)
                return null;

            return new Models.Auth.UserGroup
            {
                Id = id,
                GroupName = group.Name,
                Description = group.Description,
                IsActive = group.IsActive,
                CreatedDate = group.CreatedAt,
                CreatedBy = group.CreatedBy,
                ModifiedDate = group.ModifiedAt,
                ModifiedBy = group.ModifiedBy
            };
        }

        public async Task<int> CreateGroupAsync(Models.Auth.UserGroup groupModel, string createdBy)
        {
            var group = new Entities.UserGroup
            {
                Id = Guid.NewGuid(),
                Name = groupModel.GroupName,
                Description = groupModel.Description,
                IsActive = groupModel.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = createdBy
            };

            _context.UserGroups.Add(group);
            await _context.SaveChangesAsync();

            return group.Id.GetHashCode();
        }

        public async Task<bool> UpdateGroupAsync(Models.Auth.UserGroup groupModel, string modifiedBy)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id.GetHashCode() == groupModel.Id);
            if (group == null)
                return false;

            group.Name = groupModel.GroupName;
            group.Description = groupModel.Description;
            group.IsActive = groupModel.IsActive;
            group.ModifiedAt = DateTime.Now;
            group.ModifiedBy = modifiedBy;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteGroupAsync(int id)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id.GetHashCode() == id);
            if (group == null)
                return false;

            _context.UserGroups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddUserToGroupAsync(int groupId, string userId, string createdBy)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id.GetHashCode() == groupId);
            if (group == null)
                return false;

            var existingMembership = await _context.Set<Models.Auth.UserGroupMember>()
                .FirstOrDefaultAsync(m => m.UserGroupId == groupId && m.UserId == userId);

            if (existingMembership != null)
            {
                if (!existingMembership.IsActive)
                {
                    existingMembership.IsActive = true;
                    existingMembership.ModifiedDate = DateTime.Now;
                    existingMembership.ModifiedBy = createdBy;
                    await _context.SaveChangesAsync();
                }
                return true;
            }

            var membership = new Models.Auth.UserGroupMember
            {
                UserGroupId = groupId,
                UserId = userId,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = createdBy
            };

            _context.Set<Models.Auth.UserGroupMember>().Add(membership);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromGroupAsync(int groupId, string userId)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id.GetHashCode() == groupId);
            if (group == null)
                return false;
                
            var membership = await _context.Set<Models.Auth.UserGroupMember>()
                .FirstOrDefaultAsync(m => m.UserGroupId == groupId && m.UserId == userId && m.IsActive);

            if (membership == null)
                return false;

            membership.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Models.Auth.UserGroupMember>> GetGroupMembersAsync(int groupId)
        {
            var group = await _context.UserGroups.FirstOrDefaultAsync(g => g.Id.GetHashCode() == groupId);
            if (group == null)
                return new List<Models.Auth.UserGroupMember>();
                
            var members = await _context.Set<Models.Auth.UserGroupMember>()
                .Where(m => m.UserGroupId == groupId && m.IsActive)
                .ToListAsync();
                
            return members;
        }

        public async Task<IEnumerable<Models.Auth.UserGroup>> GetUserGroupsAsync(string userId)
        {
            var userGroupMembers = await _context.Set<Models.Auth.UserGroupMember>()
                .Where(m => m.UserId == userId && m.IsActive)
                .ToListAsync();
                
            var groupIds = userGroupMembers.Select(m => m.UserGroupId).ToList();
            
            var groups = new List<Models.Auth.UserGroup>();
            foreach (var groupId in groupIds)
            {
                var group = await GetGroupByIdAsync(groupId);
                if (group != null)
                {
                    groups.Add(group);
                }
            }
            
            return groups;
        }
    }
}
