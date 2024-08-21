using System;
using System.Collections.Generic;

namespace NotesAppScaffolded.DataAccess.Domain;

public partial class Note
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Color { get; set; }

    public int? Tag { get; set; }

    public int Priority { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
