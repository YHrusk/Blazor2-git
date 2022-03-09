using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PptNemocnice.Shared
{
    public class vybaveniModel
    {
        public string Name { get; set; }
        public DateTime BoughtDate { get; set; }
        public DateTime LastRevisionDate { get; set; }
        public bool NeedRevision => DateTime.Now - LastRevisionDate > TimeSpan.FromDays(365 * 2);

        public bool IsInEditMode { get; set; }

        public vybaveniModel(string nazev, DateTime zakoupeno, DateTime revize, bool uprava)
        {
            Name = nazev;
            BoughtDate = zakoupeno;
            LastRevisionDate = revize;
            IsInEditMode = uprava;
        }
    }
}