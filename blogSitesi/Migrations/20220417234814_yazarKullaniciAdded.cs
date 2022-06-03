using Microsoft.EntityFrameworkCore.Migrations;

namespace blogSitesi.Migrations
{
    public partial class yazarKullaniciAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YazarKullaniciAdi",
                table: "Makale",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YazarKullaniciAdi",
                table: "Makale");
        }
    }
}
