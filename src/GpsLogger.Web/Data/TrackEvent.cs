using System;
using System.ComponentModel.DataAnnotations;

namespace GpsLogger.Web.Data
{
    public class TrackEvent
    {
        public Guid TrackEventId { get; set; }
        public Guid TrackId { get; set; }
        public Track Track { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset EventTime { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal? Altitude { get; set; }
        public decimal? Accuracy { get; set; }
        public string ExtraProperties { get; set; }
    }
}
