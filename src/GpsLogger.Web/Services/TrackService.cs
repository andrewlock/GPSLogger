using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpsLogger.Web.Data;

namespace GpsLogger.Web.Services
{
    public class TrackService
    {
        private readonly ApplicationDbContext _dbContext;
        public TrackService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Attempt to load a track with the given start time.
        /// If no track exists, returns null
        /// </summary>
        /// <param name="startTime"></param>
        /// <returns></returns>
        public Track GetTrackWithStartTime(DateTimeOffset startTime)
        {
            return _dbContext.Tracks
                .FirstOrDefault(x => x.DateCreated == startTime);
        }

        public void InsertTrackEvent(TrackEvent @event)
        {
            _dbContext.TrackEvents.Add(@event);
            _dbContext.SaveChanges();
        }

        public void CreateTrackWithEvent(Track track, TrackEvent @event)
        {
            _dbContext.Tracks.Add(track);
            _dbContext.TrackEvents.Add(@event);
            _dbContext.SaveChanges();
        }

        public TrackViewModel GetLatestTrack()
        {
            return _dbContext.Tracks
                .OrderByDescending(x => x.DateCreated)
                .Select(x => new TrackViewModel
                {
                    Name = x.Name,
                    Events = x.Events
                        .OrderBy(e => e.EventTime)
                        .Select(e => new TrackViewModel.Event
                        {
                            EventTime = e.EventTime,
                            Accuracy = e.Accuracy,
                            Latitude = e.Latitude,
                            Longitude = e.Longitude,
                        })
                        .ToList()
                })
                .FirstOrDefault();

        }

        public class TrackViewModel
        {
            public string Name { get; set; }
            public ICollection<Event> Events { get; set; }

            public class Event
            {
                public DateTimeOffset EventTime { get; set; }
                public decimal Latitude { get; set; }
                public decimal Longitude { get; set; }
                public decimal? Accuracy { get; set; }
            }
        }
    }
}

