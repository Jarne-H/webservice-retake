using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PD4ExamAPI.Models;

public partial class GameSession
{
    public int GameSessionId { get; set; }

    public int MazeId { get; set; }

    public int PlayerId { get; set; }

    public DateTime StartTime { get; set; }

    [JsonIgnore]
    public virtual Maze Maze { get; set; } = null!;

    [JsonIgnore]
    public virtual Player Player { get; set; } = null!;
}
