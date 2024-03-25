using System.ComponentModel.DataAnnotations.Schema;

namespace Ricetta.Data.Entities
{
    public class Ingredient : Entity
    {
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
