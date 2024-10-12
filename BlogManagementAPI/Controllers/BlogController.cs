using BlogManagementAPI.Model;
using BlogManagementAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
namespace BlogManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public ActionResult<List<BlogPost>> GetAll()
        {
            return _blogService.GetAllPosts();
        }

        [HttpGet("{id}")]
        public ActionResult<BlogPost> Get(int id)
        {
            var post = _blogService.GetPost(id);
            if (post == null) return NotFound();
            return post;
        }

        [HttpPost]
        public IActionResult Create([FromBody] BlogPost post)
        {
            _blogService.AddPost(post);
            return CreatedAtAction(nameof(Get), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BlogPost post)
        {
            if (id != post.Id) return BadRequest();
            _blogService.UpdatePost(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _blogService.DeletePost(id);
            return NoContent();
        }
    }
}
