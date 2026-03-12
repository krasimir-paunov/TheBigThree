using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheBigThree.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededUsersAvatarUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48668352-3932-411a-966a-123456789012",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://avatarfiles.alphacoders.com/148/thumb-1920-148846.jpg", "8cc1a9e7-9727-49d0-89ef-15f92fb1e0fb", "AQAAAAIAAYagAAAAEDu+NBo5ATpdC9qJqP6z3cuqVpfBsCT4KCh2wBMt/IBe9Eo3F5ZfGRgzVk4bTF6Xug==", "7666cebf-2c09-4fcb-9307-21933e624363" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7c13958c-362c-4493-979d-098765432109",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://images3.alphacoders.com/210/thumb-1920-210637.jpg", "b876828c-f4eb-43d7-b1ad-9f8466116f7a", "AQAAAAIAAYagAAAAEMlBYPDop6U4uzioRPRtSyvQz4EopHbBV/WxOeIPSZNhHOQCiuGRBmNvcBjBIuoqfw==", "4211b6c8-b8fb-48bc-8bd2-6d9a074af421" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://images.alphacoders.com/135/thumb-1920-1359067.png", "4caee4f1-03c3-48c9-b71d-b583f8e44dfb", "AQAAAAIAAYagAAAAEKeytYxWEXoUoMRJ9PF4Cf3jJ9sSjuwWzqqsTVo9VtHLWrXBWRDRb+FYiqAfj11lfw==", "cefbf6c6-fed2-4791-b58b-1bb59ec309b4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://images2.alphacoders.com/607/thumb-1920-607052.jpg", "8057a23f-8f31-4bd9-9ce0-0fd870bdc064", "AQAAAAIAAYagAAAAEA45n8wpn/EztQJYGHh07mimnPuWJGSEBRHZ1aqmvpY0874XMeh9Ft3/4ekXSb1ZKw==", "c54af2fd-483b-40b6-84fc-3da46625d4d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://images4.alphacoders.com/136/thumb-1920-1360121.jpeg", "56ebcd72-9a3e-41b4-a405-554d40d2f0cc", "AQAAAAIAAYagAAAAEJ5DcQiRXWnKElGoWBCo+zv9LAfi2ZigfJBdWuRTrU5iv3g2JZq4RvRjtyvHakP11A==", "72785597-4adb-4b57-a8c9-5b0da512398c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://images3.alphacoders.com/716/thumb-1920-716660.jpg", "1080e15f-b402-4f2d-93c3-b76b5cd10d4f", "AQAAAAIAAYagAAAAEBAS9+oR1auFvkJiavcCzXIYWCVAPAPt1P7gOReW6oPAnUjoypsBBMZKh9WTJVzIrQ==", "b64735aa-7228-45aa-959d-bc7eb552ea91" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005",
                columns: new[] { "AvatarUrl", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "https://images6.alphacoders.com/883/thumb-1920-883187.jpg", "0b24d5d8-b99d-4778-9948-2d8b390f85f9", "AQAAAAIAAYagAAAAEDmr5vSI4FnI9n2fRMNRAw7IrhHpGd4VKKS8ZAI6bYBG1gVXRBHpr6nbZfZogjlcag==", "aded8782-67c2-4cb4-b3f1-a5b3c333dcaf" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
