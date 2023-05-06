using System;
using System.Collections.Generic;

namespace Server.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Iduser { get; set; } = null!;

    public string Adress { get; set; } = null!;
}
