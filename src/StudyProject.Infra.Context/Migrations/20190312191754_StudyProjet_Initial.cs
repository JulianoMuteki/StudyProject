using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudyProject.Infra.Context.Migrations
{
    public partial class StudyProjet_Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                    name: "Clients",
                    columns: table => new
                    {
                        ClientID = table.Column<int>(nullable: false)
                            .Annotation("SQLServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        IsActive = table.Column<bool>(nullable: false),
                        CreateDate = table.Column<DateTime>(nullable: false),
                        Name = table.Column<string>(nullable: false),
                        LastName = table.Column<string>(nullable: false),
                        Email = table.Column<string>(nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Clients", x => x.ClientID);
                    });


            migrationBuilder.InsertData(
             table: "Clients",
             columns: new[] { "ClientID", "IsActive", "CreateDate", "Name", "LastName", "Email" },
             values: new object[] { 1, true, DateTime.Now, "Developer", ".NET", "developer@developer.com" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}