using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TutorialEU.Data;
using TutorialEU.Models;
using TutorialEU.Services;

namespace TutorialEU.Pages.Productos {
    public class EditarProductoModel : PageModel {

        private readonly TutorialUEContext _context;
        private readonly ProductoServices _service;

        public EditarProductoModel(TutorialUEContext context, ProductoServices service) {
            _context = context;
            _service = service;
        }

        [BindProperty]
        public Producto Producto { get; set; }



        public async Task OnGetAsync(int id) {
            Producto = await this._context.Productos.FindAsync(id);
            if (Producto == null) {
                ViewData["mensajeError"] = $"El producto con ID {id} no fue encontrado para editar";
                return;
            }

        }

        public async Task<IActionResult> OnPostBorrarAsync(int id, string nombre) {
            var prod = await this._context.Productos.FindAsync(id);
            if (prod == null) {
                ViewData["mensajeError"] = "Este producto no se puede borrar su id no fue encontrado, si considera que es un error contacte con el administrador";
                return Page();
            }
            this._context.Productos.Remove(prod);
            await this._context.SaveChangesAsync();
            TempData["mensajeSuccess"] = $"producto {nombre} con id {id}, fue borrado exitosamente";
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }

            var prod = await this._context.Productos.FindAsync(Producto.Id);
            if (prod == null) {
                ViewData["mensajeError"] = $"El producto con ID {Producto.Id} no fue encontrado no se puede editar";
                return Page();
            }

            prod.Nombre = Producto.Nombre;
            prod.Descripcion = Producto.Descripcion;
            prod.Stock = Producto.Stock;
            prod.Precio = Producto.Precio;
            prod.Activo = Producto.Activo;

            var rta = await this._service.GuardarProductoAsync(prod);
            if (rta.Error) {
                ViewData["mensajeError"] = rta.MensajeUsuario;
                return Page();
            }

            ViewData["mensajeSuccess"] = "Producto editado con exito";
            return Page();
        }
    }
}
