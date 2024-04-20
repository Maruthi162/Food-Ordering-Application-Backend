using Food_Ordering_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Services
{
    public class MenuItemServices:IMenuItemServices
    {
        private readonly FlashFoodsContext _context;
        public MenuItemServices(FlashFoodsContext  context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuItem>> GetMenuItems()
        {
           return await _context.MenuItems.Include(m=>m.Category).Include(m=>m.Restaurant).ToListAsync();
        }
        public void AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        public void RemoveMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Remove(menuItem);
            _context.SaveChanges();
        }
    }
}
