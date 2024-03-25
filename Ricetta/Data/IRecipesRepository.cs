using Ricetta.Data.Entities;
using Ricetta.Models;

namespace Ricetta.Data
{
    public interface IRecipesRepository
    {
        Task<ICollection<RecipeListItemViewModel>> GetAll();
        Task<ICollection<RecipeListItemViewModel>> GetAll(string userId);
        Task<ICollection<RecipeListItemViewModel>> GetAllSaved(string userId);

        Task<ICollection<Recipe>> Search(string criteria);

        Task<Recipe> GetById(int? id);

        Task Create(Recipe newRecipe);
        Task Edit(Recipe recipe);
        Task Delete(Recipe recipe);
        Task Save(SavedRecipe recipe);
        IEnumerable<Category> GetAllCategories();
    }
}
