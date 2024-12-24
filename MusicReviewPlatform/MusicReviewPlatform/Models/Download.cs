using System;
using System.Collections.Generic;

namespace MusicReviewPlatform.Models;

public partial class Download
{
    public int DownloadId { get; set; }

    public int UserId { get; set; }

    public int SongId { get; set; }

    public DateTime? DownloadDate { get; set; }

    public virtual Song Song { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
