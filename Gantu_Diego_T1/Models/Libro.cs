using System.ComponentModel.DataAnnotations;

namespace Gantu_Diego_T1.Models;

public class Libro
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del libro es obligatorio.")]
    [StringLength(120)]
    public string NombreLibro { get; set; }

    [Required(ErrorMessage = "El autor es obligatorio.")]
    [StringLength(100)]
    public string Autor { get; set; }

    [Required(ErrorMessage = "El nombre de la editorial es obligatorio.")]
    [StringLength(100)]
    public string NombreEditorial { get; set; }

    [Required(ErrorMessage = "El ISDN es obligatorio.")]
    [StringLength(20)]
    public string ISDN { get; set; }
}