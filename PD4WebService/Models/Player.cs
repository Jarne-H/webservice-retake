using System;
using System.Collections.Generic;

namespace PD4ExamAPI.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public virtual ICollection<GameSession> GameSessions { get; set; } = new List<GameSession>();
}
