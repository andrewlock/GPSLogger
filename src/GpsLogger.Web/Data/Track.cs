using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GpsLogger.Web.Data
{
    public class Track
    {
        public Guid TrackId { get; set; }
        //public int ApplicationUserId { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }
        public bool IsPubliclyBroadcast { get; set; }
        public bool IsAccessibleViaDirectLink { get; set; }
        public bool IsRecording { get; set; } = true;
        public bool IsArchived { get; set; }

        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateRecordingStopped { get; set; }

        //public ApplicationUser User { get; set; }
        public ICollection<TrackEvent> Events { get; set; }
    }
}
