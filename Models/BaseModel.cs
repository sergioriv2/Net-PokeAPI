using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public abstract class BaseModel
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public BaseModel()
        {
            this.Id = Guid.NewGuid() .ToString();
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.DeletedAt = null;
        }
    }
}
