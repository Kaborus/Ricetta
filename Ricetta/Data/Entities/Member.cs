using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ricetta.Data.Entities
{
    public class Member : IdentityUser
    {
        [MaxLength(100)]
        public string Tagname { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
        public ICollection<SavedRecipe>? SavedRecipes { get; set; }
    }
}
