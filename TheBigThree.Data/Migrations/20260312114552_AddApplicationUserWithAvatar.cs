using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBigThree.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserWithAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "484b5d74-6f70-49fd-a94f-4ab382ce570f", "AQAAAAIAAYagAAAAENyuOQQHg81q9bOGPytlxE7IuCs6Og7ccOil7KrLFIfRcCZpDayvshkpi2e0gIfLGQ==", "4be95166-69ff-42a2-9b53-5289663d83e3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "a7b929c2-a21d-4848-b393-5418d5157a3c", "AQAAAAIAAYagAAAAECpDr0nq+zhg9xdRAQ4D3HaaXjCVvLoHHeVH5oN1gq3Y0IuRwt6HQcjh3e7KbxffxQ==", "fffebb95-806a-428e-9c5d-4d008554555d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "319b40c1-15f0-4786-86eb-4d71f565d0f8", "AQAAAAIAAYagAAAAEJfjZWQn8jGj713VPpI7tAded2S6DOcjXFK2ax62IsvfWNjJAY8xZt+V+VcLzhNFrg==", "14dac4fa-f68d-41f4-9515-dcb7bf2e5f6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "2ee5978b-841b-4e62-bbfe-ac1243946cb4", "AQAAAAIAAYagAAAAEGTPuV0cLw/uPQVTshcZICcxTXVg8PXl5vIR5o68StmWYbiNjT1+f5JuRRg4PlbTAA==", "9a2cd622-2358-4272-a356-a2bb49651d92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "c25197dc-3a13-466e-8fca-581e95d3db06", "AQAAAAIAAYagAAAAEGgTZQ6cx3No9lUFp2cJVyvy+Y5IdEUxgaQLfISip81gqTvQ0XDSEIsVeNmmZMZCzA==", "56277386-9919-4b6c-8dfc-65c7970cabad" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "d23b9492-1d62-4d78-a7ad-a80917f6d45f", "AQAAAAIAAYagAAAAEIYgAstLz0Iyb5RJ9DAF9opqsRSODL1pXXIntkGvyEplKCk2Kx3RGIAFGS9iSKBaKg==", "7f677d2b-5470-4869-8c98-9ecd5ee7f093" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "ea9e207d-8da3-4fc3-8d8b-e955f858700e", "AQAAAAIAAYagAAAAEPorByYSE+b8Naz3db5j9mBpOl4RG1Hf9DAh9CBfD+gaBN0rwmE5MXqpIOgMOMQi2g==", "6f12e795-14b5-4b51-8e2e-e57eccf7cffe" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 25, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1519));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 28, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1533));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 2, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1534));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 4, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1536));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 6, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 9, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1539));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 11, 11, 45, 52, 71, DateTimeKind.Utc).AddTicks(1540));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c394de7-4981-4b3b-ad73-e9690f79d6b8", "AQAAAAIAAYagAAAAEFkfmyepdU9tcOvEBfHxl81F6I8LlvbGAWjCLuXPjP5WNaS+HErGw2LY5AdWqKX9Og==", "b64505f9-fdd1-4a67-8b7e-809b35e7962d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40a459b5-2bb8-4e4b-8488-38b9d01620a8", "AQAAAAIAAYagAAAAEPoWfsaBmgTpA5PMMYyjgD1ElgUlVGzGPLW5oBCUYReYATc84YmUrdYJU2MIZp6t2A==", "6082362e-e758-427d-b26c-d19803d72b04" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2129b145-ea9e-4894-99af-2c98d527b239", "AQAAAAIAAYagAAAAENnTZoiJuMVHPU1lIqqkr4kcfvFNYjCSLf/YdmMwF1Nxg7DPg3h+i5sfESiwebKs2w==", "70cfbce7-c609-487e-b736-3157ecdb2ec7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6c027aea-b559-428a-acbe-c5ed31a5df33", "AQAAAAIAAYagAAAAEBrJMdyP1G4ET38Up4LZiJZlW+6diqENMmIIcm83UakBI7o2XGhPUEzgdnYZJaMZIw==", "42f453d3-6f24-4ecc-bc35-a0bdf3b27704" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d175a367-bc66-4398-9a74-afb0e3629525", "AQAAAAIAAYagAAAAEIpALWMXON09SNRBykp+fv1jLRCA6ceVIATpZOKwz6sNJLjP7yC42l3ZGBModBaISw==", "284228e7-4aa5-42da-b68e-d45fb17607d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0808957a-ecf8-4329-84ef-8ba15aefd847", "AQAAAAIAAYagAAAAEP5zzPpVc7dLc7sKUdAotJchNe3V2Lpy438pIzWXDj5lt+oVCBHoXG3fcSV8WyqyxA==", "f032df64-2928-47d3-a285-71a2c2934b48" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ca029f56-f464-42d0-9c06-56e0599231d1", "AQAAAAIAAYagAAAAEIU50vJonXCH3KwIQc5pA8cYmStvW2GFcQSNDze+nonZRJNVM7SH/5vNynqt/myzwA==", "95765d5e-d643-43aa-a083-d4a54f97e884" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 24, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6123));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 27, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6130));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 1, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6131));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 3, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6132));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 5, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6133));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 8, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6134));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 10, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6139));
        }
    }
}
