using Microsoft.AspNetCore.Identity;

namespace Ricetta.Data.Entities
{
    public class Member : IdentityUser
    {
        public string Tagname { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<SavedRecipe>? SavedRecipes { get; set; }
    }
}
