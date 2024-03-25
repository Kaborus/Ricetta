using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ricetta.Migrations
{
    /// <inheritdoc />
    public partial class saveddbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_SavedRecipe_SavedRecipeId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_PreparationSteps_SavedRecipe_SavedRecipeId",
                table: "PreparationSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedRecipe_AspNetUsers_MemberId",
                table: "SavedRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedRecipe_Categories_CategoryId",
                table: "SavedRecipe");

            migrationBuilder.DropIndex(
                name: "IX_PreparationSteps_SavedRecipeId",
                table: "PreparationSteps");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_SavedRecipeId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedRecipe",
                table: "SavedRecipe");

            migrationBuilder.DropIndex(
                name: "IX_SavedRecipe_CategoryId",
                table: "SavedRecipe");

            migrationBuilder.DropColumn(
                name: "SavedRecipeId",
                table: "PreparationSteps");

            migrationBuilder.DropColumn(
                name: "SavedRecipeId",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "SavedRecipe",
                newName: "SavedRecipes");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SavedRecipes",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "SavedRecipes",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedRecipe_MemberId",
                table: "SavedRecipes",
                newName: "IX_SavedRecipes_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedRecipes",
                table: "SavedRecipes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRecipes_AspNetUsers_MemberId",
                table: "SavedRecipes",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedRecipes_AspNetUsers_MemberId",
                table: "SavedRecipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedRecipes",
                table: "SavedRecipes");

            migrationBuilder.RenameTable(
                name: "SavedRecipes",
                newName: "SavedRecipe");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SavedRecipe",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "SavedRecipe",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedRecipes_MemberId",
                table: "SavedRecipe",
                newName: "IX_SavedRecipe_MemberId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedRecipe",
                table: "SavedRecipe",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRecipe_AspNetUsers_MemberId",
                table: "SavedRecipe",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRecipe_Categories_CategoryId",
                table: "SavedRecipe",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
