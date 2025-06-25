using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.Database;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserDatabaseController : ControllerBase
    {
        private readonly NanoServiceDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserDatabaseController> _logger;

        public UserDatabaseController(
            NanoServiceDbContext context, 
            UserManager<User> userManager,
            ILogger<UserDatabaseController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDatabaseDto>>> GetUserDatabases()
        {
            var userDatabases = await _context.UserDatabases
                .Include(ud => ud.User)
                .Include(ud => ud.Database)
                .Select(ud => new UserDatabaseDto
                {
                    Id = ud.Id,
                    UserId = ud.UserId,
                    UserName = ud.User.UserName ?? ud.User.Email ?? "",
                    DatabaseId = ud.DatabaseId,
                    DatabaseName = ud.Database.DatabaseName,
                    CompanyName = ud.Database.CompanyName,
                    Role = ud.Role,
                    IsActive = ud.IsActive,
                    CreatedAt = ud.CreatedAt,
                    CreatedBy = ud.CreatedBy,
                    ModifiedAt = ud.ModifiedAt,
                    ModifiedBy = ud.ModifiedBy
                })
                .ToListAsync();

            return userDatabases;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserDatabaseDto>>> GetUserDatabasesByUser(string userId)
        {
            var userDatabases = await _context.UserDatabases
                .Include(ud => ud.User)
                .Include(ud => ud.Database)
                .Where(ud => ud.UserId == userId)
                .Select(ud => new UserDatabaseDto
                {
                    Id = ud.Id,
                    UserId = ud.UserId,
                    UserName = ud.User.UserName ?? ud.User.Email ?? "",
                    DatabaseId = ud.DatabaseId,
                    DatabaseName = ud.Database.DatabaseName,
                    CompanyName = ud.Database.CompanyName,
                    Role = ud.Role,
                    IsActive = ud.IsActive,
                    CreatedAt = ud.CreatedAt,
                    CreatedBy = ud.CreatedBy,
                    ModifiedAt = ud.ModifiedAt,
                    ModifiedBy = ud.ModifiedBy
                })
                .ToListAsync();

            return userDatabases;
        }

        [HttpGet("database/{databaseId}")]
        public async Task<ActionResult<IEnumerable<UserDatabaseDto>>> GetUserDatabasesByDatabase(Guid databaseId)
        {
            var userDatabases = await _context.UserDatabases
                .Include(ud => ud.User)
                .Include(ud => ud.Database)
                .Where(ud => ud.DatabaseId == databaseId)
                .Select(ud => new UserDatabaseDto
                {
                    Id = ud.Id,
                    UserId = ud.UserId,
                    UserName = ud.User.UserName ?? ud.User.Email ?? "",
                    DatabaseId = ud.DatabaseId,
                    DatabaseName = ud.Database.DatabaseName,
                    CompanyName = ud.Database.CompanyName,
                    Role = ud.Role,
                    IsActive = ud.IsActive,
                    CreatedAt = ud.CreatedAt,
                    CreatedBy = ud.CreatedBy,
                    ModifiedAt = ud.ModifiedAt,
                    ModifiedBy = ud.ModifiedBy
                })
                .ToListAsync();

            return userDatabases;
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<IEnumerable<UserDatabaseDto>>> GetCurrentUserDatabases()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var userDatabases = await _context.UserDatabases
                .Include(ud => ud.User)
                .Include(ud => ud.Database)
                .Where(ud => ud.UserId == userId && ud.IsActive && ud.Database.IsActive)
                .Select(ud => new UserDatabaseDto
                {
                    Id = ud.Id,
                    UserId = ud.UserId,
                    UserName = ud.User.UserName ?? ud.User.Email ?? "",
                    DatabaseId = ud.DatabaseId,
                    DatabaseName = ud.Database.DatabaseName,
                    CompanyName = ud.Database.CompanyName,
                    Role = ud.Role,
                    IsActive = ud.IsActive,
                    CreatedAt = ud.CreatedAt,
                    CreatedBy = ud.CreatedBy,
                    ModifiedAt = ud.ModifiedAt,
                    ModifiedBy = ud.ModifiedBy
                })
                .ToListAsync();

            return userDatabases;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDatabaseDto>> GetUserDatabase(Guid id)
        {
            var userDatabase = await _context.UserDatabases
                .Include(ud => ud.User)
                .Include(ud => ud.Database)
                .FirstOrDefaultAsync(ud => ud.Id == id);

            if (userDatabase == null)
            {
                return NotFound();
            }

            return new UserDatabaseDto
            {
                Id = userDatabase.Id,
                UserId = userDatabase.UserId,
                UserName = userDatabase.User.UserName ?? userDatabase.User.Email ?? "",
                DatabaseId = userDatabase.DatabaseId,
                DatabaseName = userDatabase.Database.DatabaseName,
                CompanyName = userDatabase.Database.CompanyName,
                Role = userDatabase.Role,
                IsActive = userDatabase.IsActive,
                CreatedAt = userDatabase.CreatedAt,
                CreatedBy = userDatabase.CreatedBy,
                ModifiedAt = userDatabase.ModifiedAt,
                ModifiedBy = userDatabase.ModifiedBy
            };
        }

        [HttpPost]
        public async Task<ActionResult<UserDatabaseDto>> CreateUserDatabase(CreateUserDatabaseRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                return BadRequest("Kullanıcı bulunamadı.");
            }

            var database = await _context.Databases.FindAsync(request.DatabaseId);
            if (database == null)
            {
                return BadRequest("Veritabanı bulunamadı.");
            }

            // Aynı kullanıcı ve veritabanı için kayıt var mı kontrol et
            var existingUserDatabase = await _context.UserDatabases
                .FirstOrDefaultAsync(ud => ud.UserId == request.UserId && ud.DatabaseId == request.DatabaseId);
            
            if (existingUserDatabase != null)
            {
                return BadRequest("Bu kullanıcı için bu veritabanı zaten tanımlanmış.");
            }

            var username = User.Identity?.Name ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

            var userDatabase = new UserDatabase
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                DatabaseId = request.DatabaseId,
                Role = request.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = username
            };

            _context.UserDatabases.Add(userDatabase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUserDatabase),
                new { id = userDatabase.Id },
                new UserDatabaseDto
                {
                    Id = userDatabase.Id,
                    UserId = userDatabase.UserId,
                    UserName = user.UserName ?? user.Email ?? "",
                    DatabaseId = userDatabase.DatabaseId,
                    DatabaseName = database.DatabaseName,
                    CompanyName = database.CompanyName,
                    Role = userDatabase.Role,
                    IsActive = userDatabase.IsActive,
                    CreatedAt = userDatabase.CreatedAt,
                    CreatedBy = userDatabase.CreatedBy,
                    ModifiedAt = userDatabase.ModifiedAt,
                    ModifiedBy = userDatabase.ModifiedBy
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserDatabase(Guid id, UpdateUserDatabaseRequest request)
        {
            var userDatabase = await _context.UserDatabases.FindAsync(id);
            if (userDatabase == null)
            {
                return NotFound();
            }

            var username = User.Identity?.Name ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

            userDatabase.Role = request.Role;
            userDatabase.IsActive = request.IsActive;
            userDatabase.ModifiedAt = DateTime.UtcNow;
            userDatabase.ModifiedBy = username;

            _context.Entry(userDatabase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDatabaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDatabase(Guid id)
        {
            var userDatabase = await _context.UserDatabases.FindAsync(id);
            if (userDatabase == null)
            {
                return NotFound();
            }

            _context.UserDatabases.Remove(userDatabase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDatabaseExists(Guid id)
        {
            return _context.UserDatabases.Any(e => e.Id == id);
        }
    }
}
