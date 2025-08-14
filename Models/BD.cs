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


public Usuario ObtenerUsuarioPorUsername(string username)
{
    string query = "SELECT * FROM Usuarios WHERE UserName = @pUsername";
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        return connection.QueryFirstOrDefault<Usuario>(query, new { pUsername = username });
    }
}
public void AgregarTarea(Tarea tarea)
{
    string query = @"INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizado, IdU) 
                     VALUES (@pTitulo, @pDescripcion, @pFecha, @pFinalizado, @pIdU)";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new
        {
            pTitulo = tarea.Titulo,
            pDescripcion = tarea.Descripcion,
            pFecha = tarea.Fecha,
            pFinalizado = tarea.Finalizado,
            pIdU = tarea.IdU
        });
    }
}
public void EliminarTarea(int idTarea)
{
    string query = "DELETE FROM Tareas WHERE ID = @pID";

    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        connection.Execute(query, new { pID = idTarea });
    }
}
public string MarcarTareaComoFinalizada(int idTarea)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {

        string selectQuery = "SELECT Finalizado FROM Tareas WHERE ID = @pID";
        bool finalizado = connection.QueryFirstOrDefault<bool>(selectQuery, new { pID = idTarea });

        if (finalizado)
        {
            return "La tarea ya estaba completada.";
        }


        string updateQuery = "UPDATE Tareas SET Finalizado = 1 WHERE ID = @pID";
        connection.Execute(updateQuery, new { pID = idTarea });

        return "La tarea fue marcada como completada.";
    }
}
public void EditarTarea(Tarea tarea) 
{ string query = @"UPDATE Tareas SET Titulo = @pTitulo, Descripcion = @pDescripcion, Fecha = @pFecha, Finalizado = @pFinalizado, IdU = @pIdU WHERE ID = @pID"; 
using (SqlConnection connection = new SqlConnection(_connectionString))
 { connection.Execute(query, new 
 { pTitulo = tarea.Titulo
 , pDescripcion = tarea.Descripcion
 , pFecha = tarea.Fecha
 , pFinalizado = tarea.Finalizado
 , pIdU = tarea.IdU
 , pID = tarea.ID }); } }

public List<Tarea> TareasPorUsuario(int idUsuario)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = "SELECT * FROM Tareas WHERE IdU = @pIdU AND Finalizado = 0"; 
        return connection.Query<Tarea>(query, new { pIdU = idUsuario }).ToList();
    }
}



}