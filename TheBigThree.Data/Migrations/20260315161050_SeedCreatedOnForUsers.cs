using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBigThree.Migrations
{
    /// <inheritdoc />
    public partial class SeedCreatedOnForUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78e2f25d-fa43-4f90-9746-58ba286ee425", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEP6K6lUh9KdWZHc3JapOqmWawAhYlnzjh6PRV+zWeOvKNXBmPwGUsUF8uJSRYBNFLA==", "aa8c8241-2dc7-4285-b6b9-4e2669d23ed0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "10982f71-9100-4078-8c12-91953ceb906d", new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEIfftltGeAkGCZf+hOTjrfx2Mki/inl2MTNXyuo+KOTkIrZlWVxscnfm/rApYXxftQ==", "a593a9d2-f3a9-4079-964d-788852b6a76b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e110b288-70c3-4c33-af34-d6756492abf8", new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEFBfjD/jOnyt1czEcAkjy6FgZkrRrcO8TmaWNrMo45EoDKkx59Z/lIlOUv/3/NzJbA==", "3fe68bf9-de8c-4e85-95f4-9db9819f70ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35b8af6c-c450-45c9-bb83-08e87ddda4db", new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEF8XIaZg/k7FKT3x2gxoPa2WrL86YAGsQfiXYST0vMxfTs/ZkddzdYGXdYEoe/Osgg==", "a96cab1c-d25a-45b2-b2f8-be41fd20b9ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5b530bd-8553-4f11-b5d4-51f1fa9dcee8", new DateTime(2024, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEDL4ppkqxveLTS2JVm7G/XrSvEmiaFySgrfg6VDZfDwYLKKOoxCN53vtCjhb0hff7A==", "b4ec1698-7764-4dd6-9a0b-f08d9a2ca89d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c30ff68b-9e24-450c-8014-9fa3abbad618", new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEOic8QexWRUtmTmRvRZDEfnYY0UK1ZrSWqU5/4ikczCWVjg66yNIeO432ZvldkGPhA==", "0bd0bd11-08dd-471f-887e-77090d9c1d93" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005",
                columns: new[] { "ConcurrencyStamp", "CreatedOn", "PasswordHash", "SecurityStamp" },
                values: new object[] { "459c5e1d-0453-4290-9c13-ae9c952286d0", new DateTime(2024, 11, 30, 0, 0, 0, 0, DateTimeKind.Utc), "AQAAAAIAAYagAAAAEF4FwXgFRM6AMlCQVDVgpDpa3jPYCsE1PCx1WJhXjdTwG718IlLaZvz2aRfs3hlVyA==", "f7f55587-aef2-4e2c-b28d-77a5b26be07e" });

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2026, 2, 28, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3645));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 3, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3650));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 5, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3651));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 7, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3653));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 9, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3654));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 12, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3655));

            migrationBuilder.UpdateData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedOn",
                value: new DateTime(2026, 3, 14, 16, 10, 50, 68, DateTimeKind.Utc).AddTicks(3656));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
