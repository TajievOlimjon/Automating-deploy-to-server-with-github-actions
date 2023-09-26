using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RoleController> _logger;
        public RoleController(ApplicationDbContext dbContext, ILogger<RoleController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        /*// GET: api/users
        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            return await _dbContext.Roles.ToListAsync();
        }*/

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(role).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
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

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _dbContext.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RoleExists(int id)
        {
            return _dbContext.Roles.Any(e => e.Id == id);
        }
    }
}
