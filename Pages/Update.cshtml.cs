using CrudWeb.database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CrudWeb.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UpdateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Item TaskToUpdate { get; set; }

        public IList<Item> Tasks { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                // Excluir tareas completadas
                Tasks = await _context.Items
                                      .Where(t => !t.IsCompleted) // Solo las no completadas
                                      .ToListAsync();
            }
            else
            {
                // Buscar la tarea por ID
                TaskToUpdate = await _context.Items
                                             .Where(t => !t.IsCompleted && t.Id == id) // Excluir completadas
                                             .FirstOrDefaultAsync();

                if (TaskToUpdate == null)
                {
                    return NotFound();
                }

                // Solo mostrar la tarea específica si se proporciona un ID
                Tasks = new List<Item> { TaskToUpdate };
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Buscar la tarea por ID
            var taskToUpdate = await _context.Items.FindAsync(id);

            if (taskToUpdate == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades manualmente
            taskToUpdate.Name = TaskToUpdate.Name;
            taskToUpdate.Description = TaskToUpdate.Description;

            // Asegúrate de que el contexto esté rastreando la entidad
            _context.Attach(taskToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Actualización manual exitosa en la base de datos.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar en la base de datos: {ex.Message}");
            }

            return RedirectToPage("./Read");
        }
    }
}
