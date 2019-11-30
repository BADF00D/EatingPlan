using System.Collections.Generic;
using System.Threading.Tasks;
using Eatingplan;
using Eatingplan.Api.V1;
using Eatingplan.Model;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace EatingplanTests.Api.V1
{
    public abstract class RecipeControllerSpec   :Spec {
        protected RecipeController _sut = new RecipeController(RecipeContext);
        protected static readonly IRecipeContext RecipeContext = A.Fake<IRecipeContext>();

        public RecipeControllerSpec()
        {
        }
    }

    [TestFixture]
    public class If_GetAll_is_called : RecipeControllerSpec
    {
        private ActionResult<List<Recipe>> _result;
        private List<Recipe> _fakeRecipesItems;

        protected override void EstablishContext()
        {
            _fakeRecipesItems = new List<Recipe> {new Recipe()};
            A.CallTo(() => RecipeContext.ToListAsync())
                .Returns(_fakeRecipesItems);
        }

        protected override async Task BecauseOfAsync()
        {
            _result = await _sut.GetAll();
        }

        [Test]
        public void Should_all_Recipes_Items_be_Returned()
        {
            _result.Value.Should().ContainInOrder(_fakeRecipesItems);
        }
    }
}
