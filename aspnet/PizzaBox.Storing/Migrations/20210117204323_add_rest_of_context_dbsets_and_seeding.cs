using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaBox.Storing.Migrations
{
    public partial class add_rest_of_context_dbsets_and_seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crusts",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crusts", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inches = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "Toppings",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Toppings", x => x.EntityId);
                });

            migrationBuilder.CreateTable(
                name: "APizzaModel",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CrustEntityId = table.Column<long>(type: "bigint", nullable: true),
                    SizeEntityId = table.Column<long>(type: "bigint", nullable: true),
                    OrderEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APizzaModel", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_APizzaModel_Crusts_CrustEntityId",
                        column: x => x.CrustEntityId,
                        principalTable: "Crusts",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APizzaModel_Orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "Orders",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APizzaModel_Sizes_SizeEntityId",
                        column: x => x.SizeEntityId,
                        principalTable: "Sizes",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PizzaToppings",
                columns: table => new
                {
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaEntityId = table.Column<long>(type: "bigint", nullable: true),
                    ToppingEntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaToppings", x => x.EntityId);
                    table.ForeignKey(
                        name: "FK_PizzaToppings_APizzaModel_PizzaEntityId",
                        column: x => x.PizzaEntityId,
                        principalTable: "APizzaModel",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PizzaToppings_Toppings_ToppingEntityId",
                        column: x => x.ToppingEntityId,
                        principalTable: "Toppings",
                        principalColumn: "EntityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Crusts",
                columns: new[] { "EntityId", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, "Thin", 0.99m },
                    { 2L, "Regular", 1.99m },
                    { 3L, "Large", 2.99m }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "EntityId", "Inches", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, 10, "Small", 0.99m },
                    { 2L, 12, "Medium", 2.99m },
                    { 3L, 14, "Large", 4.99m }
                });

            migrationBuilder.InsertData(
                table: "Toppings",
                columns: new[] { "EntityId", "Name" },
                values: new object[,]
                {
                    { 1L, "Cheese" },
                    { 2L, "Pepperoni" },
                    { 3L, "Sausage" },
                    { 4L, "Pineapple" },
                    { 5L, "Tomato Sauce" },
                    { 6L, "Shrimp" },
                    { 7L, "Crab" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_APizzaModel_CrustEntityId",
                table: "APizzaModel",
                column: "CrustEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_APizzaModel_OrderEntityId",
                table: "APizzaModel",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_APizzaModel_SizeEntityId",
                table: "APizzaModel",
                column: "SizeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaToppings_PizzaEntityId",
                table: "PizzaToppings",
                column: "PizzaEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaToppings_ToppingEntityId",
                table: "PizzaToppings",
                column: "ToppingEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaToppings");

            migrationBuilder.DropTable(
                name: "APizzaModel");

            migrationBuilder.DropTable(
                name: "Toppings");

            migrationBuilder.DropTable(
                name: "Crusts");

            migrationBuilder.DropTable(
                name: "Sizes");
        }
    }
}
