using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudWeb.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class ReadModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public ReadModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<Item> Items { get; set; }

    public async Task OnGetAsync()
    {
        Items = await _context.Items
            .Where(item => !item.IsCompleted)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostCompleteAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        item.IsCompleted = true;
        await _context.SaveChangesAsync();

        return RedirectToPage();
    }
}
