using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class AddedPickupDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cc02f47-75fd-401d-9ecf-0340f541e98c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb88e9ab-b0df-46ae-8ef8-2bcd60bbb35a");

            migrationBuilder.DropColumn(
                name: "extraPickUps",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "PickUp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    pickedUp = table.Column<bool>(nullable: false),
                    timeOfPickup = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickUp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickUp_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1d6cbd00-751d-4da9-a268-1dbe1c46b634", "ba862c66-561f-4319-a7e1-d1f444a11fe0", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ca4c5a45-4ce7-4408-b9fb-38ef4ae1b583", "b66ee89a-b18e-405f-9cd6-bd326386d550", "Employee", "EMPLOYEE" });

            migrationBuilder.CreateIndex(
                name: "IX_PickUp_CustomerId",
                table: "PickUp",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickUp");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d6cbd00-751d-4da9-a268-1dbe1c46b634");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca4c5a45-4ce7-4408-b9fb-38ef4ae1b583");

            migrationBuilder.AddColumn<DateTime>(
                name: "extraPickUps",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7cc02f47-75fd-401d-9ecf-0340f541e98c", "94d7177f-ba93-4d7c-ab1d-6198f5443e6a", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bb88e9ab-b0df-46ae-8ef8-2bcd60bbb35a", "6f68d5fb-4111-4842-ae98-4441f99ef3b8", "Employee", "EMPLOYEE" });
        }
    }
}
