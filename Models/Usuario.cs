using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;
namespace Tp06.Models;

public class Usuario
{
    [JsonProperty]
    public int ID { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime UltimoLogin { get; set; }
    public string Password { get; set; }
    public string Foto { get; set; }
    [JsonProperty]
    public string UserName { get; set; }
}