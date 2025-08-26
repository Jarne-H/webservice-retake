using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PD4ExamAPI.Models;

public partial class Maze
{
    public int MazeId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public double Density { get; set; }

    [JsonIgnore]
    public int? OriginalMazeId { get; set; }

    public virtual ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();

    public virtual ICollection<MazeTile> MazeTiles { get; set; } = new List<MazeTile>();
}
