using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudWeb.database;

namespace CrudWeb.Pages;

public class CreateModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public CreateModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Item? Item { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Item == null)
        {
            // Manejo del caso en que Item es nulo
            return BadRequest("Item no puede ser nulo.");
        }

        _context.Items.Add(Item);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Read");
    }
}
