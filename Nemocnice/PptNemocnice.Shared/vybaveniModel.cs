using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PptNemocnice.Shared
{
    public class VybaveniModel
    {
        [Required, MinLength(5, ErrorMessage = "Délka u pole \"{0}\" musí být alespoň {1} znaků")]
        [Display(Name = "Název")]
        public string Name { get; set; }
        public DateTime BoughtDate { get; set; }
        public DateTime LastRevisionDate { get; set; }
        public bool NeedRevision => DateTime.Now - LastRevisionDate > TimeSpan.FromDays(365 * 2);
        public bool IsInEditMode { get; set; }

        [Range(1, 10000000, ErrorMessage = "Cena musí být mezi 1 a 10 000 000.")]
        //[Display(Name = "Cena")]
        public int Price { get; set; }

        public VybaveniModel(string nazev, DateTime zakoupeno, DateTime revize, bool uprava, int cena)
        {
            Name = nazev;
            BoughtDate = zakoupeno;
            LastRevisionDate = revize;
            IsInEditMode = uprava;
            Price = cena;
        }

        public VybaveniModel()              //prazdny konstruktor pro vytvoreni newEntity
        {
                
        }

    }
}