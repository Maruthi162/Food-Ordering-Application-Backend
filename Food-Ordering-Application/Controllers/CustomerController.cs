using Food_Ordering_Application.Models;
using Food_Ordering_Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
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
        private readonly ICategoryServices _categoryServices;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(FlashFoodsContext context, IMenuItemServices menuItemServices, IRestaurantRepo restaurantRepo, ICategoryServices categoryServices, ILogger<CustomerController> logger)
        {
            _context = context;
            _menuItemServices = menuItemServices;
            _restaurantRepo = restaurantRepo;
            _categoryServices = categoryServices;
            _logger = logger;
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
        //[Authorize]  if we use authorize we need to pass authentication credentials for every endpoint call in angular, we will implement this later
        [HttpGet]
        [Route("get Category by rest")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesOfRest(int restId)
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

        [HttpGet]
        [Route("Get User Favorite restaurants")]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetUserFavRestaurants(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("UserId is required.");
            }

            try
            {
                var favRests = await _restaurantRepo.GetUserFavRestaurants(userId);

                if (favRests == null || !favRests.Any())
                {
                    return NotFound("No favorite restaurants found for the user.");
                }

                return Ok(favRests);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing the request for user {UserId}", userId);
                // Return a generic error message
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("AddRestaurantToFavorite")]
        public async Task<ActionResult> AddRestTozUserFav(string userId, int restId)
        {
            string msg=await _restaurantRepo.AddFavRestaurant(userId, restId);

            return Ok(new { message = msg });
        }

        [HttpDelete]
        [Route("Remove Restaurant from favourite")]
        public async Task<ActionResult> DeleteRestaurantFromFavourite(int restId, string userId)
        {
            await _restaurantRepo.RemoveFromFavourites(restId, userId);
            return Ok(new { message = "Restaurant Removed from Favourites" });
        }

        [HttpGet]
        [Route("Get-Fav-Items")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetUserFavMenuItems(string userId)
        {
            var favs=await _menuItemServices.GetUserFavMenuItems(userId);
            return Ok(favs);
        }

        [HttpPost]
        [Route("add-to-favMenu")]
        public async Task<ActionResult> AddtoFavMenuItems(string userId,int menuId)
        {
            string msg=await _menuItemServices.AddFavMenuItem(userId, menuId);
            return Ok(new { message = msg });
        }

        [HttpDelete]
        [Route("Remove-favItem")]
        public async Task<ActionResult> RemoveFromFavitems(int menuId, string userId)
        {
            await _menuItemServices.RemoveFromFavouriteMenu(menuId, userId);
            return Ok(new { msg="successfuly Removed from fav" });
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            var cats= await _categoryServices.GetAllCategories();
            return Ok(cats);
        }
        [HttpGet]
        [Route("GetMenuItemsByCategoryId")]
        public async Task<ActionResult<IEnumerable<MenuItem>>>  GetMenuItemsByCategoryId(int categoryId)
        {
            var res= await _menuItemServices.GetMenuItemsByCategoryId(categoryId);
            return Ok(res);
        }      
    }
}
