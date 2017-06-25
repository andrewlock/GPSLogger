using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GpsLogger.Web.Data.Migrations
{
    public partial class Removeuserrequirement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateRecordingStopped = table.Column<DateTimeOffset>(nullable: true),
                    IsAccessibleViaDirectLink = table.Column<bool>(nullable: false),
                    IsArchived = table.Column<bool>(nullable: false),
                    IsPubliclyBroadcast = table.Column<bool>(nullable: false),
                    IsRecording = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                });

            migrationBuilder.CreateTable(
                name: "TrackEvents",
                columns: table => new
                {
                    TrackEventId = table.Column<Guid>(nullable: false),
                    Accuracy = table.Column<decimal>(nullable: true),
                    Altitude = table.Column<decimal>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    EventTime = table.Column<DateTimeOffset>(nullable: false),
                    ExtraProperties = table.Column<string>(nullable: true),
                    Latitude = table.Column<decimal>(nullable: false),
                    Longitude = table.Column<decimal>(nullable: false),
                    StartTime = table.Column<DateTimeOffset>(nullable: false),
                    TrackId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackEvents", x => x.TrackEventId);
                    table.ForeignKey(
                        name: "FK_TrackEvents_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackEvents_TrackId",
                table: "TrackEvents",
                column: "TrackId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackEvents");

            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
