using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheBigThree.Migrations
{
    /// <inheritdoc />
    public partial class ExtendSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "a1b2c3d4-1111-2222-3333-aabbccdd0001", 0, "2129b145-ea9e-4894-99af-2c98d527b239", "snake@shadowmoses.com", false, false, null, "SNAKE@SHADOWMOSES.COM", "SOLIDSNAKE", "AQAAAAIAAYagAAAAENnTZoiJuMVHPU1lIqqkr4kcfvFNYjCSLf/YdmMwF1Nxg7DPg3h+i5sfESiwebKs2w==", null, false, "70cfbce7-c609-487e-b736-3157ecdb2ec7", false, "SolidSnake" },
                    { "a1b2c3d4-1111-2222-3333-aabbccdd0002", 0, "6c027aea-b559-428a-acbe-c5ed31a5df33", "chief@unsc.com", false, false, null, "CHIEF@UNSC.COM", "MASTERCHIEF", "AQAAAAIAAYagAAAAEBrJMdyP1G4ET38Up4LZiJZlW+6diqENMmIIcm83UakBI7o2XGhPUEzgdnYZJaMZIw==", null, false, "42f453d3-6f24-4ecc-bc35-a0bdf3b27704", false, "MasterChief" },
                    { "a1b2c3d4-1111-2222-3333-aabbccdd0003", 0, "d175a367-bc66-4398-9a74-afb0e3629525", "arthur@dutchgang.com", false, false, null, "ARTHUR@DUTCHGANG.COM", "ARTHURMORGAN", "AQAAAAIAAYagAAAAEIpALWMXON09SNRBykp+fv1jLRCA6ceVIATpZOKwz6sNJLjP7yC42l3ZGBModBaISw==", null, false, "284228e7-4aa5-42da-b68e-d45fb17607d4", false, "ArthurMorgan" },
                    { "a1b2c3d4-1111-2222-3333-aabbccdd0004", 0, "0808957a-ecf8-4329-84ef-8ba15aefd847", "lara@croftmanor.com", false, false, null, "LARA@CROFTMANOR.COM", "LARACROFT", "AQAAAAIAAYagAAAAEP5zzPpVc7dLc7sKUdAotJchNe3V2Lpy438pIzWXDj5lt+oVCBHoXG3fcSV8WyqyxA==", null, false, "f032df64-2928-47d3-a285-71a2c2934b48", false, "LaraCroft" },
                    { "a1b2c3d4-1111-2222-3333-aabbccdd0005", 0, "ca029f56-f464-42d0-9c06-56e0599231d1", "aloy@horizon.com", false, false, null, "ALOY@HORIZON.COM", "ALOYHORIZON", "AQAAAAIAAYagAAAAEIU50vJonXCH3KwIQc5pA8cYmStvW2GFcQSNDze+nonZRJNVM7SH/5vNynqt/myzwA==", null, false, "95765d5e-d643-43aa-a083-d4a54f97e884", false, "AloyHorizon" }
                });

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
                columns: new[] { "CreatedOn", "TotalStars" },
                values: new object[] { new DateTime(2026, 2, 27, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6130), 3 });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "Id", "CreatedOn", "Title", "TotalStars", "UserId" },
                values: new object[,]
                {
                    { 3, new DateTime(2026, 3, 1, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6131), "Stealth Legends", 8, "a1b2c3d4-1111-2222-3333-aabbccdd0001" },
                    { 4, new DateTime(2026, 3, 3, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6132), "Shooters That Defined a Generation", 12, "a1b2c3d4-1111-2222-3333-aabbccdd0002" },
                    { 5, new DateTime(2026, 3, 5, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6133), "Open World Masterpieces", 15, "a1b2c3d4-1111-2222-3333-aabbccdd0003" },
                    { 6, new DateTime(2026, 3, 8, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6134), "Indie Gems Worth Your Time", 3, "a1b2c3d4-1111-2222-3333-aabbccdd0004" },
                    { 7, new DateTime(2026, 3, 10, 11, 20, 36, 103, DateTimeKind.Utc).AddTicks(6139), "Horror That Kept Me Up at Night", 7, "a1b2c3d4-1111-2222-3333-aabbccdd0005" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CollectionId", "Description", "GenreId", "ImageUrl", "Title" },
                values: new object[,]
                {
                    { 7, 3, "Metal Gear Solid 3: Snake Eater is a masterpiece of stealth game design set against the backdrop of the Cold War. Following the mission of Naked Snake deep in the Soviet jungle, the game introduces a survival and camouflage system that demands creative thinking and patience. Its emotionally devastating finale remains one of the most impactful moments in gaming history, cementing Big Boss as one of the medium's greatest tragic heroes.", 14, "/images/seed/mgs3.jpg", "Metal Gear Solid 3" },
                    { 8, 3, "Dishonored is a brilliantly designed immersive sim that places player agency above all else. As Corvo Attano, a supernatural assassin framed for murder, the game offers a staggering variety of approaches to every encounter — from ghost-like pacifist runs to chaotic high-power takedowns. Its Victorian-industrial city of Dunwall is one of gaming's most atmospheric settings, dripping with political intrigue and dark lore.", 14, "/images/seed/dishonored.jpg", "Dishonored" },
                    { 9, 3, "Hitman: Blood Money represents the peak of the sandbox assassination formula. Each level is an intricately designed puzzle box filled with creative opportunities to eliminate targets in increasingly elaborate ways. The game rewards patience, observation, and improvisation, allowing Agent 47 to blend seamlessly into any environment. Its iconic missions and darkly comedic tone make it an enduring classic of the stealth genre.", 14, "/images/seed/hitman.jpg", "Hitman: Blood Money" },
                    { 10, 4, "Halo 2 fundamentally changed what was expected of a console first-person shooter. Its dual-wielding mechanics, switching narrative between Master Chief and the Arbiter, and the introduction of online multiplayer via Xbox Live created a template that the industry followed for years. The cliffhanger ending may have frustrated fans at the time, but the game's impact on competitive online gaming is still felt to this day.", 5, "/images/seed/halo2.jpg", "Halo 2" },
                    { 11, 4, "DOOM 2016 revitalized the first-person shooter genre by stripping away cover mechanics and regenerating health in favor of relentless, aggressive combat. The glory kill system encourages constant forward momentum, rewarding fearless play with health and ammo. Mick Gordon's iconic industrial metal soundtrack perfectly complements the non-stop carnage, making this one of the most purely satisfying shooters ever created.", 5, "/images/seed/doom2016.jpg", "DOOM (2016)" },
                    { 12, 4, "Counter-Strike: Global Offensive is the definitive competitive first-person shooter, refined over decades into a game of pure mechanical skill and tactical teamwork. Its economy system, precise gunplay, and iconic maps like Dust2 have created a competitive ecosystem unlike any other. The game demands absolute precision and communication, rewarding dedicated players with a depth of mastery that few shooters can match.", 5, "/images/seed/csgo.jpg", "Counter-Strike: Global Offensive" },
                    { 13, 5, "Red Dead Redemption 2 is Rockstar's magnum opus — a sprawling, painstakingly detailed portrait of the dying American frontier. Arthur Morgan's journey is one of the most emotionally resonant narratives in gaming, exploring themes of loyalty, redemption, and the inevitable march of progress. Every corner of its vast world feels handcrafted and alive, from chance encounters on muddy trails to the bustling streets of Saint Denis.", 16, "/images/seed/rdr2.jpg", "Red Dead Redemption 2" },
                    { 14, 5, "Breath of the Wild reinvented the open world genre by creating a world governed by consistent physical rules rather than scripted events. Every element of Hyrule can be interacted with, climbed, burned, or frozen, giving players unprecedented freedom to approach any situation creatively. Its philosophy of discovery over hand-holding restored a sense of childlike wonder to gaming that had been largely absent from big-budget titles for years.", 2, "/images/seed/botw.jpg", "The Legend of Zelda: Breath of the Wild" },
                    { 15, 5, "Grand Theft Auto V is an unprecedented achievement in open world design, featuring three interwoven protagonists navigating the sun-soaked excess and moral bankruptcy of Los Santos. Its heist missions represent some of the most ambitious set pieces in gaming history, while the living, breathing city remains a benchmark for environmental detail and emergent storytelling. The game's enduring popularity is a testament to the sheer density of its world.", 16, "/images/seed/gtav.jpg", "Grand Theft Auto V" },
                    { 16, 6, "Hollow Knight is a triumph of independent game development, delivering a vast and hauntingly beautiful underground kingdom filled with challenging combat, intricate platforming, and a melancholic lore told through environmental storytelling. Team Cherry's masterpiece rivals the best of the Metroidvania genre, offering dozens of hours of exploration across a world that constantly rewards curiosity and punishes complacency.", 18, "/images/seed/hollowknight.jpg", "Hollow Knight" },
                    { 17, 6, "Hades redefined what a roguelike could be by weaving a compelling narrative directly into its endlessly replayable structure. Each failed escape attempt from the Underworld advances the story and deepens relationships with a cast of brilliantly written mythological characters. Supergiant Games achieved something extraordinary — a game where dying feels as rewarding as succeeding, ensuring every run leaves the player hungry for one more attempt.", 17, "/images/seed/hades.jpg", "Hades" },
                    { 18, 6, "Celeste is a precision platformer that uses its brutally challenging gameplay as a metaphor for mental health struggles, delivering one of gaming's most sincere and affecting narratives. Madeline's climb up the titular mountain mirrors her internal battle with anxiety and self-doubt, creating a rare synergy between mechanics and story. Its tight controls and brilliantly designed levels make every hard-won summit feel like a genuine personal achievement.", 9, "/images/seed/celeste.jpg", "Celeste" },
                    { 19, 7, "The Resident Evil 2 Remake is a near-perfect reimagining of a survival horror classic, updating its fixed-camera predecessor into a tense over-the-shoulder nightmare without sacrificing any of the original's atmosphere or tension. The relentless pursuit of Mr. X creates a constant sense of dread that permeates every corridor of the Raccoon City Police Department. It stands as a blueprint for how to respectfully modernize a beloved classic.", 8, "/images/seed/re2remake.jpg", "Resident Evil 2 Remake" },
                    { 20, 7, "Amnesia: The Dark Descent stripped away combat entirely, leaving players with nothing but darkness, limited resources, and the terrifying sounds of pursuing monsters. Its sanity mechanic punishes even looking at threats for too long, creating a uniquely psychological horror experience. The game fundamentally changed the horror genre and inspired an entire generation of atmospheric, narrative-driven horror titles.", 8, "/images/seed/amnesia.jpg", "Amnesia: The Dark Descent" },
                    { 21, 7, "Outlast drops players into the nightmare of Mount Massive Asylum armed with nothing but a camcorder and their nerve. The absence of any combat mechanics transforms every encounter into pure flight-or-hide survival, creating sustained tension that few horror games have matched. Seen entirely through the grainy green glow of night vision, its corridors filled with disturbing imagery make it one of the most viscerally unsettling horror experiences in gaming.", 8, "/images/seed/outlast.jpg", "Outlast" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Collections",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0001");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0002");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0003");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0004");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a1b2c3d4-1111-2222-3333-aabbccdd0005");

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
                columns: new[] { "CreatedOn", "TotalStars" },
                values: new object[] { new DateTime(2026, 3, 7, 13, 57, 48, 253, DateTimeKind.Utc).AddTicks(5604), 1 });
        }
    }
}
