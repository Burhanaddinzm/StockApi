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
    private readonly IStockService _stockService;

    public CommentController(
        ICommentService commentService,
        IStockService stockService)
    {
        _commentService = commentService;
        _stockService = stockService;
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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int? id)
    {
        if (id == null || id <= 0)
        {
            return BadRequest("Invalid id!");
        }

        if (await _commentService.GetCommentAsync(id.Value) is not Comment comment)
        {
            return NotFound("Comment not found!");
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int? stockId, [FromBody] CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (stockId == null || stockId <= 0)
        {
            return BadRequest("Invalid stockId!");
        }

        if (!await _stockService.StockExistsAsync(stockId.Value))
        {
            return NotFound("Stock not found!");
        }

        var comment = await _commentService.CreateCommentAsync(stockId.Value, commentDto);
        return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment.ToCommentDto());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int? id, [FromBody] UpdateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (id == null || id <= 0)
        {
            return BadRequest("Invalid id!");
        }

        var comment = await _commentService.UpdateCommentAsync(id.Value, commentDto);

        if (comment == null)
        {
            return NotFound("Comment not found!");
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int? id)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        if (id == null || id <= 0)
        {
            return BadRequest("Invalid id!");
        }

        await _commentService.DeleteCommentAsync(id.Value);
        return NoContent();
    }
}
