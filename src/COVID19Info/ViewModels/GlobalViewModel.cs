using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using COVID19Info.Services;
using Microcharts;
using SkiaSharp;

namespace COVID19Info.ViewModels
{
    
    public class GlobalViewModel : BaseViewModel
    {
        #region Properties
        public IHtmlData HtmlContent => DependencyService.Get<IHtmlData>();
        public int Cases { get; set; }
        public int Deaths { get; set; }
        public int Recovered { get; set; }
        public string LastUpdate { get; set; }
        public BarChart Chart { get; set; }
        public bool IsRefreshing { get; set; }
        public Command RefreshViewCommand { get; set; }
        #endregion

        public GlobalViewModel()
        {
            Title = "Global COVID-19 Info";
            RefreshViewCommand = new Command(async()=> await RefreshData());
        }

        /// <summary>
        /// generate a chart 
        /// </summary>
        private void GenerateChart()
        {
            var entries = new List<Microcharts.Entry>()
            {
                new Microcharts.Entry(Cases)
                {
                    Label = "Cases",
                    ValueLabel = Cases.ToString("N0"),
                    Color = SKColor.Parse("#266489")
                },
                new Microcharts.Entry(Deaths)
                {
                    Label = "Deaths",
                    ValueLabel = Deaths.ToString("N0"),
                    Color = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(Recovered)
                {
                    Label = "Recovered",
                    ValueLabel = Recovered.ToString("N0"),
                    Color = SKColor.Parse("#90D585")
                }
            };

            // set font size for the chart label
            var chart = new BarChart() { Entries = entries, LabelTextSize = 40f};

            Chart = chart;
        }

        public async Task RefreshData()
        {
            this.IsRefreshing = true;
         
            await HtmlContent.GetData();
            HtmlContent.GetTotal();

            if (HtmlContent.Total.Count > 0)
            {
                LastUpdate = HtmlContent.LastUpdate;
                Cases = HtmlContent.Total[0];
                Deaths = HtmlContent.Total[1];
                Recovered = HtmlContent.Total[2];
                GenerateChart();
            }
            

            this.IsRefreshing = false;
        }
    }
}