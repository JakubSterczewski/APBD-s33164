using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace System_Uznawania_Przychodów.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoftwareVersions",
                columns: table => new
                {
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftwareVersions", x => new { x.SoftwareId, x.Version });
                    table.ForeignKey(
                        name: "FK_SoftwareVersions_Softwares_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Softwares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Address", "DeletedAt", "Email", "Phone" },
                values: new object[,]
                {
                    { 1, "Addr1", null, "client1@test.com", "100000001" },
                    { 2, "Addr2", null, "client2@test.com", "100000002" },
                    { 3, "Addr3", null, "client3@test.com", "100000003" },
                    { 4, "Addr4", null, "client4@test.com", "100000004" },
                    { 5, "Addr5", null, "client5@test.com", "100000005" }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "IsOnAllProducts", "Name", "Percentage", "PurchaseModel", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Disc1", 10m, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Disc2", 15m, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "Name", "Percentage", "PurchaseModel", "StartDate" },
                values: new object[] { 3, new DateTime(2026, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Disc3", 8m, 0, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "IsOnAllProducts", "Name", "Percentage", "PurchaseModel", "StartDate" },
                values: new object[] { 4, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Disc4", 20m, 0, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Softwares",
                columns: new[] { "Id", "Category", "CurrentVersion", "Description", "MonthlySubscriptionPrice", "Name", "YearlyLicensePrice" },
                values: new object[,]
                {
                    { 1, "Finance", "3.2.1", "Desc1", 1000m, "Prog1", 10000m },
                    { 2, "Education", "1.8.0", "Desc2", 500m, "Prog2", 5000m },
                    { 3, "Education", "2.0.4", "Desc3", 800m, "Prog3", 8000m }
                });

            migrationBuilder.InsertData(
                table: "CompanyClients",
                columns: new[] { "ClientId", "CompanyName", "Krs" },
                values: new object[,]
                {
                    { 4, "Corp1", "0000000001" },
                    { 5, "Corp2", "0000000002" }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "AdditionalSupportYears", "ClientId", "EndDate", "IsActive", "Price", "SignedAt", "SoftwareId", "SoftwareVersion", "StartDate", "TotalPaid" },
                values: new object[,]
                {
                    { 1, 0, 1, new DateOnly(2026, 1, 15), true, 10000m, new DateOnly(2026, 1, 10), 1, "3.2.1", new DateOnly(2026, 1, 1), 10000m },
                    { 2, 1, 2, new DateOnly(2026, 6, 20), true, 5000m, null, 2, "1.8.0", new DateOnly(2026, 6, 1), 2000m }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "AdditionalSupportYears", "ClientId", "EndDate", "Price", "SignedAt", "SoftwareId", "SoftwareVersion", "StartDate" },
                values: new object[] { 3, 0, 3, new DateOnly(2026, 5, 20), 8000m, null, 3, "2.0.4", new DateOnly(2026, 5, 1) });

            migrationBuilder.InsertData(
                table: "IndividualClients",
                columns: new[] { "ClientId", "FirstName", "LastName", "Pesel" },
                values: new object[,]
                {
                    { 1, "First1", "Last1", "00000000001" },
                    { 2, "First2", "Last2", "00000000002" },
                    { 3, "First3", "Last3", "00000000003" }
                });

            migrationBuilder.InsertData(
                table: "SoftwareDiscounts",
                columns: new[] { "DiscountId", "SoftwareId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "SoftwareVersions",
                columns: new[] { "SoftwareId", "Version" },
                values: new object[,]
                {
                    { 1, "1.0.0" },
                    { 1, "2.0.0" },
                    { 1, "3.2.1" },
                    { 2, "1.0.0" },
                    { 2, "1.8.0" },
                    { 3, "1.0.0" },
                    { 3, "2.0.4" }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClientId", "HasPaid", "Name", "NextPeriodStart", "RenewalPeriodMonths", "RenewalPrice", "SoftwareId", "StartDate" },
                values: new object[,]
                {
                    { 1, 4, true, "Sub1", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1000m, 1, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 5, true, "Sub2", new DateTime(2027, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 5000m, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Subscriptions",
                columns: new[] { "Id", "ClientId", "HasPaid", "IsCancelled", "Name", "NextPeriodStart", "RenewalPeriodMonths", "RenewalPrice", "SoftwareId", "StartDate" },
                values: new object[] { 3, 1, false, true, "Sub3", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 800m, 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "ContractPayments",
                columns: new[] { "Id", "Amount", "ContractId", "PaidAt" },
                values: new object[,]
                {
                    { 1, 10000m, 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2000m, 2, new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPayments",
                columns: new[] { "Id", "Amount", "PaidAt", "SubscriptionId" },
                values: new object[,]
                {
                    { 1, 1000m, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1000m, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 5000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, 800m, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoftwareVersions");

            migrationBuilder.DeleteData(
                table: "CompanyClients",
                keyColumn: "ClientId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CompanyClients",
                keyColumn: "ClientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ContractPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContractPayments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "IndividualClients",
                keyColumn: "ClientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "IndividualClients",
                keyColumn: "ClientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "IndividualClients",
                keyColumn: "ClientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SoftwareDiscounts",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "SoftwareDiscounts",
                keyColumns: new[] { "DiscountId", "SoftwareId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "SubscriptionPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubscriptionPayments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubscriptionPayments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubscriptionPayments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contracts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subscriptions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Softwares",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
