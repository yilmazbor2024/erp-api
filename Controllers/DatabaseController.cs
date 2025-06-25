using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ErpMobile.Api.Data;
using ErpMobile.Api.Entities;
using ErpMobile.Api.Models.Database;
using System.Security.Claims;

namespace ErpMobile.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class DatabaseController : ControllerBase
    {
        private readonly NanoServiceDbContext _context;
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(NanoServiceDbContext context, ILogger<DatabaseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatabaseDto>>> GetDatabases()
        {
            var databases = await _context.Databases
                .Select(db => new DatabaseDto
                {
                    Id = db.Id,
                    DatabaseName = db.DatabaseName,
                    CompanyName = db.CompanyName,
                    CompanyAddress = db.CompanyAddress,
                    CompanyPhone = db.CompanyPhone,
                    CompanyEmail = db.CompanyEmail,
                    CompanyTaxNumber = db.CompanyTaxNumber,
                    CompanyTaxOffice = db.CompanyTaxOffice,
                    ServerName = db.ServerName ?? "",
                    ServerPort = db.ServerPort,
                    IsActive = db.IsActive,
                    CreatedAt = db.CreatedAt,
                    CreatedBy = db.CreatedBy,
                    ModifiedAt = db.ModifiedAt,
                    ModifiedBy = db.ModifiedBy
                })
                .ToListAsync();

            return databases;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DatabaseDto>> GetDatabase(Guid id)
        {
            var database = await _context.Databases.FindAsync(id);

            if (database == null)
            {
                return NotFound();
            }

            return new DatabaseDto
            {
                Id = database.Id,
                DatabaseName = database.DatabaseName,
                CompanyName = database.CompanyName,
                CompanyAddress = database.CompanyAddress,
                CompanyPhone = database.CompanyPhone,
                CompanyEmail = database.CompanyEmail,
                CompanyTaxNumber = database.CompanyTaxNumber,
                CompanyTaxOffice = database.CompanyTaxOffice,
                ServerName = database.ServerName ?? "",
                ServerPort = database.ServerPort,
                IsActive = database.IsActive,
                CreatedAt = database.CreatedAt,
                CreatedBy = database.CreatedBy,
                ModifiedAt = database.ModifiedAt,
                ModifiedBy = database.ModifiedBy
            };
        }

        [HttpPost]
        public async Task<ActionResult<DatabaseDto>> CreateDatabase(CreateDatabaseRequest request)
        {
            var username = User.Identity?.Name ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

            var database = new Database
            {
                Id = Guid.NewGuid(),
                DatabaseName = request.DatabaseName,
                CompanyName = request.CompanyName,
                CompanyAddress = request.CompanyAddress,
                CompanyPhone = request.CompanyPhone,
                CompanyEmail = request.CompanyEmail,
                CompanyTaxNumber = request.CompanyTaxNumber,
                CompanyTaxOffice = request.CompanyTaxOffice,
                ConnectionString = request.ConnectionString,
                ServerName = request.ServerName,
                ServerPort = request.ServerPort,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = username
            };

            _context.Databases.Add(database);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDatabase),
                new { id = database.Id },
                new DatabaseDto
                {
                    Id = database.Id,
                    DatabaseName = database.DatabaseName,
                    CompanyName = database.CompanyName,
                    CompanyAddress = database.CompanyAddress,
                    CompanyPhone = database.CompanyPhone,
                    CompanyEmail = database.CompanyEmail,
                    CompanyTaxNumber = database.CompanyTaxNumber,
                    CompanyTaxOffice = database.CompanyTaxOffice,
                    ServerName = database.ServerName ?? "",
                    ServerPort = database.ServerPort,
                    IsActive = database.IsActive,
                    CreatedAt = database.CreatedAt,
                    CreatedBy = database.CreatedBy,
                    ModifiedAt = database.ModifiedAt,
                    ModifiedBy = database.ModifiedBy
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDatabase(Guid id, UpdateDatabaseRequest request)
        {
            var database = await _context.Databases.FindAsync(id);
            if (database == null)
            {
                return NotFound();
            }

            var username = User.Identity?.Name ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";

            database.DatabaseName = request.DatabaseName;
            database.CompanyName = request.CompanyName;
            database.CompanyAddress = request.CompanyAddress;
            database.CompanyPhone = request.CompanyPhone;
            database.CompanyEmail = request.CompanyEmail;
            database.CompanyTaxNumber = request.CompanyTaxNumber;
            database.CompanyTaxOffice = request.CompanyTaxOffice;
            database.ConnectionString = request.ConnectionString;
            database.ServerName = request.ServerName;
            database.ServerPort = request.ServerPort;
            database.IsActive = request.IsActive;
            database.ModifiedAt = DateTime.UtcNow;
            database.ModifiedBy = username;

            _context.Entry(database).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatabaseExists(id))
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
        public async Task<IActionResult> DeleteDatabase(Guid id)
        {
            var database = await _context.Databases.FindAsync(id);
            if (database == null)
            {
                return NotFound();
            }

            _context.Databases.Remove(database);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DatabaseExists(Guid id)
        {
            return _context.Databases.Any(e => e.Id == id);
        }
    }
}
