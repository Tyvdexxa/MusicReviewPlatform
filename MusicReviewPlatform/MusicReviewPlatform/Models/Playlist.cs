using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime? CreationDate { get; set; }

    public virtual ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();

    public virtual User User { get; set; } = null!;
}
