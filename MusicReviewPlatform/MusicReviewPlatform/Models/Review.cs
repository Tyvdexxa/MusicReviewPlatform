using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int AlbumId { get; set; }

    public int UserId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? ReviewDate { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;
}
