namespace Ricetta.Data.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Recipe>? Recipes { get; set; }
    }
}
