using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveMoney.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeIdentifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeIdentifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agency",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryId = table.Column<int>(nullable: false),
                    AgencyName = table.Column<string>(nullable: true),
                    AgencyType = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    MoneyStored = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agency_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountrySenderId = table.Column<int>(nullable: false),
                    CountryReceiverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comissions_Countries_CountryReceiverId",
                        column: x => x.CountryReceiverId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comissions_Countries_CountrySenderId",
                        column: x => x.CountrySenderId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    TypeIdentificationID = table.Column<int>(nullable: false),
                    Identification = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_TypeIdentifications_TypeIdentificationID",
                        column: x => x.TypeIdentificationID,
                        principalTable: "TypeIdentifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AgencyId = table.Column<int>(nullable: false),
                    UserRoleId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Money = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComissionRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComissionId = table.Column<int>(nullable: false),
                    MinAmount = table.Column<double>(nullable: false),
                    MaxAmount = table.Column<double>(nullable: false),
                    Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissionRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComissionRanges_Comissions_ComissionId",
                        column: x => x.ComissionId,
                        principalTable: "Comissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCashAgents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCashAgents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosingCashAgents_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClosingCashAgents_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCashManagers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CloserId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalComission = table.Column<decimal>(nullable: false),
                    TotalTaxes = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCashManagers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosingCashManagers_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClosingCashManagers_Users_CloserId",
                        column: x => x.CloserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    AgencyDestinationId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    IsClosed = table.Column<bool>(nullable: false),
                    DeliveryType = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Comission = table.Column<double>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Taxes = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Agency_AgencyDestinationId",
                        column: x => x.AgencyDestinationId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(nullable: false),
                    AgencyId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    EventType = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_Agency_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCashManangerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClosingCashMasterId = table.Column<int>(nullable: false),
                    ClosingCashAgentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCashManangerDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosingCashManangerDetails_ClosingCashAgents_ClosingCashAgentId",
                        column: x => x.ClosingCashAgentId,
                        principalTable: "ClosingCashAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClosingCashManangerDetails_ClosingCashManagers_ClosingCashMasterId",
                        column: x => x.ClosingCashMasterId,
                        principalTable: "ClosingCashManagers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClosingCashAgentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClosingCashAgentMasterId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosingCashAgentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClosingCashAgentDetail_ClosingCashAgents_ClosingCashAgentMasterId",
                        column: x => x.ClosingCashAgentMasterId,
                        principalTable: "ClosingCashAgents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClosingCashAgentDetail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agency_CountryId",
                table: "Agency",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashAgentDetail_ClosingCashAgentMasterId",
                table: "ClosingCashAgentDetail",
                column: "ClosingCashAgentMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashAgentDetail_OrderId",
                table: "ClosingCashAgentDetail",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashAgents_RecipientId",
                table: "ClosingCashAgents",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashAgents_SenderId",
                table: "ClosingCashAgents",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashManagers_AgencyId",
                table: "ClosingCashManagers",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashManagers_CloserId",
                table: "ClosingCashManagers",
                column: "CloserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashManangerDetails_ClosingCashAgentId",
                table: "ClosingCashManangerDetails",
                column: "ClosingCashAgentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClosingCashManangerDetails_ClosingCashMasterId",
                table: "ClosingCashManangerDetails",
                column: "ClosingCashMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ComissionRanges_ComissionId",
                table: "ComissionRanges",
                column: "ComissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissions_CountryReceiverId",
                table: "Comissions",
                column: "CountryReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissions_CountrySenderId",
                table: "Comissions",
                column: "CountrySenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_TypeIdentificationID",
                table: "Customers",
                column: "TypeIdentificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AgencyDestinationId",
                table: "Orders",
                column: "AgencyDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RecipientId",
                table: "Orders",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SenderId",
                table: "Orders",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_AgencyId",
                table: "UserLogs",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AgencyId",
                table: "Users",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserRoleId",
                table: "Users",
                column: "UserRoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClosingCashAgentDetail");

            migrationBuilder.DropTable(
                name: "ClosingCashManangerDetails");

            migrationBuilder.DropTable(
                name: "ComissionRanges");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ClosingCashAgents");

            migrationBuilder.DropTable(
                name: "ClosingCashManagers");

            migrationBuilder.DropTable(
                name: "Comissions");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TypeIdentifications");

            migrationBuilder.DropTable(
                name: "Agency");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
