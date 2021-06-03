using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactPage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nvarchar25 = table.Column<string>(name: "nvarchar(25)", nullable: false),
                    nvarchar50 = table.Column<string>(name: "nvarchar(50)", nullable: true),
                    nvarchar11 = table.Column<string>(name: "nvarchar(11)", nullable: false),
                    nvarchar100 = table.Column<string>(name: "nvarchar(100)", nullable: false),
                    nvarchar150 = table.Column<string>(name: "nvarchar(150)", nullable: false),
                    nvarchar250 = table.Column<string>(name: "nvarchar(250)", nullable: true),
                    nvarchar200 = table.Column<string>(name: "nvarchar(200)", nullable: false),
                    nvarchar1000 = table.Column<string>(name: "nvarchar(1000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
