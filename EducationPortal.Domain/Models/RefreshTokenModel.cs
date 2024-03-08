namespace EducationPortal.Domain.Models
{
    public class RefreshTokenModel : DataBaseEntity
    {
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set;}
        public UserModel User { get; set; }
    }
}
