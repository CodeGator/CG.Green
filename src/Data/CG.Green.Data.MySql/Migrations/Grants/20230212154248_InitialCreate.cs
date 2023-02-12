using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CG.Green.Data.MySql.Migrations.Grants
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Green");

            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                schema: "Green",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Data = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Keys",
                schema: "Green",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Use = table.Column<string>(type: "varchar(255)", nullable: true),
                    Algorithm = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataProtected = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                schema: "Green",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Data = table.Column<string>(type: "longtext", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ServerSideSessions",
                schema: "Green",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Scheme = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SubjectId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SessionId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    DisplayName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Renewed = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Data = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerSideSessions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                schema: "Green",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                schema: "Green",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                schema: "Green",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                schema: "Green",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                schema: "Green",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Key",
                schema: "Green",
                table: "PersistedGrants",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                schema: "Green",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                schema: "Green",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSessions_DisplayName",
                schema: "Green",
                table: "ServerSideSessions",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSessions_Expires",
                schema: "Green",
                table: "ServerSideSessions",
                column: "Expires");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSessions_Key",
                schema: "Green",
                table: "ServerSideSessions",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSessions_SessionId",
                schema: "Green",
                table: "ServerSideSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerSideSessions_SubjectId",
                schema: "Green",
                table: "ServerSideSessions",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceCodes",
                schema: "Green");

            migrationBuilder.DropTable(
                name: "Keys",
                schema: "Green");

            migrationBuilder.DropTable(
                name: "PersistedGrants",
                schema: "Green");

            migrationBuilder.DropTable(
                name: "ServerSideSessions",
                schema: "Green");
        }
    }
}
