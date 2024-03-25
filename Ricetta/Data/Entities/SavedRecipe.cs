namespace Ricetta.Data.Entities
{
    public class SavedRecipe : Entity
    {
        public string UserId { get; set; }
        public int RecipeId { get; set; }

    }
}
