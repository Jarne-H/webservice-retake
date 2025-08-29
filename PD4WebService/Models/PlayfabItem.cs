using System;
using System.Collections.Generic;

namespace PD4ExamAPI.Models;

public partial class PlayfabItem
{
    public int PlayfabItemId { get; set; }

    public string playfabid { get; set; }

    public string displayname { get; set; } = null!;
}


