using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLiteWebApi.Data;
using SQLiteWebApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SQLiteWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly SQLiteWebApiDbContext _context;

        public AppUserController(SQLiteWebApiDbContext context)
        {
            _context = context;
        }

        // GET: api/<AppUserController>
        [Route("api/AppUserCount")]
        [HttpGet]
        public async Task<int> GetCount()
        {
            var queryResult = await _context.Database.SqlQuery<int>($"select count(*) from AppUser").ToListAsync();

            return queryResult.FirstOrDefault();
        }

        // GET: api/<AppUserController>
        [HttpGet]
        public async Task<IEnumerable<AppUser>> Get()
        {
            return await _context.AppUser.Take(20).ToListAsync();
        }

        // GET api/<AppUserController>/5
        [HttpGet("{id}")]
        public async Task<AppUser> Get(string id)
        {
            return await _context.AppUser.FindAsync(id);
        }

        // POST api/<AppUserController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] AppUser appUser)
        {
            appUser.Id = Guid.NewGuid().ToString();
            appUser.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _context.AppUser.Add(appUser);
            await _context.SaveChangesAsync();

            return Results.Created("api/AppUser", appUser);
        }

        // PUT api/<AppUserController>/5
        /// <summary>
        /// Update a AppUser
        /// </summary>
        /// <returns>Updated AppUser</returns>
        /// <remarks>
        /// Example:
        ///
        ///     PATCH https://localhost:port/api/AppUser/5
        ///     [
        ///         {
        ///             "op": "replace",
        ///             "path": "/lasName",
        ///             "value": "The new Last Name"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        [HttpPatch("{id}")]
        public async Task<IResult> Patch(string id, JsonPatchDocument<AppUser> appUserNew)
        {
            AppUser appUserOnDB = await _context.AppUser.FindAsync(id);
            if (appUserOnDB != null)
            {
                appUserNew.ApplyTo(appUserOnDB);
                await _context.SaveChangesAsync();
                AppUser appUserResult = await _context.AppUser.FindAsync(id);
                return Results.Ok(appUserResult);
            }

            return Results.NotFound();
        }

        // DELETE api/<AppUserController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(string id)
        {
            AppUser appUser = await _context.AppUser.FindAsync(id);
            if (appUser != null)
            {
                _context.AppUser.Remove(appUser);
                await _context.SaveChangesAsync();
                return Results.Ok(appUser);
            }

            return Results.NotFound();
        }
    }
}
