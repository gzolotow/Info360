using Microsoft.Data.SqlClient;
using Dapper;

namespace EcoPlay.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=Ecoplay3_BD;Integrated Security=True;TrustServerCertificate=True;";

    public static bool Registrarse(string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto)
    {
        if(!BuscarUsuario(Username))
        {
            string query = "INSERT INTO Usuario (Username, Mail, FechaNacimiento, Contraseña, Foto) VALUES (@pUsername, @pMail, @pFechaNacimiento, @pContraseña, @pFoto)";

            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { pUsername = Username, pMail = Mail, pFechaNacimiento = FechaNacimiento, pContraseña = Contraseña, pFoto = Foto });
            }
            return true;
        }else{
            return false;
        }
    }
    
    public static bool BuscarUsuario(string Username)
    {
        Usuario miUsuario;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Username = @pUsername";
            miUsuario = connection.QueryFirstOrDefault<Usuario>(query, new { pUsername = Username });
        }
        if(miUsuario == null)
        {
            return false;
        }else{
            return true;
        }
    }
    public static Usuario Login(string Username, string Contraseña)
    {
        Usuario miUsuario = null;
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Username = @pUsername AND Contraseña = @pContraseña";
            if(query == null){

            }else{
                miUsuario = connection.QueryFirstOrDefault<Usuario>(query, new { pUsername = Username, pContraseña = Contraseña });
            }
        }
        return miUsuario;
    }
    public static void ModificarUsuario(int IdUsuario, string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto)
    {
        
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
           string query = "UPDATE Usuario SET Username = @pUsername, Mail = @pMail, pFechaNacimiento = @ppFechaNacimiento, Contraseña = @pContraseña WHERE IdUsuario = @pIdUsuario";
           connection.Execute(query, new { pIdUsuario = IdUsuario, pUsername = Username, pMail = Mail, pFechaNacimiento = FechaNacimiento, pContraseña = Contraseña, pFoto = Foto });
        }

    }
    public static void EliminarUsuario(int IdUsuario)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Usuario WHERE IdUsuario = @pIdUsuario";

            connection.Execute(query, new { pIdUsuario = IdUsuario});
        }
    }
    public static void ActLogin(int IdUsuarios)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Usuarios SET username, password WHERE IdUsuarios = @pUsusarios";

            connection.Execute(query, new { pUsuarios = IdUsuarios});
        }

    }
 
}