using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly FlashFoodsContext _context;
        private readonly IMenuItemServices _menuItemServices;

        public MenuItemController(FlashFoodsContext context, IMenuItemServices menuItemServices)
        {
            _context = context;
            _menuItemServices = menuItemServices;
        }

        [HttpGet]
        [Route("items")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            var items = await _menuItemServices.GetMenuItems();
            if(items == null)
            {
                return BadRequest();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("menuitems")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItemsWithCategoryAndRestaurant()
        {
            // Eager loading using Include method
            var menuItems = await _context.MenuItems
                .Include(mi => mi.Category)
                .Include(mi => mi.Restaurant)
                .ToListAsync();

            return Ok(menuItems);
        }

        [HttpPost]
        [Route("Add item")]
        public async Task<ActionResult> AddItem([FromBody] MenuItem menuItem)
        {
            var exists= await _context.MenuItems.FirstOrDefaultAsync(a=>a.MenuItemId==menuItem.MenuItemId);
            if (exists!=null) 
            {
                return BadRequest("Item already exists, just increse the quantity");
            }

            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
            return Ok("Item Added successfully");
        }
    }
}
