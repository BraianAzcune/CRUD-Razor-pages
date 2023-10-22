using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TutorialEU.common.interfaces;
using TutorialEU.Data;
using TutorialEU.Models;

namespace TutorialEU.Services;



public class ProductoServices {
    private readonly TutorialUEContext context;
    private readonly ILogger<ProductoServices> logger;

    public ProductoServices(TutorialUEContext context, ILogger<ProductoServices> logger) {
        this.context = context;
        this.logger = logger;
    }

    public async Task<ResultadoOperacion> GuardarProductoAsync(Producto prod) {
        try {
            var cambios = await this.context.SaveChangesAsync();
            if (cambios == 0) {
                return new ResultadoOperacion(true, "no se ha podido guardar", "SaveChangesAsync reporto 0 cambios");
            }
            if (cambios > 1) {
                this.logger.LogWarning("Se guardaron mas de lo esperado (esperaba 1)= " + cambios);
                return new ResultadoOperacion(false, "guardado de producto exitoso", $"SaveChangesAsync reporto {cambios} cambios");
            }
            return new ResultadoOperacion(false, "guardado de producto exitoso", "");

        } catch (Exception excepcion) {
            var result = new ResultadoOperacion(true, "Error desconocido contacte con el administrador", "");
            if (excepcion is not DbUpdateException) {
                this.logger.LogCritical("Error no manejado GuardarProductoAsync, excepcion no es DbUpdateException", excepcion);
                result.MensajeDesarrollador = "Error no manejado GuardarProductoAsync, excepcion no es DbUpdateException";

            }
            if (excepcion.InnerException is not SqlException) {
                this.logger.LogCritical("Error no manejado GuardarProductoAsync, InnerException no es SqlException", excepcion);
                result.MensajeDesarrollador = "Error no manejado GuardarProductoAsync, InnerException no es SqlException";
            }
            var ex = excepcion.InnerException as SqlException;
            if (ex == null) {
                this.logger.LogCritical("excepcion.InnerException as SqlException casteo nulo");
                return result;
            }

            // error index unico || error clave primaria existente
            if (ex.Number == 2601 || ex.Number == 2627) {
                this.logger.LogError("error clave primaria o clave indexada debe ser unica", ex.Message);
                string pattern = @"key value is \(([^)]+)\)";
                Match match = Regex.Match(ex.Message, pattern);
                if (match.Success) {
                    result.MensajeUsuario = "Error un producto tiene un valor duplicado, que no puede usar, cambielo, el valor duplicado es: " + match.Groups[1].Value;
                } else {
                    result.MensajeUsuario = "Error un producto tiene un valor duplicado, que no puede usar, el sistema no sabe cual es, contacte con el administrador";
                }
            } else {
                this.logger.LogCritical($"Error no controlado SqlException ({ex.Number})", ex);
                result.MensajeDesarrollador = $"Error no controlado SqlException ({ex.Number})";
            }
            return result;
        }

    }
}
