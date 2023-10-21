using System.ComponentModel.DataAnnotations;

namespace TutorialEU.Models;

public class Person {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    [Range(0, 999999999, ErrorMessage = "El valor debe ser mayor a 0 y con una longitud menor a 9 digitos (999999999)")]
    public int Secret { get; set; }
    public DateTime CreatedAt { get; set; }
    //por defecto estan desactivados
    public bool IsEnabled { get; set; } = false;
}
