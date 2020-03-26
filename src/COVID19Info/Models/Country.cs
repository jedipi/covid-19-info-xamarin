using PropertyChanged;


namespace COVID19Info.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Country
    {
        public string Name { get; set; }
        public int TotalCases { get; set; }
        public int NewCases { get; set; }
        public int TotalDeaths { get; set; }
        public int NewDeaths { get; set; }
        public int TotalRecovered { get; set; }
        public int ActiveCases { get; set; }
        public int SeriousCritical { get; set; }
        public int TotalCasesPer1MPop { get; set; }
    }
}
