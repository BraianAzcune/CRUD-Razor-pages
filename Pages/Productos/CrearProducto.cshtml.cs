using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.RegularExpressions;
using TutorialEU.Data;
using TutorialEU.Models;

namespace TutorialEU.Pages.Productos {
    public class CrearProductoModel : PageModel {

        private readonly TutorialUEContext _context;
        private readonly ILogger<CrearProductoModel> logger;

        public CrearProductoModel(TutorialUEContext context, ILogger<CrearProductoModel> logger) {
            _context = context;
            this.logger = logger;
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
                try {

                    await this._context.SaveChangesAsync();
                } catch (DbUpdateException excepcion) {
                    if (excepcion.InnerException is not SqlException) {
                        this.logger.LogCritical("Error no controlado", excepcion);
                        ViewData["mensajeError"] = "Error no controlado contacte con el administrador";
                        return Page();
                    }
                    SqlException ex = (SqlException)excepcion.InnerException;

                    // error index unico || error clave primaria existente
                    if (ex.Number == 2601 || ex.Number == 2627) {
                        this.logger.LogError("error clave primaria o clave indexada debe ser unica", ex.Message);
                        string pattern = @"key value is \(([^)]+)\)";
                        Match match = Regex.Match(ex.Message, pattern);
                        if (match.Success) {
                            ViewData["mensajeError"] = "Error un producto tiene un valor duplicado, que no puede usar, cambielo, el valor duplicado es: " + match.Groups[1].Value;
                        } else {
                            ViewData["mensajeError"] = "Error un producto tiene un valor duplicado, que no puede usar, el sistema no sabe cual es, contacte con el administrador";
                        }
                    } else {
                        this.logger.LogCritical("Error no controlado", ex);
                        ViewData["mensajeError"] = "Error de base de datos no controlada contacte con el administrador";
                    }
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
