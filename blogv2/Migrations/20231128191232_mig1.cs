using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogv2.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Makaleler",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    baslik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    icerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yazanKisi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    yazilimTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    okumaSure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    icerikResim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    goruntulenme = table.Column<int>(type: "int", nullable: false),
                    kategori = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makaleler", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Makaleler");
        }
    }
}
