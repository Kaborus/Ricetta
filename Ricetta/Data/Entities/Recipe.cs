using System.ComponentModel.DataAnnotations;

namespace Ricetta.Data.Entities
{
    public class Recipe : Entity
    {
        public string? MemberId { get; set; }
        //public Member? Member { get; set; }
        //public string MemberSavedId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public ICollection<Ingredient> Ingredients { get; set; }
        [MaxLength(2000)]
        public ICollection<PreparationStep> PreparationSteps { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
