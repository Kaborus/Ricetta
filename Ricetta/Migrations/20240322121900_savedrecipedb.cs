using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ricetta.Migrations
{
    /// <inheritdoc />
    public partial class savedrecipedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SavedRecipeId",
                table: "PreparationSteps",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SavedRecipeId",
                table: "Ingredients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SavedRecipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MemberId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedRecipe_AspNetUsers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SavedRecipe_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreparationSteps_SavedRecipeId",
                table: "PreparationSteps",
                column: "SavedRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_SavedRecipeId",
                table: "Ingredients",
                column: "SavedRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipe_CategoryId",
                table: "SavedRecipe",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SavedRecipe_MemberId",
                table: "SavedRecipe",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_SavedRecipe_SavedRecipeId",
                table: "Ingredients",
                column: "SavedRecipeId",
                principalTable: "SavedRecipe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PreparationSteps_SavedRecipe_SavedRecipeId",
                table: "PreparationSteps",
                column: "SavedRecipeId",
                principalTable: "SavedRecipe",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_SavedRecipe_SavedRecipeId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_PreparationSteps_SavedRecipe_SavedRecipeId",
                table: "PreparationSteps");

            migrationBuilder.DropTable(
                name: "SavedRecipe");

            migrationBuilder.DropIndex(
                name: "IX_PreparationSteps_SavedRecipeId",
                table: "PreparationSteps");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_SavedRecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "SavedRecipeId",
                table: "PreparationSteps");

            migrationBuilder.DropColumn(
                name: "SavedRecipeId",
                table: "Ingredients");
        }
    }
}
