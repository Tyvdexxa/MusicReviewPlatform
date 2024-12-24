using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class PlaylistSong
{
    public int PlaylistSongId { get; set; }

    public int PlaylistId { get; set; }

    public int SongId { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Song Song { get; set; } = null!;
}
