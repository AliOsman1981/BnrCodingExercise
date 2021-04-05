using BnrCodingExercise.Core.Entities;
using BnrCodingExercise.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BnrCodingExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly AppDataContext _appDataContext;

        public PostsController(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext ?? throw new ArgumentNullException(nameof(appDataContext));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _appDataContext.Posts.ToListAsync();

                return Ok(result);
            }
            catch (Exception)
            {
                // log errors
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == default)
                return BadRequest($"{id} is invalid");

            try
            {
                var result = await _appDataContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                {
                    NotFound($"{id} was not found.");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                // log exception
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUser(int id)
        {
            if (id == default)
                return BadRequest($"User {id} is invalid");

            try
            {
                var result = await _appDataContext.Posts.Where(x => x.UserId == id).ToListAsync();

                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Post post)
        {
            if (post == null)
                return BadRequest("Post can not be null.");

            try
            {
                _appDataContext.Posts.Add(post);
                var result = await _appDataContext.SaveChangesAsync();

                if (result == 1) // success
                {
                    return new ObjectResult(post)
                    {
                        StatusCode = StatusCodes.Status201Created
                    };
                }
            }
            catch (Exception)
            {
                // log exception
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Post post)
        {
            if (post == null)
                return BadRequest("Post can not be null.");

            try
            {
                _appDataContext.Posts.Update(post);
                var result = await _appDataContext.SaveChangesAsync();

                if (result == 1)
                    return Ok(post);

            }
            catch (Exception)
            {
                // log error                
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == default)
                return BadRequest($"{id} is invalid");

            try
            {
                var entity = new Post { Id = id };
                _appDataContext.Attach(entity);
                _appDataContext.Remove(entity);
                var result = await _appDataContext.SaveChangesAsync();

                if (result == 1)
                    return Ok($"{id} deleted successfully");
            }
            catch (Exception)
            {
                // log error
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
