using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;
namespace Tp06.Models;

public class BD
{
private static string _connectionString = @"Server=localhost;DataBase=TP06;Integrated Security=True;TrustServerCertificate=True;";

public List<Usuario> Usuarios()
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Usuarios";
        return connection.Query<Usuario>(query).ToList();
    }
}
public List<Tarea> LevantarTareas()
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Tareas";
        return connection.Query<Tarea>(query).ToList();
    }
}


}