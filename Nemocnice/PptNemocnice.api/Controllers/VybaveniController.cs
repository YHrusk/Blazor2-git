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

    [HttpDelete]
    public IActionResult DeleteVybaveni(Guid Id)
    {
        var item = _db.Vybavenis.SingleOrDefault(x => x.Id == Id);
        if (item == null) return NotFound("Položka nenalezena");
        Vybaveni ent = _mapper.Map<Vybaveni>(item);
        _db.Vybavenis.Remove(ent);
        _db.SaveChanges();
        return Ok();
    }

    [HttpPut]
    public IActionResult PutVybaveni(VybaveniModel prichoziModel)
    {
        var staryZaznam = _db.Vybavenis.SingleOrDefault(x => x.Id == prichoziModel.Id);
        if (staryZaznam == null) return NotFound("Položka nenalezena");

        _mapper.Map(prichoziModel, staryZaznam);
        _db.SaveChanges();
        return Ok();
    }

    [HttpGet("{Id:guid}")]
    public IActionResult GetVybaveni(Guid Id)
    {
        var ents = _db.Vybavenis.Include(x => x.Revizes).Include(x => x.Ukons).SingleOrDefault(x => x.Id == Id);
        if (ents == null) return NotFound("nenalezeno");

        VybaveniSRevizesModel vybaveni = _mapper.Map<VybaveniSRevizesModel>(ents);
        return Ok(vybaveni);
    }
}
