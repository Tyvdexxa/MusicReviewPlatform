using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string Name { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string? Country { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
