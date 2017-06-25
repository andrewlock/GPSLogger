namespace GpsLogger.Web.Data
{
    public class ExtendedEventDetails
    {
        public string Description { get; set; }
        public int? NumberOfSatelites { get; set; }
        public decimal? Speed { get; set; }
        public decimal? Direction { get; set; }
        public string Provider { get; set; }
        public int? Battery { get; set; }
        public string AndroidId { get; set; }
        public string SerialNumber { get; set; }
        public string Activity { get; set; }
    }
}
