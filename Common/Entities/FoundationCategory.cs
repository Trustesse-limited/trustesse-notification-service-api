namespace Ivoluntia.BackgroudServices.Common.Entities
{
    public class FoundationCategory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Foundation> Foundations { get; set; }
    }
}
