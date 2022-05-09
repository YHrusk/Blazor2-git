using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PptNemocnice.Shared;

public class UkonModel
{
    public string Name { get; set; } = "";
    public Guid Id { get; set; }
    public Guid VybaveniId { get; set; }
    public string Info { get; set; } = "";
    public DateTime? DateTime { get; set; }
    public string Kod { get; set; } = "";
}
