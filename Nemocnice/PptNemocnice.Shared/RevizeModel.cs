using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PptNemocnice.Shared;

public class RevizeModel
{
    public string Name { get; set; } = "";
    public Guid Id { get; set; }
    public Guid VybaveniId { get; set; }
    public DateTime? DateTime { get; set; }

    //public static List<RevizeModel> Vygenerovat()               //jiz neni potreba? zakomentovaat ci vymazat?
    //{
    //    List<RevizeModel> list = new List<RevizeModel>();
    //    for (int i = 0; i < 100; i++)
    //    {
    //        Random random = new Random();
    //        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    //        string nazev = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

    //        var rev = new RevizeModel { Id = Guid.NewGuid(), Name = nazev};
    //        list.Add(rev);
    //    }
    //    return list;
    //}

}

public record RevizeCreatedResponseModel(Guid Id, DateTime DateTime);
//toto je dataová struktura na vrácení informace o vytvoření revizi
//chceme vrátit 2 věci, proto je nutné si na to vytvořit "record"
//"record" je něco jako třída (tady by se to dalo jednoduše nahradit), 
//ale má spoustu výhod pro "držení" dat. jednou z nich je například tento zkrácený zápis
//který bý normálně odpovídal třídě na 10 řádků:
public class RevizeCreatedResponseModelClass
{
    public RevizeCreatedResponseModelClass(Guid Id, DateTime DateTime)
    {
        this.Id = Id;
        this.DateTime = DateTime;
    }
    public Guid Id { get; }//není setter, nemůžeme změnit hodnotu (což record dělá sám o sobě)
    //má to výhodu tu, že si můžeme být jisti, že nám data doputují tam kam mají, nikde v kódu se nezmění
    //to co se tam přiřadí, už tam zůstane. record je "immutable"  
    public DateTime DateTime { get; }

}
