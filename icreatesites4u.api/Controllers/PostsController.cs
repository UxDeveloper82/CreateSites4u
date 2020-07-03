using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using icreatesites4u.api.Data;
using icreatesites4u.api.Dtos;
using icreatesites4u.api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace icreatesites4u.api.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDevRepository _repo;
        private readonly IMapper _mapper;

        public PostsController(IDevRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> GetPost(int userId, int id)
        {
         
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var postFromRepo = await _repo.GetPost(id);
            if (postFromRepo == null)
                return NotFound();
            return Ok(postFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(int userId, 
                PostForCreationDto postForCreationDto)
        {
            var creator = await _repo.GetUser(userId);

            if (creator.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            postForCreationDto.CreatorId = userId;

            var postCreater = await _repo.GetUser(postForCreationDto.Id);

            if (postCreater == null)
                return BadRequest("Could not find a user");

            var newPost = _mapper.Map<Post>(postForCreationDto);

            _repo.Add(newPost);

            if (await _repo.SaveAll())
                return CreatedAtAction("GetPost", new { id = newPost.Id }, newPost);

            throw new Exception("Creating the message failed on save");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();


            var postFromRepo = await _repo.GetPost(id);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed to save");
        }


    }
}