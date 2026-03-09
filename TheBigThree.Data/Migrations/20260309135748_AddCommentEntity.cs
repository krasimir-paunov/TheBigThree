using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBigThree.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "94a95976-c5ed-40a7-ba4f-4651f0685f61", "AQAAAAIAAYagAAAAEITtir2fc6GWpAFcntJgLPChbzs1o1U1g9m+tPM+qRj28WJa/vDgU3i5NV2A7a0Bqw==", "8b77c84e-88f2-4635-ab1d-57b65d6dbc7c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c7f3a972-c05f-41a2-be50-993ae3d34583", "AQAAAAIAAYagAAAAEG6pe8Oz4/v6lywsIKs5q3mCIAgFHpXOgIOi6QXB/drWdkQamUGSby8IpaZSvnDsJw==", "7ca4dfc1-242a-4808-a2e4-d73010023bea" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 4, 13, 57, 48, 253, DateTimeKind.Utc).AddTicks(5599));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 7, 13, 57, 48, 253, DateTimeKind.Utc).AddTicks(5604));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CollectionId",
                table: "Comments",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bafcfe9e-b0a2-47a7-b17e-2217c96f649c", "AQAAAAIAAYagAAAAECI3DvNrudh1T0lLAEw+gRfhXAcBVgNG+hTt6da5j5NyuHoEU2uGqnbK7CWITEY6Cw==", "4c176def-4b52-4c10-b605-f59e54bdc282" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c118dcc4-9b3f-4493-96c7-c08b2f804047", "AQAAAAIAAYagAAAAEGUyZwakLfIC7wZqfAbIIMB8eTNpuH2um+YnKor9nrg73PVLLwp78qq8e42KuGDAcA==", "f4a82053-59c8-4e82-ac8a-b04b34737dde" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 1, 29, 7, 26, 25, 319, DateTimeKind.Utc).AddTicks(9855));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 1, 7, 26, 25, 319, DateTimeKind.Utc).AddTicks(9860));
        }
    }
}
