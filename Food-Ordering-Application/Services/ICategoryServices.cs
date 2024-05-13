using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface ICategoryServices
    {
        public Task<IEnumerable<Category>> GetAllCategories();
    }
}
