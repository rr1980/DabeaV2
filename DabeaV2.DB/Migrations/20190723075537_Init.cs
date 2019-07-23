using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DabeaV2.DB.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personen",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    VorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Benutzer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    IsExtern = table.Column<bool>(nullable: false),
                    PersonId = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Passwort = table.Column<string>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benutzer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Benutzer_Personen_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Personen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kontakte",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(nullable: false),
                    PersonId = table.Column<long>(nullable: false),
                    Telefon = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontakte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kontakte_Personen_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Personen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ModificationType = table.Column<int>(nullable: false),
                    ChangedPersonId = table.Column<long>(nullable: true),
                    ChangedBenutzerId = table.Column<long>(nullable: true),
                    ChangedKontaktId = table.Column<long>(nullable: true),
                    BenutzerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modifications_Benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Modifications_Benutzer_ChangedBenutzerId",
                        column: x => x.ChangedBenutzerId,
                        principalTable: "Benutzer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modifications_Kontakte_ChangedKontaktId",
                        column: x => x.ChangedKontaktId,
                        principalTable: "Kontakte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modifications_Personen_ChangedPersonId",
                        column: x => x.ChangedPersonId,
                        principalTable: "Personen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModificationItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PropertyName = table.Column<string>(nullable: true),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    ModificationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModificationItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModificationItems_Modifications_ModificationId",
                        column: x => x.ModificationId,
                        principalTable: "Modifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Benutzer_PersonId",
                table: "Benutzer",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Kontakte_PersonId",
                table: "Kontakte",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ModificationItems_ModificationId",
                table: "ModificationItems",
                column: "ModificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_BenutzerId",
                table: "Modifications",
                column: "BenutzerId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_ChangedBenutzerId",
                table: "Modifications",
                column: "ChangedBenutzerId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_ChangedKontaktId",
                table: "Modifications",
                column: "ChangedKontaktId");

            migrationBuilder.CreateIndex(
                name: "IX_Modifications_ChangedPersonId",
                table: "Modifications",
                column: "ChangedPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModificationItems");

            migrationBuilder.DropTable(
                name: "Modifications");

            migrationBuilder.DropTable(
                name: "Benutzer");

            migrationBuilder.DropTable(
                name: "Kontakte");

            migrationBuilder.DropTable(
                name: "Personen");
        }
    }
}
