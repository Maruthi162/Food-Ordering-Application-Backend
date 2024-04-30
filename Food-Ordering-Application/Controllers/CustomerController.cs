using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Food_Ordering_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlashFoodsContext _context;
        private readonly IMenuItemServices _menuItemServices;
        private readonly IRestaurantRepo _restaurantRepo;

        public CustomerController(FlashFoodsContext context, IMenuItemServices menuItemServices, IRestaurantRepo restaurantRepo)
        {
            _context = context;
            _menuItemServices = menuItemServices;
            _restaurantRepo = restaurantRepo;
        }

        [HttpGet]
        [Route("menu items")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItems()
        {
            var items = await _menuItemServices.GetMenuItems();
            if(items == null)
            {
                return BadRequest();
            }
            return Ok(items);
        }

        [Authorize]
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
        //get all the restaurants list, display it, after clicking on restaurant name, we should call getcategory by restId
        //
        [HttpGet]
        [Route("Get all restaurants")]
        public async Task<ActionResult> GetRestaurants()
        {
            var rests = await _restaurantRepo.GetAllRestaurants();
            return Ok(rests);
        }
        //getting categpries for particular restaurant
        [Authorize]
        [HttpGet]
        [Route("get Category by rest")]
        public async Task<ActionResult> GetCategoriesOfRest(int restId)
        {
            var categories = await _restaurantRepo.GetCategoriesByRest(restId);
            return Ok(categories);
                
        }

        //get items under particular restaurant and category
        [HttpGet]
        [Route("Get Items by rest and category")]
        public async Task<ActionResult> GetMenuItemsForRestAndCat(int catId, int restId)
        {
            var items=await _restaurantRepo.GetMenuItemsByRestAndCat(catId, restId);

            return Ok(items);
        }
        
    }
}
