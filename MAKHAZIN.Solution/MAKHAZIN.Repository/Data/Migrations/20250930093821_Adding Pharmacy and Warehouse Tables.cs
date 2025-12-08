using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MAKHAZIN.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingPharmacyandWarehouseTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Users_UserId",
                table: "StockItems");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StockItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PharmacyId",
                table: "StockItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "StockItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_PharmacyId",
                table: "StockItems",
                column: "PharmacyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockItems_WarehouseId",
                table: "StockItems",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacies_UserId",
                table: "Pharmacies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_UserId",
                table: "Warehouses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Pharmacies_PharmacyId",
                table: "StockItems",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Users_UserId",
                table: "StockItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Warehouses_WarehouseId",
                table: "StockItems",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Pharmacies_PharmacyId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Users_UserId",
                table: "StockItems");

            migrationBuilder.DropForeignKey(
                name: "FK_StockItems_Warehouses_WarehouseId",
                table: "StockItems");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_PharmacyId",
                table: "StockItems");

            migrationBuilder.DropIndex(
                name: "IX_StockItems_WarehouseId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "PharmacyId",
                table: "StockItems");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "StockItems");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StockItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockItems_Users_UserId",
                table: "StockItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
