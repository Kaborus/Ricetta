using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Ricetta.Controllers;
using Ricetta.Data;
using Ricetta.Data.Entities;
using Ricetta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ricetta_Test
{
    public class RecipesControllerTests
    {
        private RecipesController _controller;
        private Mock<IRecipesRepository> _repositoryMock;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRecipesRepository>();
            _controller = new RecipesController(_repositoryMock.Object, null, null, null);
        }

        [Test]
        public async Task Create_ValidRecipe_ReturnsRedirectToActionResult()
        {
            // Arrange
            var recipe = new Recipe { Name = "Test Recipe", CategoryId = 1 };
            _repositoryMock.Setup(repo => repo.Create(recipe)).Returns(Task.CompletedTask);
            
            // Act
            var result = await _controller.Create(recipe);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }
        
        [Test]
        public async Task Edit_ValidRecipe_ReturnsRedirectToActionResult()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Name = "Updated Recipe", CategoryId = 2 };
            _repositoryMock.Setup(repo => repo.Edit(recipe)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Edit(1, recipe);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public async Task Delete_ValidRecipeId_ReturnsRedirectToActionResult()
        {
            // Arrange
            int recipeId = 1;
            var recipe = new Recipe { Id = recipeId, Name = "Test Recipe", CategoryId = 1 };
            _repositoryMock.Setup(repo => repo.GetById(recipeId)).ReturnsAsync(recipe);
            _repositoryMock.Setup(repo => repo.Delete(recipe)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(recipeId);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
        }

        [Test]
        public async Task Details_ExistingRecipeId_ReturnsViewResultWithRecipeModel()
        {
            // Arrange
            int recipeId = 1;
            var recipe = new Recipe { Id = recipeId, Name = "Test Recipe", CategoryId = 1 };
            _repositoryMock.Setup(repo => repo.GetById(recipeId)).ReturnsAsync(recipe);

            // Act
            var result = await _controller.Details(recipeId);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsInstanceOf<Recipe>(viewResult.Model);
            Assert.AreEqual(recipe, viewResult.Model);
        }
    }
}
