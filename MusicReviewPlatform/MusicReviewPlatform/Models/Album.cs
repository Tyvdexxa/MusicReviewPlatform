using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public int ArtistId { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
}
