using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;
namespace Tp06.Models;

public class Tarea {
    
    public int ID { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }
    public bool Finalizado { get; set; }
    public int IdU { get; set; }
}