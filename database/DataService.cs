using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudWeb.database;

public class DataService : IDataService
{
    private readonly ApplicationDbContext _context;

    // Constructor que recibe el contexto de la base de datos
    public DataService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Método para obtener todos los elementos de la base de datos
    public async Task<List<Item>> GetAllItemsAsync()
    {
        try
        {
            // Utiliza el contexto para obtener la lista de items
            return await _context.Items.ToListAsync();
        }
        catch (Exception ex)
        {
            // Manejo de excepciones: puedes registrar el error o lanzar una excepción personalizada
            throw new Exception("Error al obtener los items", ex);
        }
    }
}
