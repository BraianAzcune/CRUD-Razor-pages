using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TutorialEU.Models;

[Index(nameof(Nombre), IsUnique = true)]
public class Producto {
    public int Id { get; set; }
    // la anotacion Display sirve para mostrar en Razor
    [Display(Name = "Nombre del producto")]
    public string Nombre { get; set; }
    [Display(Name = "Descripción")]
    public string Descripcion { get; set; }
    [Display(Name = "Cantidad")]
    public int Stock { get; set; }
    [DisplayFormat(DataFormatString = "{0:C0}")]
    public int Precio { get; set; }
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime CreatedAt { get; set; }
    [Display(Name = "Estado")]
    public bool Activo { get; set; } = false;
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime ModifiedAt { get; set; }
}
