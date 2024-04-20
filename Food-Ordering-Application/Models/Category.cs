using System.Text.Json.Serialization;

namespace Food_Ordering_Application.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUrl {  get; set; }

        // Add other category properties as needed
        [JsonIgnore]
        public ICollection<MenuItem>? MenuItems { get; set; }

    }
}
