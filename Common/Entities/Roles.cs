namespace Ivoluntia.BackgroudServices.Common.Entities
{
    public class Role : IdentityRole
    {
        public bool AllowedForFoundation { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; }
        public bool IsDeprecated { get; set; }
    }
}
