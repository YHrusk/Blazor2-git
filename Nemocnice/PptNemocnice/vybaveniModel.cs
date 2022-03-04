namespace PptNemocnice
{
    public class vybaveniModel
    {
        public string Name { get; set; }
        public DateTime BoughtDate { get; set; }
        public DateTime LastRevisionDate { get; set; }
        public bool NeedRevision { get;}

        public vybaveniModel(string nazev, DateTime zakoupeno, DateTime revize, bool potrebaRevize)
        {
            Name = nazev;
            BoughtDate = zakoupeno;
            LastRevisionDate = revize;
            NeedRevision = potrebaRevize;
        }
    }
}
