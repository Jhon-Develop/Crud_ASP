using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudWeb.database;

namespace CrudWeb.Pages
{
    public class CompletedTasksModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CompletedTasksModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> CompletedItems { get; set; }

        public async Task OnGetAsync()
        {
            CompletedItems = await _context.Items
                .Where(item => item.IsCompleted)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            var completedItems = await _context.Items
                .Where(item => item.IsCompleted)
                .ToListAsync();

            _context.Items.RemoveRange(completedItems);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
