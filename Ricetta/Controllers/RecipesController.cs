using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Ricetta.Data;
using Ricetta.Data.Entities;
using Ricetta.Hubs;
using Ricetta.Migrations;
using Ricetta.Models;

namespace Ricetta.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly UserManager<Member> _userManager;
        private readonly SignInManager<Member> _signInManager;
        private readonly IHubContext<ChatHub> _hub;

        public RecipesController(IRecipesRepository recipesRepository, UserManager<Member> userManager, SignInManager<Member> signInManager, IHubContext<ChatHub> hub)
        {
            _recipesRepository = recipesRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _hub = hub;
        }

        public async Task<IActionResult> Profile(string? id)
        {
            Member member = await _userManager.FindByIdAsync(id);
            string tagname = member.Tagname;

            ViewData["TagName"] = tagname;
            return View(await _recipesRepository.GetAll(id));
        }

        // GET: Recipes
        public async Task<IActionResult> Index() => View(await _recipesRepository.GetAll(_userManager.GetUserId(this.User)));

        public async Task<IActionResult> AllRecipes()
        {
            return View(await _recipesRepository.GetAll());
        }

        public async Task<IActionResult> SavedRecipes(string userId)
        {
            userId = _userManager.GetUserId(this.User);

            return View(await _recipesRepository.GetAllSaved(userId));
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var recipe = await _recipesRepository.GetById(id);

            Member member = await _userManager.FindByIdAsync(recipe.MemberId);
            string tagName = member?.Tagname;



            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["TagName"] = tagName;

            return View(recipe);
        }

        // GET: Recipes/Createx
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_recipesRepository.GetAllCategories(), nameof(Category.Id), nameof(Category.Name));
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Ingredients,PreparationSteps,CategoryId,MemberId,Id")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                AddIngredients(recipe);
                AddSteps(recipe);

                string? userId = _userManager.GetUserId(this.User);
                recipe.MemberId = userId;


                await _recipesRepository.Create(recipe);

                
                await _hub.Clients.All.SendAsync("RecipeCreated", recipe.Name,"" /*_recipesRepository.GetCategory(recipe.CategoryId)*/);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            ViewData["CategoryId"] = new SelectList(_recipesRepository.GetAllCategories(), nameof(Category.Id), nameof(Category.Name), recipe.CategoryId);
            return View(recipe);
        }

        public void AddIngredients(Recipe recipe)
        {

            string ingredientsText = Request.Form["Ingredients"];

            string[] lines = ingredientsText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                recipe.Ingredients.Add(new Ingredient { Name = line.Trim() });
            }
        }

        public void AddSteps(Recipe recipe)
        {
            recipe.PreparationSteps.Clear();
            string stepsText = Request.Form["PreparationSteps"];

            string[] lines = stepsText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);


            foreach (string line in lines)
            {
                recipe.PreparationSteps.Add(new PreparationStep { Step = line.Trim() });
            }
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var recipe = await _context.Recipes.
                   FindAsync(id);*/

            var recipe = await _recipesRepository.GetById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_recipesRepository.GetAllCategories(), nameof(Category.Id), nameof(Category.Name), recipe.CategoryId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Ingredients,PreparationSteps,CategoryId,MemberId,Id")] Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    AddIngredients(recipe);
                    AddSteps(recipe);

                    recipe.MemberId = _userManager.GetUserId(this.User);

                    await _recipesRepository.Edit(recipe);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_recipesRepository.GetAllCategories(), "Id", "Id", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var recipe = await _context.Recipes.Include(r => r.Ingredients).Include(r => r.PreparationSteps)
                .FirstOrDefaultAsync(m => m.Id == id);*/
            var recipe = await _recipesRepository.GetById(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _recipesRepository.GetById(id);
            if (recipe != null)
            {
                await _recipesRepository.Delete(recipe);
            }


            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            //return _context.Recipes.Any(e => e.Id == id);
            return true;
        }

        public async Task<IActionResult> Save(int id)
        {
            var recipe = await _recipesRepository.GetById(id);
            var userId = _userManager.GetUserId(this.User);
            var savedRecipe = new SavedRecipe()
            {
                UserId = userId,
                RecipeId = id
            };

            await _recipesRepository.Save(savedRecipe);

            return RedirectToAction(nameof(Index));

        }
    }
}
