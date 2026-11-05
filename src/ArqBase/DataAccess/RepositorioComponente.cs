using LockersInteligentes.ArqBase.Composite;
using LockersInteligentes.ArqBase.Enums;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockersInteligentes.ArqBase.DataAccess
{
    public class RepositorioComponente
    {
        /// <summary>Carga el árbol completo y devuelve todos los componentes indexados por Id.</summary>
        public IDictionary<int, Componente> ObtenerArbolCompleto()
        {
            Dictionary<int, Componente> mapa = new Dictionary<int, Componente>();

            // Primera pasada: crear cada componente una sola vez.
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT IdComponente, Nombre, Descripcion, TipoComponente " +
                "FROM dbo.Componente ORDER BY Nombre", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        TipoComponente tipo =
                            (TipoComponente)Convert.ToInt32(lector["TipoComponente"]);

                        Componente componente = tipo == TipoComponente.Familia
                            ? (Componente)new Familia()
                            : new Patente();

                        componente.Id = Convert.ToInt32(lector["IdComponente"]);
                        componente.Nombre = lector["Nombre"].ToString();
                        componente.Descripcion = lector["Descripcion"] == DBNull.Value
                            ? null : lector["Descripcion"].ToString();

                        mapa[componente.Id] = componente;
                    }
                }
            }

            // Segunda pasada: conectar padres con hijos usando las instancias ya creadas.
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT IdPadre, IdHijo FROM dbo.ComponenteHijo", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        int idPadre = Convert.ToInt32(lector["IdPadre"]);
                        int idHijo = Convert.ToInt32(lector["IdHijo"]);

                        Componente padre;
                        Componente hijo;

                        if (!mapa.TryGetValue(idPadre, out padre) ||
                            !mapa.TryGetValue(idHijo, out hijo))
                            continue;

                        Familia familia = padre as Familia;

                        if (familia != null)
                            familia.Agregar(hijo);
                    }
                }
            }

            return mapa;
        }

        /// <summary>Componentes asignados directamente a un usuario.</summary>
        public IList<Componente> ObtenerDeUsuario(int idUsuario)
        {
            IDictionary<int, Componente> mapa = ObtenerArbolCompleto();
            List<Componente> asignados = new List<Componente>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT IdComponente FROM dbo.UsuarioComponente WHERE IdUsuario = @IdUsuario",
                conexion))
            {
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Componente componente;

                        if (mapa.TryGetValue(Convert.ToInt32(lector["IdComponente"]), out componente))
                            asignados.Add(componente);
                    }
                }
            }

            return asignados;
        }

        /// <summary>Raíces del árbol: componentes que no son hijos de nadie.</summary>
        public IList<Componente> ObtenerRaices()
        {
            IDictionary<int, Componente> mapa = ObtenerArbolCompleto();
            HashSet<int> hijos = new HashSet<int>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "SELECT DISTINCT IdHijo FROM dbo.ComponenteHijo", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        hijos.Add(Convert.ToInt32(lector["IdHijo"]));
                }
            }

            return mapa.Values.Where(c => !hijos.Contains(c.Id)).OrderBy(c => c.Nombre).ToList();
        }

        public Componente Insertar(string nombre, string descripcion, TipoComponente tipo)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "INSERT INTO dbo.Componente (Nombre, Descripcion, TipoComponente) " +
                "VALUES (@Nombre, @Descripcion, @Tipo); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Descripcion",
                    descripcion != null ? (object)descripcion : DBNull.Value);
                comando.Parameters.AddWithValue("@Tipo", (byte)tipo);
                conexion.Open();

                int id = Convert.ToInt32(comando.ExecuteScalar());

                Componente creado = tipo == TipoComponente.Familia
                    ? (Componente)new Familia()
                    : new Patente();

                creado.Id = id;
                creado.Nombre = nombre;
                creado.Descripcion = descripcion;

                return creado;
            }
        }

        public void Actualizar(int idComponente, string nombre, string descripcion)
        {
            Ejecutar("UPDATE dbo.Componente SET Nombre = @Nombre, Descripcion = @Descripcion " +
                     "WHERE IdComponente = @Id",
                new SqlParameter("@Nombre", nombre),
                new SqlParameter("@Descripcion", (object)descripcion ?? DBNull.Value),
                new SqlParameter("@Id", idComponente));
        }

        /// <summary>Elimina el componente y todos sus vínculos como padre o hijo.</summary>
        public void Eliminar(int idComponente)
        {
            Ejecutar("DELETE FROM dbo.UsuarioComponente WHERE IdComponente = @Id",
                new SqlParameter("@Id", idComponente));

            Ejecutar("DELETE FROM dbo.ComponenteHijo WHERE IdPadre = @Id OR IdHijo = @Id",
                new SqlParameter("@Id", idComponente));

            Ejecutar("DELETE FROM dbo.Componente WHERE IdComponente = @Id",
                new SqlParameter("@Id", idComponente));
        }

        public void AgregarHijo(int idPadre, int idHijo)
        {
            Ejecutar("INSERT INTO dbo.ComponenteHijo (IdPadre, IdHijo) VALUES (@Padre, @Hijo)",
                new SqlParameter("@Padre", idPadre),
                new SqlParameter("@Hijo", idHijo));
        }

        public void QuitarHijo(int idPadre, int idHijo)
        {
            Ejecutar("DELETE FROM dbo.ComponenteHijo WHERE IdPadre = @Padre AND IdHijo = @Hijo",
                new SqlParameter("@Padre", idPadre),
                new SqlParameter("@Hijo", idHijo));
        }

        public void AsignarAUsuario(int idUsuario, int idComponente)
        {
            Ejecutar("INSERT INTO dbo.UsuarioComponente (IdUsuario, IdComponente) " +
                     "VALUES (@Usuario, @Componente)",
                new SqlParameter("@Usuario", idUsuario),
                new SqlParameter("@Componente", idComponente));
        }

        public void QuitarDeUsuario(int idUsuario, int idComponente)
        {
            Ejecutar("DELETE FROM dbo.UsuarioComponente " +
                     "WHERE IdUsuario = @Usuario AND IdComponente = @Componente",
                new SqlParameter("@Usuario", idUsuario),
                new SqlParameter("@Componente", idComponente));
        }

        private void Ejecutar(string sql, params SqlParameter[] parametros)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddRange(parametros);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}
