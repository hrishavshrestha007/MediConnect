using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClinicWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppointmentCategories",
                columns: new[] { "Id", "DefaultDurationMinutes", "Name" },
                values: new object[,]
                {
                    { 1, null, "Consultation" },
                    { 2, null, "Follow-up" },
                    { 3, null, "Emergency" },
                    { 4, null, "Routine Check-up" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppointmentCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppointmentCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppointmentCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppointmentCategories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
