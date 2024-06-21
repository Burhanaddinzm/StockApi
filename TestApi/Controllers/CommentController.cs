using Microsoft.AspNetCore.Mvc;
using TestApi.Dtos.Comment;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var comments = await _commentService.GetAllCommentsAsync();
        if (comments.Count == 0)
        {
            return NotFound("Comments not found!");
        }
        return Ok(comments.Select(x => x.ToCommentDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int? id)
    {
        if (id == null || id == 0)
        {
            return BadRequest("Invalid id!");
        }

        if (await _commentService.GetCommentAsync(id.Value) is not Comment comment)
        {
            return NotFound("Comment not found!");
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto commentDto)
    {
        var comment = await _commentService.CreateCommentAsync(commentDto);
        return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment.ToCommentDto());
    }
}
