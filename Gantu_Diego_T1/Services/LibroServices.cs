using Gantu_Diego_T1.Models;
using Gantu_Diego_T1.Repositories;

namespace Gantu_Diego_T1.Services;

public class LibroServices : ILibroServices
{
    private readonly ILibroRepository _libroRepository;

    public LibroServices(ILibroRepository libroRepository)
    {
        _libroRepository = libroRepository;
    }

    public IEnumerable<Libro> ListarLibros()
    {
        return _libroRepository.ListarLibros("usp_ListarLibros", new Dictionary<string, object>());
    }

    public Libro? ObtenerLibroPorId(int id)
    {
        Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "@Id", id }
        };

        return _libroRepository.ObtenerLibroPorId("usp_ObtenerLibroPorId", parametros);
    }

    public bool RegistrarLibro(Libro libro)
    {
        Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "@NombreLibro", libro.NombreLibro },
            { "@Autor", libro.Autor },
            { "@NombreEditorial", libro.NombreEditorial },
            { "@ISDN", libro.ISDN }
        };

        return _libroRepository.EjecutarComando("usp_RegistrarLibro", parametros) > 0;
    }

    public bool ActualizarLibro(Libro libro)
    {
        Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "@Id", libro.Id },
            { "@NombreLibro", libro.NombreLibro },
            { "@Autor", libro.Autor },
            { "@NombreEditorial", libro.NombreEditorial },
            { "@ISDN", libro.ISDN }
        };

        return _libroRepository.EjecutarComando("usp_ActualizarLibro", parametros) > 0;
    }

    public bool EliminarLibro(int id)
    {
        Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "@Id", id }
        };

        return _libroRepository.EjecutarComando("usp_EliminarLibro", parametros) > 0;
    }

    public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
    {
        Dictionary<string, object> parametros = new Dictionary<string, object>
        {
            { "@Autor", autor ?? string.Empty }
        };

        return _libroRepository.ListarLibros("usp_ListarLibrosPorAutor", parametros);
    }
}