using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBigThree.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedOnToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Collections",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff1e6aa1-9260-482b-819e-c2ccea83b3ec", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEP0sKmCqH+Lt1hsZ9iEfF691b6798SAN3RoPkdh5zg3jke1gFC3HHOATdu57pPjwJQ==", "cf86ae75-4bc3-41eb-a6a9-3390142f7e7d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bcacdb4d-b97e-41c4-a5d9-60a57a4adec7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAELWdi/172pWDABLONC/tpOPxR2+oDCii8afV7UsiXs5bp20UKIKekIChbZoNzK1PXg==", "83c4efb1-d3d5-4532-a96b-d71590122309" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afb4996d-a1ea-4ae4-8325-fa92e985cff6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEGOKGq3G+9mcyzz9GGzcUjJB0pR41DmGwFHi5KKIdmsyDp+M7WAJMM33NeT5zJ1P5Q==", "c20fb11b-df9c-4d57-99bd-d73efaf3df5e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bf1c341-be64-4a12-a94d-324e34f26657", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEPUiTOKljls/qFvUr6hrMvRSLAEUnU8GHoCZ1iVNqUsjYpfS1T5bJcXFF95/JZ4LgQ==", "ab514941-284e-4fde-b9ae-fe65d8778d20" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6ded001f-26ae-42d4-a11c-343abb6781e5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEDPRHYIuNu03WxbJvJYBf8Qgsr57fAez2oxWJlrKUcmtAXF+XwEIH2O/Oajldvb8Mg==", "db6b2a72-e470-4a4b-8c41-ee5c081c6ecf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d745d887-d59c-4393-ba5b-e58ce29550b3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEK4ZcVq5eLQ50mIBBj2XeJAJA9lq9JmEb9HUxcAt1L4FavvGoJpYFVZQzrPHCgI4IA==", "a5de9808-a907-4d88-a7c0-a5521b0aaf8b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32e0c348-128d-40bc-8792-b9a826c430d6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AQAAAAIAAYagAAAAEJ4JNSQ/tQE6sZUBF7LZ14tGy2eoeQpwdJIwSLLU1TdiPmCy2X1jevsdZJvlQVav3A==", "7ae24626-21a9-46f6-897e-b5c99ab81923" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 28, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6090));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 3, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6096));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 5, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6097));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 7, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6098));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 9, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6099));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 12, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6100));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 14, 16, 7, 14, 470, DateTimeKind.Utc).AddTicks(6107));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Collections",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(55)",
                oldMaxLength: 55);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8cc1a9e7-9727-49d0-89ef-15f92fb1e0fb", "AQAAAAIAAYagAAAAEDu+NBo5ATpdC9qJqP6z3cuqVpfBsCT4KCh2wBMt/IBe9Eo3F5ZfGRgzVk4bTF6Xug==", "7666cebf-2c09-4fcb-9307-21933e624363" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b876828c-f4eb-43d7-b1ad-9f8466116f7a", "AQAAAAIAAYagAAAAEMlBYPDop6U4uzioRPRtSyvQz4EopHbBV/WxOeIPSZNhHOQCiuGRBmNvcBjBIuoqfw==", "4211b6c8-b8fb-48bc-8bd2-6d9a074af421" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4caee4f1-03c3-48c9-b71d-b583f8e44dfb", "AQAAAAIAAYagAAAAEKeytYxWEXoUoMRJ9PF4Cf3jJ9sSjuwWzqqsTVo9VtHLWrXBWRDRb+FYiqAfj11lfw==", "cefbf6c6-fed2-4791-b58b-1bb59ec309b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8057a23f-8f31-4bd9-9ce0-0fd870bdc064", "AQAAAAIAAYagAAAAEA45n8wpn/EztQJYGHh07mimnPuWJGSEBRHZ1aqmvpY0874XMeh9Ft3/4ekXSb1ZKw==", "c54af2fd-483b-40b6-84fc-3da46625d4d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "56ebcd72-9a3e-41b4-a405-554d40d2f0cc", "AQAAAAIAAYagAAAAEJ5DcQiRXWnKElGoWBCo+zv9LAfi2ZigfJBdWuRTrU5iv3g2JZq4RvRjtyvHakP11A==", "72785597-4adb-4b57-a8c9-5b0da512398c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1080e15f-b402-4f2d-93c3-b76b5cd10d4f", "AQAAAAIAAYagAAAAEBAS9+oR1auFvkJiavcCzXIYWCVAPAPt1P7gOReW6oPAnUjoypsBBMZKh9WTJVzIrQ==", "b64735aa-7228-45aa-959d-bc7eb552ea91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b24d5d8-b99d-4778-9948-2d8b390f85f9", "AQAAAAIAAYagAAAAEDmr5vSI4FnI9n2fRMNRAw7IrhHpGd4VKKS8ZAI6bYBG1gVXRBHpr6nbZfZogjlcag==", "aded8782-67c2-4cb4-b3f1-a5b3c333dcaf" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 25, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2203));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 28, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2211));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 2, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2212));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 4, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2213));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 6, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2214));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 9, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2215));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 11, 13, 4, 35, 903, DateTimeKind.Utc).AddTicks(2219));
        }
    }
}
