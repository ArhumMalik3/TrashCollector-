using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class AddedToCustomerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14137aa-d9bb-4577-9dea-3a6164e4b8d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c81105fe-73e1-423d-9a09-5cd8ce979ed8");

            migrationBuilder.AlterColumn<int>(
                name: "weeklyPickUpDay",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "amountOwed",
                table: "Customers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "endDate",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "extraPickUps",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startDate",
                table: "Customers",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "amountOwed",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "endDate",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "extraPickUps",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "startDate",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "weeklyPickUpDay",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14137aa-d9bb-4577-9dea-3a6164e4b8d8", "12050963-2a03-4406-84b6-2b33632421d3", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c81105fe-73e1-423d-9a09-5cd8ce979ed8", "ac602b60-de61-4f6f-ad31-b6756c219ebf", "Employee", "EMPLOYEE" });
        }
    }
}
