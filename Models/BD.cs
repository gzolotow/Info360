using Microsoft.Data.SqlClient;
using Dapper;

namespace EcoPlay.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=Ecoplay;Integrated Security=True;TrustServerCertificate=True;";

    // REGISTRAR USUARIO
    public static bool Registrarse(string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto)
    {
        if (!BuscarUsuario(Username))
        {
            int idNivelUsuario;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string queryNivel = @"INSERT INTO NivelUsuario (IDNivel, AspectoEquipado, Estrellas, MisionesCompletadas) VALUES (@pIDNivel, @pAspectoEquipado, @pEstrellas, @pMisionesCompletadas);
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                idNivelUsuario = connection.QuerySingle<int>(queryNivel, new {
                    pIDNivel = 1,
                    pAspectoEquipado = 1,
                    pEstrellas = 0,
                    pMisionesCompletadas = 0
                });
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string queryUsuario = @"INSERT INTO Usuario (Username, Mail, FechaNacimiento, Contraseña, Foto, IDNivelUsuario) VALUES (@pUsername, @pMail, @pFechaNacimiento, @pContraseña, @pFoto, @pIDNivelUsuario)";

                connection.Execute(queryUsuario, new {
                    pUsername = Username,
                    pMail = Mail,
                    pFechaNacimiento = FechaNacimiento,
                    pContraseña = Contraseña,
                    pFoto = Foto,
                    pIDNivelUsuario = idNivelUsuario
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
    public static bool BuscarIDUsuario(string IDUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuario WHERE IDUsuario = @pIDUsuario";
            var usuario = connection.QueryFirstOrDefault<Usuario>(query, new { pIDUsuario = IDUsuario });
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
    public static Usuario ModificarUsuario(int IDUsuario, string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto, int IDNivelUsuario)
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
        Usuario usu = new Usuario(Username, Mail, FechaNacimiento, Contraseña, Foto, IDNivelUsuario);
        return (usu);
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

    public static NivelUsuario BuscarNivelUsuario(int IDUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM NivelUsuario WHERE IDNivelUsuario = (SELECT IDNivelUsuario FROM Usuario WHERE IDUsuario = @pIDUsuario)";
            return connection.QueryFirstOrDefault<NivelUsuario>(query, new { pIDUsuario = IDUsuario });
        }
    }

    public static string BuscarEquipado(int IDNivelUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT AspectoEquipado FROM NivelUsuario WHERE IDNivelUsuario = @pIDNivelUsuario";
            return connection.QueryFirstOrDefault<string>(query, new { pIDNivelUsuario = IDNivelUsuario });
        }
        
    }

}