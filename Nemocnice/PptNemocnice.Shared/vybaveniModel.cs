using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PptNemocnice.Shared
{
    public class VybaveniModel
    {
        public Guid Id { get; set; }

        [Required, MinLength(5, ErrorMessage = "Délka u pole \"{0}\" musí být alespoň {1} znaků")]
        [Display(Name = "Název")]
        public string Name { get; set; }
        public DateTime BoughtDate { get; set; }
        public DateTime LastRevisionDate { get; set; }

        [JsonIgnore]
        public bool NeedRevision => DateTime.Now - LastRevisionDate > TimeSpan.FromDays(365 * 2);
        public bool IsInEditMode { get; set; }

        [Display(Name = "Cena")]
        [Range(1, 10_000_000, ErrorMessage = "{0} musí být v rozsahu {1} až {2} ")]
        public int Price { get; set; }

        public VybaveniModel(Guid id, string nazev, DateTime zakoupeno, DateTime revize, bool uprava, int cena)
        {
            Id = id;
            Name = nazev;
            BoughtDate = zakoupeno;
            LastRevisionDate = revize;
            IsInEditMode = uprava;
            Price = cena;
        }

        public VybaveniModel()              //prazdny konstruktor pro vytvoreni newEntity
        {

        }

        public static List<VybaveniModel> Generovat()
        {
            List<VybaveniModel> seznam = new List<VybaveniModel>();

            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();

                //id
                Guid id = Guid.NewGuid();

                //nazev
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                string nazev = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

                //datum nakupu

                DateTime start_nakup = new DateTime(2005, 1, 1);
                int range_nakup = (DateTime.Today - start_nakup).Days;
                DateTime nakup = start_nakup.AddDays(random.Next(range_nakup));

                //datum revize
                DateTime start_revize = new DateTime(2018, 1, 1);
                int range_revize = (DateTime.Today - start_revize).Days;
                DateTime revize = start_revize.AddDays(random.Next(range_revize));

                int cena = random.Next(500, 1000000);

                VybaveniModel vyb = new VybaveniModel(id, nazev, nakup, revize, false, cena);
                seznam.Add(vyb);
            }

            return seznam;

        }

        public VybaveniModel Copy()
        {
            VybaveniModel to = new();
            to.BoughtDate = BoughtDate;
            to.LastRevisionDate = LastRevisionDate;
            to.IsInEditMode = IsInEditMode;
            to.Name = Name;
            to.Price = Price;
            return to;
        }

        public void MapTo(VybaveniModel? to)        //potvrzeni editu --> přemapování
        {
            if (to == null) return;
            to.BoughtDate = BoughtDate;
            to.LastRevisionDate = LastRevisionDate;
            to.Name = Name;
            to.Price = Price;
        }

    }
}