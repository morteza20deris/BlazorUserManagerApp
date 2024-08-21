using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorUserManagerApp.Migrations
{
    /// <inheritdoc />
    public partial class MyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: true),
                    Avatar = table.Column<string>(type: "TEXT", nullable: true),
                    Salary = table.Column<decimal>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: true),
                    Role = table.Column<int>(type: "INTEGER", nullable: true),
                    LastPasswordChange = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ChangePaswword = table.Column<bool>(type: "INTEGER", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0", null, "Admin", "ADMIN" },
                    { "1", null, "Supervisor", "SUPERVISOR" },
                    { "2", null, "Operator", "OPERATOR" },
                    { "3", null, "WhithoutRole", "WHITHOUTROLE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Active", "Avatar", "ChangePaswword", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FullName", "LastPasswordChange", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "Salary", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[,]
                {
                    { "-1041728855", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/252.jpg", true, "f5f629cd-13a0-4ed1-918e-74b9a5931ee8", "Employee", "Citlalli.Koch@hotmail.com", true, "Carley Yundt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(783) 768-5183 x9700", false, 3, 52251m, "da2006eb-0cac-44da-9a0f-2fe7703ee958", false, 3, "Annabelle75" },
                    { "-1064855082", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/354.jpg", true, "1da29396-de72-47c8-83ea-3ba7a82f3f6e", "Employee", "Alexandra88@hotmail.com", true, "Ernestine Mills", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "380.517.2255 x7109", false, 3, 92344m, "c14c35d5-94bc-486a-9e11-3f5385d311c0", false, 2, "Katrina_Koch53" },
                    { "-1202671349", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/42.jpg", true, "833a20df-0357-4e98-86c1-cf0904e091fd", "Employee", "Ashly50@gmail.com", true, "Giuseppe Satterfield", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(921) 359-2018", false, 3, 88878m, "d6ae4723-e9ef-462e-9971-7a455ee2ac26", false, 0, "Royal_Huel" },
                    { "-1325717676", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/379.jpg", true, "12f17541-cd8d-4407-904f-7bd46d36eee2", "Employee", "Jesse59@yahoo.com", true, "Zola Quitzon", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-267-569-6576", false, 3, 92898m, "844cb6bc-082f-4665-8c37-fd3378036c80", false, 3, "Catharine_Rolfson61" },
                    { "-1380571505", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/992.jpg", true, "798f1556-ed62-4d0b-a91e-13cdee1e8400", "Employee", "Clair41@hotmail.com", true, "Geovanni Harvey", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-356-491-2834 x2235", false, 3, 58839m, "435a33f6-00ec-43a8-9ca3-5dbbd23a2c26", false, 3, "Gregg.Waters68" },
                    { "-1539967329", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/329.jpg", true, "55449886-3499-4aaf-a93b-d512e2d42816", "Employee", "Kariane.Witting@hotmail.com", true, "Timmy Cummerata", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(516) 688-2295", false, 3, 71233m, "7db3de35-8173-4f1d-9587-37b7db071ef6", false, 0, "Nina_Deckow" },
                    { "-1556218354", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/530.jpg", true, "7529404b-1ca8-4279-a1fc-01f7eb4174d5", "Employee", "Richard.McGlynn@yahoo.com", true, "Lawrence Ziemann", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "368-328-9441 x39977", false, 3, 79453m, "35bdd05c-52e4-4045-8681-42abbc87fa13", false, 0, "Merlin.Osinski" },
                    { "-1556712821", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/951.jpg", true, "bde0ad31-0d89-4548-8fa1-1bf011d3c816", "Employee", "Elouise.Kshlerin@hotmail.com", true, "Miller Wilkinson", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "624-957-1932 x01194", false, 3, 47325m, "57fc7f8c-eec0-46ae-9498-ef52929a7ee0", false, 0, "Theresa.Bartoletti" },
                    { "-1572370794", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/911.jpg", true, "fcf2d671-02bb-464a-b378-79ee75b70afa", "Employee", "Kathryn.Pacocha73@gmail.com", true, "Marjorie Sauer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "800.488.4218", false, 3, 53947m, "92cf7599-d422-4d1b-aeb4-54addb94254c", false, 1, "Brennon.Monahan88" },
                    { "-1583939571", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/105.jpg", true, "729b79fb-0660-4974-8191-3975b753ea42", "Employee", "Rupert.Hermann5@yahoo.com", true, "Alvera Mills", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(591) 515-8994 x2421", false, 3, 55100m, "507c5268-fb70-4b7f-8b51-d2306b0a0134", false, 0, "Marjorie69" },
                    { "-1605068460", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/652.jpg", true, "e44f4b4a-b5dd-4c46-879f-39faf6cc7337", "Employee", "Gerardo_Satterfield97@hotmail.com", true, "Adela DuBuque", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "307-969-4001", false, 3, 85074m, "df5ee9ad-7bb0-426e-abbe-848386173f9d", false, 1, "Alejandra31" },
                    { "-1733199110", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/159.jpg", true, "3062f40a-9df1-42c9-b520-5e08950a611d", "Employee", "Cole_Okuneva@hotmail.com", true, "Alexander Schneider", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-892-399-6145 x753", false, 3, 33170m, "0ffed20c-7791-47cf-959e-bb0f9721390b", false, 3, "Justice68" },
                    { "-1791425608", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/366.jpg", true, "5b9e2a66-2beb-4631-a14e-65d49bcad20e", "Employee", "Gerry44@gmail.com", true, "Vernie Dicki", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(600) 507-6469", false, 3, 79483m, "76ea199e-e54d-4d8c-ad5e-9cd8f9adf03a", false, 3, "Jeffery51" },
                    { "-1840598733", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/415.jpg", true, "8fc4bdb1-d209-4b93-951a-02a54736a301", "Employee", "Jasen_McClure77@hotmail.com", true, "Helen Osinski", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(420) 553-2414 x3905", false, 3, 36941m, "3e0d8c99-4ba2-4695-a2c0-917d98c43d93", false, 2, "Ryan_Hodkiewicz41" },
                    { "-2022590128", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1045.jpg", true, "1b59eaca-ec25-4ed4-80ac-875ed0f99c01", "Employee", "Norwood_Johns@hotmail.com", true, "Norbert Doyle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "716-566-8231", false, 3, 35409m, "b0794b3c-6343-4e27-88e3-a2c6dc636fdc", false, 2, "Elias_Renner" },
                    { "-30157812", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/662.jpg", true, "1568b0d8-3f64-43f9-bc9f-e2c547db909c", "Employee", "Darrell44@gmail.com", true, "Coby Vandervort", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "906-416-5642", false, 3, 56727m, "9ab58ef0-bb6c-400f-a92f-774dd33e3fdb", false, 3, "Anne1" },
                    { "-519073023", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/343.jpg", true, "97333606-e47e-4ad4-8c62-4de7cede4518", "Employee", "June.Brown@gmail.com", true, "Emie Schowalter", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-400-397-4849", false, 3, 85753m, "ddf19a00-dcf0-42c8-aa40-b226f9309736", false, 0, "Jamison.Kessler22" },
                    { "-549051341", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1151.jpg", true, "e81fa2e9-4fd3-401b-8cdc-c8cace78bc85", "Employee", "Samara42@gmail.com", true, "Ellis Treutel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "576-658-9974 x1364", false, 3, 44377m, "3898c3f0-2e3e-43f0-8e38-3af4e0b2adfe", false, 0, "Adela.Goldner" },
                    { "-552809776", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/304.jpg", true, "b6a4f745-9fb8-410e-b283-862cad0af82d", "Employee", "Alessandro_Rutherford42@hotmail.com", true, "Bridie Dibbert", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "812-329-0224 x8716", false, 3, 38658m, "b07bee03-41c7-41ff-a053-db8b8d06584c", false, 2, "Bernadine.Braun5" },
                    { "-609167778", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/418.jpg", true, "73095c21-8ce0-4583-8e90-b8711f1c5646", "Employee", "Robbie66@hotmail.com", true, "Aliya Witting", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "872.200.3987 x315", false, 3, 48568m, "0a058870-74a0-4eab-8fe0-d73488285a0b", false, 1, "Vergie.Weimann48" },
                    { "-666694766", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/485.jpg", true, "5be4a987-c13f-471f-b582-c686dece834c", "Employee", "Berniece.Rempel24@hotmail.com", true, "Ariel Wisoky", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "743.857.8529 x812", false, 3, 85231m, "9dbd47ed-f74f-4769-9fd8-266b498c22db", false, 0, "Earline13" },
                    { "-687822296", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/900.jpg", true, "52225dcc-0f1d-4d7b-ae25-84d45685ee41", "Employee", "Tobin.Wiza@hotmail.com", true, "Clement Balistreri", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "347-663-5599 x829", false, 3, 65941m, "55ceee0b-7639-4743-9cf7-88168a79aff5", false, 0, "Sibyl98" },
                    { "-8925989", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/202.jpg", true, "dface7ac-474b-48ce-b514-71b6ba38b1ab", "Employee", "Austin.Koss53@yahoo.com", true, "Ansley Hansen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-489-543-9729 x778", false, 3, 83617m, "ed9c9fd1-9a55-4fca-b356-1e15793748df", false, 2, "Karlie_Cremin" },
                    { "-894282017", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/514.jpg", true, "0347e9f8-05eb-433d-b812-0624ec122445", "Employee", "Gretchen.Hagenes4@yahoo.com", true, "Matteo Schulist", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(268) 472-2301 x1395", false, 3, 93358m, "782e6f32-2329-46e0-a6e0-c1a834a735d8", false, 2, "Dorothy_Pfeffer90" },
                    { "-989830954", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/852.jpg", true, "b207e01c-9d12-41f4-9739-302509904dbc", "Employee", "Esmeralda.Lehner0@hotmail.com", true, "Mabel Hane", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(646) 262-6373 x15249", false, 3, 56739m, "b648c30f-7afb-4692-8e7b-1be6c9627cf6", false, 1, "Dion.Carter95" },
                    { "1116895551", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/947.jpg", true, "c8ada07d-7439-4dcb-88d5-2c306c8b909e", "Employee", "Casimer.Streich@yahoo.com", true, "Zackary Schmidt", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(425) 562-0860", false, 3, 95770m, "1113e2ee-cabf-4b10-89eb-8e9b9ddab034", false, 0, "Elvie_Donnelly53" },
                    { "112133638", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/839.jpg", true, "244ba56d-dea9-431c-a54b-7e05a5fef751", "Employee", "Sam66@hotmail.com", true, "Xander Stark", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-627-419-4121", false, 3, 99655m, "c897319c-2afb-4187-b56c-bfd39ddef271", false, 2, "Tyler86" },
                    { "1151347690", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1177.jpg", true, "dd24b7a7-9ba0-430e-8cdf-18ec32849182", "Employee", "Lavina_Morar@yahoo.com", true, "Rey Waters", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "224-959-8132 x6878", false, 3, 85651m, "8336f1ed-5991-4072-b4ff-11e7548b2af7", false, 1, "Jon89" },
                    { "1160810117", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/132.jpg", true, "fee9a66b-8867-4691-a7e1-b1f098871100", "Employee", "Elfrieda_Tillman@hotmail.com", true, "Constantin Labadie", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "332.847.2436 x68629", false, 3, 46454m, "a3825e42-ef7c-4cc5-b6cc-38fc545b0c60", false, 3, "Madelyn.Johnston26" },
                    { "1169615930", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/23.jpg", true, "77d3f377-77dc-4dec-a982-18645e71a7b3", "Employee", "Lester_Wunsch92@yahoo.com", true, "Mittie Grant", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(352) 259-9836 x738", false, 3, 64444m, "cf50800c-4213-4112-ba23-ae1b4a0a712f", false, 3, "Darrick_Mraz86" },
                    { "1421866959", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/519.jpg", true, "c99a4d6d-5629-40fd-b877-c2ee060293dd", "Employee", "Ezra.Gutkowski@gmail.com", true, "Ernestine Christiansen", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "539-899-3095", false, 3, 91036m, "53a473ea-265b-4d19-860a-bec527922874", false, 2, "Lamar13" },
                    { "1654946659", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/765.jpg", true, "fcaef1f7-d307-479e-ba5a-65c306156a0c", "Employee", "Ramon.Friesen22@gmail.com", true, "Alan Zboncak", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-649-924-6505", false, 3, 59607m, "80455730-465b-4044-ac28-4f80823243bf", false, 3, "Kyle_Wisoky" },
                    { "1719016834", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/672.jpg", true, "2dafb1ae-91ed-4db4-837d-532b5269f60b", "Employee", "Lavern22@yahoo.com", true, "Ardella Schultz", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-240-968-9382 x3947", false, 3, 62691m, "a8903ca2-6db0-4951-8604-1ddac8fbcded", false, 3, "Geovanni.Hoeger" },
                    { "1746114836", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1157.jpg", true, "3bb8b26d-245a-4e35-974c-db9b5cb186e3", "Employee", "Jaylon48@gmail.com", true, "Michelle Langosh", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-377-965-6144", false, 3, 67545m, "da9c7b32-843b-49ac-ae9f-8cc2087e5f68", false, 3, "Louvenia.Halvorson84" },
                    { "1901365618", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/13.jpg", true, "c0b7b494-3a18-4152-a413-29778c1d8110", "Employee", "Donald_Romaguera@gmail.com", true, "Brain Tillman", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(402) 499-1437", false, 3, 91622m, "d8dc48b4-a0ae-499b-b658-2ad6f0caa0fe", false, 2, "Zaria_Cruickshank" },
                    { "192774192", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/922.jpg", true, "3480ba3d-f69f-4253-9e57-6757a3e89f12", "Employee", "Tiana.Jacobson35@yahoo.com", true, "Samir Walker", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "606.563.4660", false, 3, 57748m, "633499ff-dc7e-4216-84d0-2f22bc181475", false, 0, "Krista85" },
                    { "199212329", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1172.jpg", true, "d443e812-f5bf-4f66-b316-3c5b064201d3", "Employee", "Wiley_Cummerata74@yahoo.com", true, "Jeanie Bashirian", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "258.236.5869 x38499", false, 3, 33691m, "933663d7-2202-42a0-a231-ed76c83ee3cc", false, 0, "Lera79" },
                    { "1996961046", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/468.jpg", true, "f4dad82b-c7c4-4810-802f-1e6a19bf13e9", "Employee", "Hertha_Torp14@hotmail.com", true, "Velma Swaniawski", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "248.533.5750 x2645", false, 3, 52072m, "ed8db82b-4ec9-4b4e-a4ad-cfc90bb9fc85", false, 3, "Alanis31" },
                    { "2018019028", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/499.jpg", true, "6db286d1-eae2-49ca-a40e-811545f25cbb", "Employee", "Ray_Gutkowski44@hotmail.com", true, "Maud Durgan", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "827.682.9105", false, 3, 45593m, "cbf625de-ccc9-4d84-b1f0-c2ece7e7016c", false, 1, "Hilario10" },
                    { "2019466986", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/383.jpg", true, "e325fd02-688f-4925-9a44-7673451736e3", "Employee", "Preston_Rogahn98@hotmail.com", true, "Marcellus Langworth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-443-963-0127 x1206", false, 3, 38738m, "ab54d76c-2abb-475c-a911-c289c59a88e6", false, 3, "Deja.Thiel" },
                    { "249140173", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/880.jpg", true, "fdd683b4-ca89-4707-9116-f1073e5fc31a", "Employee", "Earlene.Hackett@gmail.com", true, "Kacie Block", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(509) 689-5353 x188", false, 3, 50570m, "64173ecd-98f8-48cc-9f2f-2d7efba9dff5", false, 0, "Alice91" },
                    { "285144790", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/398.jpg", true, "204b5c45-2dd0-4189-9b85-e9ff637f1de6", "Employee", "Armani.Pacocha@yahoo.com", true, "Gregg O'Reilly", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-480-884-3129", false, 3, 61953m, "8ebe40dd-cb66-4282-8594-a1ba74955c9a", false, 0, "Lilyan_Kessler48" },
                    { "343730503", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/215.jpg", true, "1aa5bf8e-520d-49fb-9f61-f7ade70f1241", "Employee", "Mohammed.Champlin35@gmail.com", true, "Sharon Little", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "495-522-6579", false, 3, 78385m, "bec47097-a8a8-4287-9842-1f44360f1bf0", false, 0, "Kathryn_Rau23" },
                    { "494582182", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/123.jpg", true, "af113a88-87a2-4214-b0a9-1354730cf43c", "Employee", "Berenice_Labadie@hotmail.com", true, "Reina Lemke", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-344-843-3582", false, 3, 46086m, "5687fc41-d524-46b1-8a5b-027d423294ba", false, 1, "Rigoberto40" },
                    { "559459875", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/649.jpg", true, "98bf9900-f539-4102-acd5-6539818063a3", "Employee", "Kyleigh37@hotmail.com", true, "Rafael Weber", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "201-854-7729 x41420", false, 3, 52106m, "5d605882-1b90-4fc2-b62d-d54f6c4ba439", false, 3, "Meta42" },
                    { "661376552", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/865.jpg", true, "3c5fab14-2278-4998-aa3c-94d7498d145e", "Employee", "Kieran_Sporer70@gmail.com", true, "Dave Kling", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "954-914-4447", false, 3, 68744m, "c6208f7a-363a-4490-bcc1-f8a92d23faac", false, 2, "Rocio_Murray" },
                    { "717878175", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/568.jpg", true, "da151093-facf-46cb-8524-e34242e8adb2", "Employee", "Isabella53@hotmail.com", true, "Anibal Koss", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-459-459-0795", false, 3, 33936m, "aa5e3ef1-5197-4a6e-8266-140fa58d43e0", false, 2, "Veda.Schamberger74" },
                    { "78815829", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/18.jpg", true, "9a2dfd5c-5e74-41d2-a68a-1577e8b3789f", "Employee", "Natalie48@yahoo.com", true, "Jairo Gutkowski", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "(707) 728-0770 x58508", false, 3, 43836m, "c222658f-bfcd-4910-bd2e-55ba6fb54bc8", false, 0, "Aiyana12" },
                    { "788708289", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/736.jpg", true, "9bf4c740-d838-465f-8d7b-c1c7bb9d1289", "Employee", "Berneice73@yahoo.com", true, "Talia Langworth", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "269.401.3725", false, 3, 97812m, "fe470d0d-b485-4fee-875a-e5f6aad8d95a", false, 1, "Korbin63" },
                    { "81077800", 0, true, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/503.jpg", true, "b4283721-2bf2-4211-8108-1c2d6d8ee14e", "Employee", "Felton.Bartoletti@gmail.com", true, "Eric Gislason", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, null, null, null, "1-657-591-4297 x643", false, 3, 85782m, "6f998fb3-9e22-4def-8c07-74f386a35016", false, 1, "Triston_Robel" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
