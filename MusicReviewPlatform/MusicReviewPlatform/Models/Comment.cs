using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual Review Review { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
