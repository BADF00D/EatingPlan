using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Eatingplan.Model;
using Microsoft.AspNetCore.Mvc;

namespace Eatingplan.Api.V1
{
    /// <summary>
    /// Controller for manipulation recipes.
    /// </summary>
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/v1/recipes")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeContext _context;

        /// <summary>
        /// Create a new controller for recipes
        /// </summary>
        /// <param name="context"></param>
        public RecipeController(IRecipeContext context)
        {
            _context = context;
            if (_context.Any() ) return;

            AddDummyData();
        }

        private void AddDummyData()
        {
            _context.Add(new Recipe {Name = "Items1"});
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets all recipes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Recipe>), 200)]
        public async Task<ActionResult<List<Recipe>>> GetAll()
        {
            return await _context.ToListAsync();
        }

        /// <summary>
        /// Gets single recipe by its Id.
        /// </summary>
        /// <param name="id">It from recipes to get.</param>
        /// <response code="200">Returns recipe.</response>
        /// <response code="404">Returns recipe with given Id not found.</response>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRecipe")]
        [ProducesResponseType(typeof(Recipe), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Recipe>> GetById([FromRoute]long id)
        {
            var item = await _context.FindAsync(id);
            return item ?? (ActionResult<Recipe>) NotFound();
        }

        /// <summary>
        /// Creates a recipe.
        /// </summary>
        /// <param name="item">recipes to create.</param>
        /// <response code="201">recipe created successfully.</response>
        /// <response code="400">Invalid or missing properties.</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody, Required]Recipe item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetRecipe", new {id = item.Id}, item);
        }

        /// <summary>
        /// Updates a recipe.
        /// </summary>
        /// <param name="id">Id of recipe to update.</param>
        /// <param name="item">Data of recipe.</param>
        /// <response code="204">recipes updated successfully.</response>
        /// <response code="400">Invalid or missing properties.</response>
        /// <response code="404">recipes with given Id not found.</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute]long id, [FromBody, Required]Recipe item)
        {
            var recipe = await _context.FindAsync(id);
            if (recipe == null) return NotFound();

            recipe.Name = item.Name;

            _context.Update(recipe);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete a recipe.
        /// </summary>
        /// <param name="id">Id of recipe to delete.</param>
        /// <response code="204">recipes deleted successfully.</response>
        /// <response code="404">recipes with given Id not found.</response>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute]long id)
        {
            var recipe = await _context.FindAsync(id);
            if (recipe == null) return NotFound();

            _context.Remove(recipe);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}