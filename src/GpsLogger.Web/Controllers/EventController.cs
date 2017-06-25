using System;
using System.ComponentModel.DataAnnotations;
using GpsLogger.Web.Data;
using GpsLogger.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GpsLogger.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly TrackService _service;
        public EventController(TrackService service)
        {
            _service = service;
        }

        public IActionResult Log(LogViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var startTime = viewModel.Start;
            var track = _service.GetTrackWithStartTime(startTime);

            if(track == null)
            {
                track = viewModel.ToNewTrack();
                var @event = viewModel.ToTrackEvent(track.TrackId);
                _service.CreateTrackWithEvent(track, @event);
            }
            else
            {
                var @event = viewModel.ToTrackEvent(track.TrackId);
                _service.InsertTrackEvent(@event);
            }

            return Ok();
        }

        public class LogViewModel
        {
            [Required]
            public DateTimeOffset Time { get; set; }
            [Required]
            public DateTimeOffset Start { get; set; }

            [Required]
            public decimal? Lat { get; set; }
            [Required]
            public decimal? Lon { get; set; }
            public decimal? Alt { get; set; }
            public decimal? Acc { get; set; }
            [MaxLength(511)]
            public string Desc { get; set; }
            public int? Sats { get; set; }
            public decimal? Spd { get; set; }

            public TrackEvent ToTrackEvent(Guid trackId)
            {
                var details = new ExtendedEventDetails()
                {
                    Description = Desc,
                    NumberOfSatelites = Sats,
                    Speed = Spd,
                };

                return new TrackEvent
                {
                    TrackEventId = Guid.NewGuid(),
                    TrackId = trackId,
                    Accuracy = Acc,
                    Altitude = Alt,
                    CreatedAt = DateTimeOffset.UtcNow,
                    EventTime = Time,
                    Latitude = Lat.Value,
                    Longitude = Lon.Value,
                    StartTime = Start,
                    ExtraProperties = JsonConvert.SerializeObject(details),
                };
            }

            public Track ToNewTrack()
            {
                return new Track
                {
                    TrackId = Guid.NewGuid(),
                    Name = Desc ?? "New GPS Logger Track",
                    IsPubliclyBroadcast = true,
                    IsAccessibleViaDirectLink = true,
                    IsRecording = true,
                    IsArchived = false,
                    DateCreated = Start,
                };
            }
        }
    }
}
