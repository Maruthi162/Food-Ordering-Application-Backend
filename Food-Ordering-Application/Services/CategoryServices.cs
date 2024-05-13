using Food_Ordering_Application.DTO_s;
using Food_Ordering_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Services
{
    public class CategoryServices:ICategoryServices
    {
        private readonly FlashFoodsContext _context;
        public CategoryServices(FlashFoodsContext context)
        {
            _context = context;
        }
        public async  Task<IEnumerable<Category>> GetAllCategories()
        {
            var res = await _context.Categories.ToListAsync();
            return res;
        }
    }
}
