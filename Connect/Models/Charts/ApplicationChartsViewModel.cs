using System.Collections.Generic;

namespace Connect.Models.Charts
{
    public class ApplicationChartsViewModel
    {
        public IList<ApplicationChartData> Applications { get; set; }
    }

    public class ApplicationChartData
    {
        public string Campus { get; set; }
        public int Count { get; set; }
    }
}