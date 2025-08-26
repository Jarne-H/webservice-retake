using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PD4ExamAPI.Models;

public partial class MazeTile
{
    public int TileId { get; set; }

    public int RowIndex { get; set; }

    public int ColumnIndex { get; set; }

    public string TileType { get; set; } = null!;

    public int MazeId { get; set; }

    public double DensityFallOff { get; set; }

    [JsonIgnore]
    public virtual Maze Maze { get; set; } = null!;
}
