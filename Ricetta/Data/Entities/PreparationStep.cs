using System.ComponentModel.DataAnnotations.Schema;

namespace Ricetta.Data.Entities
{
    public class PreparationStep : Entity
    {
        public string Step {  get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
