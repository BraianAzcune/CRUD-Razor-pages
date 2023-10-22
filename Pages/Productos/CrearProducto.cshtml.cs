using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TutorialEU.Data;
using TutorialEU.Models;
using TutorialEU.Services;

namespace TutorialEU.Pages.Productos {
    public class CrearProductoModel : PageModel {

        private readonly TutorialUEContext _context;
        private readonly ILogger<CrearProductoModel> logger;
        private readonly ProductoServices _services;

        public CrearProductoModel(TutorialUEContext context, ILogger<CrearProductoModel> logger, ProductoServices services) {
            _context = context;
            this.logger = logger;
            this._services = services;
        }

        [BindProperty]
        public Producto? Producto { get; set; }

        public void OnGet() {
        }

        public async Task<IActionResult> OnPostAsync() {
            if (!ModelState.IsValid) {
                return Page();
            }
            this.logger.LogInformation("producto on PostAsync: " + JsonSerializer.Serialize(this.Producto));

            var item = new Producto();
            if (await TryUpdateModelAsync<Producto>(
                item,
                "producto",
                i => i.Nombre, i => i.Descripcion, i => i.Stock, i => i.Precio, i => i.Activo
                )) {

                this._context.Add(item);
                var rta = await this._services.GuardarProductoAsync(item);
                if (rta.Error) {
                    ViewData["mensajeError"] = rta.MensajeUsuario;
                    return Page();
                }

                ViewData["mensajeSuccess"] = "producto creado con exito";
                return Page();
            }
            // resetear el formulario, para que pueda agregar nuevo producto
            this.Producto = null;
            ModelState.Clear();
            return Page();
        }
    }
}
