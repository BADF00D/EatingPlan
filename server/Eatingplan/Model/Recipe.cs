namespace Eatingplan.Model
{
    /// <summary>
    /// Recipe
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Internal id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Title of recipe
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public RecipePart[] RecipeParts { get; set; }
    }


    public class RecipePart
    {
        public long Id { get; set; }
        public long Name { get; set; }
        public int? Order { get; set; }
        public Instruction[] Instructions { get; set; }
    }

    public class Instruction
    {
        public long Id { get; set; }
    }

    public class IngredientInRecipePart
    {
        public RecipePart  RecipePart { get; set; }
        public Ingredient Ingredient { get; set; }
        public float Amout { get; set; }
    }

    public class Ingredient
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
    }
}