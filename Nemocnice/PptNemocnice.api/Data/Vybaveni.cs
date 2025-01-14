﻿namespace PptNemocnice.api.Data;

public class Vybaveni
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime BoughtDate { get; set; }
    //public DateTime LastRevisionDate { get; set; }
    public int Price { get; set; }

    public List<Revize> Revizes { get; set; } = new();
    public List<Ukon> Ukons { get; set; } = new();
}
