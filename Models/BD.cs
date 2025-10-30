using Microsoft.Data.SqlClient;
using Dapper;

namespace EcoPlay.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=Ecoplay;Integrated Security=True;TrustServerCertificate=True;";

    // REGISTRAR USUARIO
    public static bool Registrarse( string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto)
    {
        if (!BuscarUsuario(Username))
        {
            string query = @"INSERT INTO Usuario (Username, Mail, FechaNacimiento, Contraseña, Foto) VALUES (@pUsername, @pMail, @pFechaNacimiento, @pContraseña, @pFoto)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new{
                    
                    pUsername = Username,
                    pMail = Mail,
                    pFechaNacimiento = FechaNacimiento,
                    pContraseña = Contraseña,
                    pFoto = Foto
                });
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    // BUSCAR USUARIO POR USERNAME
    public static bool BuscarUsuario(string Username)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Username = @pUsername";
            var usuario = connection.QueryFirstOrDefault<Usuario>(query, new { pUsername = Username });
            return usuario != null;
        }
    }

    // LOGIN
    public static Usuario Login(string Username, string Contraseña)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE Username = @pUsername AND Contraseña = @pContraseña";
            return connection.QueryFirstOrDefault<Usuario>(query, new { pUsername = Username, pContraseña = Contraseña });
        }
    }

    // MODIFICAR USUARIO
    public static void ModificarUsuario(int IDUsuario, string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto)
    {
        string query = @"UPDATE Usuario 
                         SET Username = @pUsername, Mail = @pMail, FechaNacimiento = @pFechaNacimiento, 
                             Contraseña = @pContraseña, Foto = @pFoto 
                         WHERE IDUsuario = @pIDUsuario";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new
            {
                pIDUsuario = IDUsuario,
                pUsername = Username,
                pMail = Mail,
                pFechaNacimiento = FechaNacimiento,
                pContraseña = Contraseña,
                pFoto = Foto
            });
        }
    }

    // ELIMINAR USUARIO
    public static void EliminarUsuario(int IDUsuario)
    {
        string query = "DELETE FROM Usuario WHERE IDUsuario = @pIDUsuario";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pIDUsuario = IDUsuario });
        }
    }
}