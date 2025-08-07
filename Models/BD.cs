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
public void AgregarUsuario(Usuario usuario)
{
    string query = "INSERT INTO Usuarios (Nombre, Apellido, UltimoLogin, Password, Foto, UserName) VALUES (@pNombre, @pApellido, @pUltimoLogin, @pPassword, @pFoto, @pUserName)";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new
        {
            pNombre = usuario.Nombre,
            pApellido = usuario.Apellido,
            pUltimoLogin = usuario.UltimoLogin,
            pPassword = usuario.Password,
            pFoto = usuario.Foto,
            pUserName = usuario.UserName
        });
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