using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityDataBases.Storage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategoryParts",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 50, nullable: false),
                    szDescription = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategoryParts", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblCity",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 50, nullable: false),
                    izNumberOfWarehouses = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCity", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblManufacturer",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 50, nullable: false),
                    szCountry = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblManufacturer", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblOrdersParts",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    gOrderId = table.Column<Guid>(nullable: false),
                    gPartId = table.Column<Guid>(nullable: false),
                    izNumberInOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrdersParts", x => x.gId);
                });

            migrationBuilder.CreateTable(
                name: "tblStorage",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    izStorageNumber = table.Column<int>(nullable: false),
                    szAddress = table.Column<string>(maxLength: 100, nullable: false),
                    gCityId = table.Column<Guid>(nullable: false),
                    szCityName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStorage", x => x.gId);
                    table.ForeignKey(
                        name: "FK_tblStorage_tblCity_gCityId",
                        column: x => x.gCityId,
                        principalTable: "tblCity",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblCarModel",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 50, nullable: false),
                    izYear = table.Column<int>(nullable: false),
                    gManufacturerId = table.Column<Guid>(nullable: false),
                    szManufacturerName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCarModel", x => x.gId);
                    table.ForeignKey(
                        name: "FK_tblCarModel_tblManufacturer_gManufacturerId",
                        column: x => x.gManufacturerId,
                        principalTable: "tblManufacturer",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOrder",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    izOrderNumber = table.Column<int>(nullable: false),
                    dcCost = table.Column<decimal>(maxLength: 25, nullable: false),
                    dtOrderTime = table.Column<DateTime>(nullable: false),
                    szClient = table.Column<string>(maxLength: 50, nullable: false),
                    gStorageId = table.Column<Guid>(nullable: false),
                    izStorageStorageNumber = table.Column<int>(maxLength: 50, nullable: false),
                    szStorageAddress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrder", x => x.gId);
                    table.ForeignKey(
                        name: "FK_tblOrder_tblStorage_gStorageId",
                        column: x => x.gStorageId,
                        principalTable: "tblStorage",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblPart",
                columns: table => new
                {
                    gId = table.Column<Guid>(nullable: false),
                    szName = table.Column<string>(maxLength: 50, nullable: false),
                    dcCost = table.Column<decimal>(maxLength: 25, nullable: false),
                    szDescription = table.Column<string>(maxLength: 1000, nullable: false),
                    gCategoryId = table.Column<Guid>(nullable: false),
                    szCategoryName = table.Column<string>(maxLength: 50, nullable: false),
                    gCarModelId = table.Column<Guid>(nullable: false),
                    szCarModelName = table.Column<string>(maxLength: 50, nullable: false),
                    szManufacturerName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPart", x => x.gId);
                    table.ForeignKey(
                        name: "FK_tblPart_tblCarModel_gCarModelId",
                        column: x => x.gCarModelId,
                        principalTable: "tblCarModel",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblPart_tblCategoryParts_gCategoryId",
                        column: x => x.gCategoryId,
                        principalTable: "tblCategoryParts",
                        principalColumn: "gId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblCarModel_gManufacturerId",
                table: "tblCarModel",
                column: "gManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrder_gStorageId",
                table: "tblOrder",
                column: "gStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPart_gCarModelId",
                table: "tblPart",
                column: "gCarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPart_gCategoryId",
                table: "tblPart",
                column: "gCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblStorage_gCityId",
                table: "tblStorage",
                column: "gCityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOrder");

            migrationBuilder.DropTable(
                name: "tblOrdersParts");

            migrationBuilder.DropTable(
                name: "tblPart");

            migrationBuilder.DropTable(
                name: "tblStorage");

            migrationBuilder.DropTable(
                name: "tblCarModel");

            migrationBuilder.DropTable(
                name: "tblCategoryParts");

            migrationBuilder.DropTable(
                name: "tblCity");

            migrationBuilder.DropTable(
                name: "tblManufacturer");
        }
    }
}
