using Food_Ordering_Application.Models;

namespace Food_Ordering_Application.Services
{
    public interface IMenuItemServices
    {
        public Task<IEnumerable<MenuItem>> GetMenuItems();
        public void AddMenuItem(MenuItem menuItem);
        public void RemoveMenuItem(MenuItem menuItem);

    }
}
