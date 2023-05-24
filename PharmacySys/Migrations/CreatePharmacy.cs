//using Microsoft.EntityFrameworkCore.Migrations;
//using System;

//namespace PharmacySys.Migrations
//{
//    public partial class CreatePharmacy : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Pharmacies",
//                columns: table => new
//                {
//                    Id = table.Column<int>(nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    Name = table.Column<string>(nullable: true),
//                    Address = table.Column<string>(nullable: true),
//                    City = table.Column<string>(nullable: true),
//                    State = table.Column<string>(nullable: true),
//                    Zip = table.Column<string>(nullable: true),
//                    NumberOfFilledPrescriptions = table.Column<int>(nullable: false),
//                    CreatedDate = table.Column<DateTime>(nullable: false),
//                    UpdatedDate = table.Column<DateTime>(nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
//                });

//            migrationBuilder.InsertData(
//                table: "Pharmacies",
//                columns: new[] { "Name", "Address", "City", "State", "Zip", "NumberOfFilledPrescriptions", "CreatedDate", "UpdatedDate" },
//                values: new object[] { "Pharmacy 1", "123 Street", "City 1", "State 1", "12345", 10, DateTime.UtcNow, DateTime.UtcNow });

//            migrationBuilder.InsertData(
//                table: "Pharmacies",
//                columns: new[] { "Name", "Address", "City", "State", "Zip", "NumberOfFilledPrescriptions", "CreatedDate", "UpdatedDate" },
//                values: new object[] { "Pharmacy 2", "456 Street", "City 2", "State 2", "67890", 20, DateTime.UtcNow, DateTime.UtcNow });
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "Pharmacies");
//        }
//    }
//}