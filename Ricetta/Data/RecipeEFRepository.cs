using Microsoft.EntityFrameworkCore;
using Ricetta.Data.Entities;
using Ricetta.Models;

namespace Ricetta.Data
{
    public class RecipeEFRepository : IRecipesRepository
    {
        private readonly RicettaDbContext _context;

        public RecipeEFRepository(RicettaDbContext context)
        {
            _context = context;
        }

        public async Task Create(Recipe newRecipe)
        {
            await _context.AddAsync(newRecipe);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Recipe recipe)
        {

            _context.Recipes.Remove(recipe);

            await _context.SaveChangesAsync();
        }

        public async Task Edit(Recipe recipe)
        {
            _context.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<RecipeListItemViewModel>> GetAll()
        {
            ICollection<RecipeListItemViewModel> recipes = await _context.Recipes.Select(l => new RecipeListItemViewModel() { RecipeId = l.Id, Name = l.Name, Category = l.Category.Name }).ToListAsync();
            return recipes;
        }

        public async Task<ICollection<RecipeListItemViewModel>> GetAll(string userId)
        {
            ICollection<RecipeListItemViewModel> recipes = await _context.Recipes.Where(r => r.MemberId == userId).Select(l => new RecipeListItemViewModel() { RecipeId = l.Id, Name = l.Name, Category = l.Category.Name }).ToListAsync();
            return recipes;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }

        public async Task<ICollection<RecipeListItemViewModel>> GetAllSaved(string userId)
        {
            // Haal alle opgeslagen recepten op voor de gegeven gebruiker
            ICollection<SavedRecipe> savedRecipes = await _context.SavedRecipes
                .Where(sr => sr.UserId == userId)
                .ToListAsync();

            // Initialiseer de lijst van RecipeListItemViewModel
            ICollection<RecipeListItemViewModel> recipes = new List<RecipeListItemViewModel>();

            // Loop door de opgeslagen recepten en converteer ze naar RecipeListItemViewModel
            foreach (var savedRecipe in savedRecipes)
            {
                // Haal het bijbehorende recept op
                var recipe = await _context.Recipes.FindAsync(savedRecipe.RecipeId);
                if (recipe != null)
                {
                    // Maak een nieuw RecipeListItemViewModel en voeg het toe aan de collectie
                    var recipeViewModel = new RecipeListItemViewModel
                    {
                        // Vul de eigenschappen van de RecipeListItemViewModel in met de gegevens van het recept
                        RecipeId = recipe.Id,
                        Name = recipe.Name,
                        // Voeg eventuele andere eigenschappen toe die je nodig hebt
                    };
                    recipes.Add(recipeViewModel);
                }
            }

            // Retourneer de lijst van RecipeListItemViewModel
            return recipes;
        }


        public async Task<Recipe> GetById(int? id)
        {
            var recipe = await _context.Recipes.Include(r => r.Category).
                Include(r => r.Ingredients).
                Include(r => r.PreparationSteps).
                FirstOrDefaultAsync(m => m.Id == id);
            var userId = recipe?.MemberId;

            return recipe;
        }

        public async Task Save(SavedRecipe recipe)
        {
            await _context.SavedRecipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            
        }

        public Task<ICollection<Recipe>> Search(string criteria)
        {
            return (Task<ICollection<Recipe>>)_context.Recipes.Where(r => r.Name.Contains(criteria));
        }


    }


}
