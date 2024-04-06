namespace Ricetta.Data.Entities
{
    public class Notification : Entity
    {
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
