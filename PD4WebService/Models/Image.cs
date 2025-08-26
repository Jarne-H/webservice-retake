using System;
using System.Collections.Generic;

namespace PD4ExamAPI.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public string Name { get; set; } = null!;

    public string Link { get; set; } = null!;
}
