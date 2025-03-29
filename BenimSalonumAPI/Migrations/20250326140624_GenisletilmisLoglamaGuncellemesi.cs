using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BenimSalonumAPI.Migrations
{
    /// <inheritdoc />
    public partial class GenisletilmisLoglamaGuncellemesi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciLoglari",
                table: "KullaniciLoglari");

            migrationBuilder.RenameTable(
                name: "KullaniciLoglari",
                newName: "KullaniciLoglar");

            migrationBuilder.AddColumn<int>(
                name: "Gorünürlük",
                table: "SistemLoglar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Veri",
                table: "SistemLoglar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detay",
                table: "KullaniciLoglar",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAdresi",
                table: "KullaniciLoglar",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubeId",
                table: "KullaniciLoglar",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciLoglar",
                table: "KullaniciLoglar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SistemLoglar_Gorünürlük",
                table: "SistemLoglar",
                column: "Gorünürlük");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciLoglar_KullaniciAdi",
                table: "KullaniciLoglar",
                column: "KullaniciAdi");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciLoglar_SubeId",
                table: "KullaniciLoglar",
                column: "SubeId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciLoglar_YapilanIslemTarihi",
                table: "KullaniciLoglar",
                column: "YapilanIslemTarihi");

            migrationBuilder.AddForeignKey(
                name: "FK_KullaniciLoglar_Subeler_SubeId",
                table: "KullaniciLoglar",
                column: "SubeId",
                principalTable: "Subeler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KullaniciLoglar_Subeler_SubeId",
                table: "KullaniciLoglar");

            migrationBuilder.DropIndex(
                name: "IX_SistemLoglar_Gorünürlük",
                table: "SistemLoglar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KullaniciLoglar",
                table: "KullaniciLoglar");

            migrationBuilder.DropIndex(
                name: "IX_KullaniciLoglar_KullaniciAdi",
                table: "KullaniciLoglar");

            migrationBuilder.DropIndex(
                name: "IX_KullaniciLoglar_SubeId",
                table: "KullaniciLoglar");

            migrationBuilder.DropIndex(
                name: "IX_KullaniciLoglar_YapilanIslemTarihi",
                table: "KullaniciLoglar");

            migrationBuilder.DropColumn(
                name: "Gorünürlük",
                table: "SistemLoglar");

            migrationBuilder.DropColumn(
                name: "Veri",
                table: "SistemLoglar");

            migrationBuilder.DropColumn(
                name: "Detay",
                table: "KullaniciLoglar");

            migrationBuilder.DropColumn(
                name: "IpAdresi",
                table: "KullaniciLoglar");

            migrationBuilder.DropColumn(
                name: "SubeId",
                table: "KullaniciLoglar");

            migrationBuilder.RenameTable(
                name: "KullaniciLoglar",
                newName: "KullaniciLoglari");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KullaniciLoglari",
                table: "KullaniciLoglari",
                column: "Id");
        }
    }
}
