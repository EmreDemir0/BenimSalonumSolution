using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class GelistirilmisLoglama : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SistemLoglar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mesaj = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    HataSeviyesi = table.Column<int>(type: "int", nullable: false),
                    Modul = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IstekYolu = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IpAdresi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HataDetay = table.Column<string>(type: "text", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemLoglar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SistemLoglar_Subeler_SubeId",
                        column: x => x.SubeId,
                        principalTable: "Subeler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SistemLoglar_HataSeviyesi",
                table: "SistemLoglar",
                column: "HataSeviyesi");

            migrationBuilder.CreateIndex(
                name: "IX_SistemLoglar_KullaniciAdi",
                table: "SistemLoglar",
                column: "KullaniciAdi");

            migrationBuilder.CreateIndex(
                name: "IX_SistemLoglar_SubeId",
                table: "SistemLoglar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_SistemLoglar_Tarih",
                table: "SistemLoglar",
                column: "Tarih");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SistemLoglar");
        }
    }
}
