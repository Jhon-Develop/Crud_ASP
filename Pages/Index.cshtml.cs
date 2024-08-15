using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrudWeb.database;

public class IndexModel : PageModel
{
    private readonly IDataService _dataService;

    public IndexModel(IDataService dataService)
    {
        _dataService = dataService;
    }

    public List<Item> Items { get; set; }

    public async Task OnGetAsync()
    {
        Items = await _dataService.GetAllItemsAsync();
    }
}
