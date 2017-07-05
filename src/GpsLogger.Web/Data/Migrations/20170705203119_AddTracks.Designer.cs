using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GpsLogger.Web.Data;

namespace GpsLogger.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170705203119_AddTracks")]
    partial class AddTracks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("GpsLogger.Web.Data.Track", b =>
                {
                    b.Property<Guid>("TrackId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateCreated");

                    b.Property<DateTimeOffset?>("DateRecordingStopped");

                    b.Property<bool>("IsAccessibleViaDirectLink");

                    b.Property<bool>("IsArchived");

                    b.Property<bool>("IsPubliclyBroadcast");

                    b.Property<bool>("IsRecording");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("TrackId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("GpsLogger.Web.Data.TrackEvent", b =>
                {
                    b.Property<Guid>("TrackEventId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal?>("Accuracy");

                    b.Property<decimal?>("Altitude");

                    b.Property<DateTimeOffset>("CreatedAt");

                    b.Property<DateTimeOffset>("EventTime");

                    b.Property<string>("ExtraProperties");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<DateTimeOffset>("StartTime");

                    b.Property<Guid>("TrackId");

                    b.HasKey("TrackEventId");

                    b.HasIndex("TrackId");

                    b.ToTable("TrackEvents");
                });

            modelBuilder.Entity("GpsLogger.Web.Data.TrackEvent", b =>
                {
                    b.HasOne("GpsLogger.Web.Data.Track", "Track")
                        .WithMany("Events")
                        .HasForeignKey("TrackId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
