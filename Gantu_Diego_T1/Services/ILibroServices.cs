using Gantu_Diego_T1.Models;

namespace Gantu_Diego_T1.Services;

public interface ILibroServices
{
    IEnumerable<Libro> ListarLibros();
    Libro? ObtenerLibroPorId(int id);
    bool RegistrarLibro(Libro libro);
    bool ActualizarLibro(Libro libro);
    bool EliminarLibro(int id);
    IEnumerable<Libro> ListarLibrosPorAutor(string autor);
}