﻿namespace TestApi.Dtos.Comment;

public class CreateCommentDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
}
