namespace Ricetta.Data.Entities
{
    public class Recipe : Entity
    {
        public string? MemberId { get; set; }
        //public Member? Member { get; set; }
        //public string MemberSavedId { get; set; }
        public string Name { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<PreparationStep> PreparationSteps { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
