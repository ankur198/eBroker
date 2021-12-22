using Microsoft.EntityFrameworkCore.Migrations;

namespace eBroker.Migrations
{
    public partial class holding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Equities",
                table: "Equities");

            migrationBuilder.DropColumn(
                name: "Holdings",
                table: "Traders");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Equities");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Equities",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "EquityId",
                table: "Equities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equities",
                table: "Equities",
                column: "EquityId");

            migrationBuilder.CreateTable(
                name: "Holdings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TraderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holdings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holdings_Equities_EquityId",
                        column: x => x.EquityId,
                        principalTable: "Equities",
                        principalColumn: "EquityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Holdings_Traders_TraderId",
                        column: x => x.TraderId,
                        principalTable: "Traders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_EquityId",
                table: "Holdings",
                column: "EquityId");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_TraderId",
                table: "Holdings",
                column: "TraderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holdings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equities",
                table: "Equities");

            migrationBuilder.DropColumn(
                name: "EquityId",
                table: "Equities");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Equities",
                newName: "price");

            migrationBuilder.AddColumn<string>(
                name: "Holdings",
                table: "Traders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "Equities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equities",
                table: "Equities",
                column: "id");
        }
    }
}
