using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Song
{
    public int SongId { get; set; }

    public string Title { get; set; } = null!;

    public TimeOnly Duration { get; set; }

    public int AlbumId { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual ICollection<Download> Downloads { get; set; } = new List<Download>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();
}
