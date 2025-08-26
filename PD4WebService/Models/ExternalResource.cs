using System;
using System.Collections.Generic;

namespace PD4ExamAPI.Models;

public partial class ExternalResource
{
    public int ResourceId { get; set; }

    public string? ResourceType { get; set; }

    public string? ResourceUrl { get; set; }
}
