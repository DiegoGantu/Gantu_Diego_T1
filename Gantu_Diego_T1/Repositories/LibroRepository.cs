using System.Data;
using Microsoft.Data.SqlClient;
using Gantu_Diego_T1.Models;

namespace Gantu_Diego_T1.Repositories;

public class LibroRepository : ILibroRepository
{
    private readonly IConfiguration _configuration;

    public LibroRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Libro> ListarLibros(string storeProcedure, Dictionary<string, object> parametros)
    {
        List<Libro> listaLibros = new List<Libro>();

        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("BD")))
        {
            sqlConnection.Open();
            using (SqlCommand sqlCommand = new SqlCommand(storeProcedure, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parametros)
                {
                    sqlCommand.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        listaLibros.Add(new Libro
                        {
                            Id = sqlDataReader.GetInt32(0),
                            NombreLibro = sqlDataReader.GetString(1),
                            Autor = sqlDataReader.GetString(2),
                            NombreEditorial = sqlDataReader.GetString(3),
                            ISDN = sqlDataReader.GetString(4)
                        });
                    }
                }
            }
        }

        return listaLibros;
    }

    public Libro? ObtenerLibroPorId(string storeProcedure, Dictionary<string, object> parametros)
    {
        Libro? libro = null;

        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("BD")))
        {
            sqlConnection.Open();
            using (SqlCommand sqlCommand = new SqlCommand(storeProcedure, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parametros)
                {
                    sqlCommand.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        libro = new Libro
                        {
                            Id = sqlDataReader.GetInt32(0),
                            NombreLibro = sqlDataReader.GetString(1),
                            Autor = sqlDataReader.GetString(2),
                            NombreEditorial = sqlDataReader.GetString(3),
                            ISDN = sqlDataReader.GetString(4)
                        };
                    }
                }
            }
        }

        return libro;
    }

    public int EjecutarComando(string storeProcedure, Dictionary<string, object> parametros)
    {
        using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("BD")))
        {
            sqlConnection.Open();
            using (SqlCommand sqlCommand = new SqlCommand(storeProcedure, sqlConnection))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;

                foreach (KeyValuePair<string, object> param in parametros)
                {
                    sqlCommand.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }

                return sqlCommand.ExecuteNonQuery();
            }
        }
    }
}