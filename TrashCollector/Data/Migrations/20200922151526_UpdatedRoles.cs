using Microsoft.EntityFrameworkCore.Migrations;

namespace TrashCollector.Data.Migrations
{
    public partial class UpdatedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbe49b19-9711-4998-b160-21ff16b36b98");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b14137aa-d9bb-4577-9dea-3a6164e4b8d8", "12050963-2a03-4406-84b6-2b33632421d3", "Customer", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c81105fe-73e1-423d-9a09-5cd8ce979ed8", "ac602b60-de61-4f6f-ad31-b6756c219ebf", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b14137aa-d9bb-4577-9dea-3a6164e4b8d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c81105fe-73e1-423d-9a09-5cd8ce979ed8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dbe49b19-9711-4998-b160-21ff16b36b98", "aca6b866-279b-4e95-9949-ef57b6717252", "Admin", "ADMIN" });
        }
    }
}
