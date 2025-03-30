using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Photos")]
    public class PhotoModel
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        //Navigation Property 
        [Column("UserId")]
        public Guid UserModelId { get; set; }
        public UserModel UserModel { get; set; } = null!;
    }
}