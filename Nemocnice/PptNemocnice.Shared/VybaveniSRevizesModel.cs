using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PptNemocnice.Shared;

public class VybaveniSRevizesModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public int Price { get; set; }
    public DateTime BoughtDate { get; set; }

    public List<RevizeModel> Revizes { get; set; } = new();
}
