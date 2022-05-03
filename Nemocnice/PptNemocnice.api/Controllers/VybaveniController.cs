using Microsoft.AspNetCore.Mvc;             //apicontroller a controllerbase
using PptNemocnice.Shared;                  //VybevniModel
using PptNemocnice.api.Data;                //NemocniceDbContext
using AutoMapper;                           //Mapper
using Microsoft.EntityFrameworkCore;        //.Include u _db.Vybavenis

namespace PptNemocnice.api.Controllers;

[ApiController]
[Route("[controller]")]         //prida misto controlleru "vybaveni"
public class VybaveniController : ControllerBase
{
    private readonly NemocniceDbContext _db;
    private readonly IMapper _mapper;

    public VybaveniController(NemocniceDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    [HttpGet]
    public List<VybaveniModel> GetAllVybaveni()
    {
        List<VybaveniModel> models = new();

        var ents = _db.Vybavenis.Include(x => x.Revizes);

        foreach (var ent in ents)
        {
            VybaveniModel vybaveni = _mapper.Map<VybaveniModel>(ent);
            vybaveni.LastRevisionDate = ent.Revizes.OrderByDescending(x => x.DateTime).FirstOrDefault()?.DateTime;
            models.Add(vybaveni);
        }
        return models;
    }

    [HttpPost]
    public Guid PostVybaveni(VybaveniModel prichoziModel)
    {
        prichoziModel.Id = Guid.Empty;                                //databaze si ID spravuje sama
        Vybaveni ent = _mapper.Map<Vybaveni>(prichoziModel);           //mapovani prichozihoModelu na Vybaveni
        _db.Vybavenis.Add(ent);
        _db.SaveChanges();

        return ent.Id;
    }
}
