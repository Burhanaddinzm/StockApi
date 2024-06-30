using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestApi.Dtos.Comment;
using TestApi.Extensions.Claims;
using TestApi.Mappers;
using TestApi.Models;
using TestApi.Services.Interfaces;

namespace TestApi.Controllers;
/// <summary>
/// The controller for handling comments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IStockService _stockService;
    private readonly UserManager<AppUser> _userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentController"/> class.
    /// </summary>
    /// <param name="commentService">The comment service.</param>
    /// <param name="stockService">The stock service.</param>
    /// <param name="userManager">The user manager.</param>
    public CommentController(
        ICommentService commentService,
        IStockService stockService,
        UserManager<AppUser> userManager)
    {
        _commentService = commentService;
        _stockService = stockService;
        _userManager = userManager;
    }

    /// <summary>
    /// Gets all comments.
    /// </summary>
    /// <returns>The comments.</returns>
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

    /// <summary>
    /// Gets a comment by id.
    /// </summary>
    /// <param name="id">The comment id.</param>
    /// <returns>The comment.</returns>
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

    /// <summary>
    /// Creates a comment for a stock.
    /// </summary>
    /// <param name="stockId">The stock id.</param>
    /// <param name="commentDto">The comment dto.</param>
    /// <returns>The created comment.</returns>
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

        var username = User.GetUserName();
        if (username == null)
        {
            return NotFound("Username not found!");
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound("User not found!");
        }

        var comment = await _commentService.CreateCommentAsync(stockId.Value, user.Id, commentDto);
        return CreatedAtAction(nameof(Get), new { id = comment.Id }, comment.ToCommentDto());
    }

    /// <summary>
    /// Updates a comment.
    /// </summary>
    /// <param name="id">The comment id.</param>
    /// <param name="commentDto">The comment dto.</param>
    /// <returns>The updated comment.</returns>
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

    /// <summary>
    /// Deletes a comment.
    /// </summary>
    /// <param name="id">The comment id.</param>
    /// <returns>No content.</returns>
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
