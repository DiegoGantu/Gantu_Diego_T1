using Gantu_Diego_T1.Models;

namespace Gantu_Diego_T1.Repositories;

public interface ILibroRepository
{
    IEnumerable<Libro> ListarLibros(string storeProcedure, Dictionary<string, object> parametros);
    Libro? ObtenerLibroPorId(string storeProcedure, Dictionary<string, object> parametros);
    int EjecutarComando(string storeProcedure, Dictionary<string, object> parametros);
}