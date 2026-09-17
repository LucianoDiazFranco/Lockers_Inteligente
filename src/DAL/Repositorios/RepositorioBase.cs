using System;
using System.Collections.Generic;
using LockersInteligentes.Dominio.Entidades;
using Microsoft.Data.SqlClient;

namespace LockersInteligentes.DAL.Repositorios
{
    public abstract class RepositorioBase<T> : IRepositorio<T> where T : EntidadBase
    {
        protected abstract string NombreTabla { get; }
        protected abstract string ColumnaId { get; }
        protected abstract string SqlSelect { get; }
        protected abstract string FiltroPorId { get; }

        protected abstract string SqlInsert { get; }
        protected abstract string SqlUpdate { get; }
        protected abstract T Mapear(SqlDataReader lector);
        protected abstract void CargarParametros(SqlCommand comando, T entidad);

        public virtual IList<T> ObtenerTodos()
        {
            List<T> lista = new List<T>();

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(SqlSelect, conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                        lista.Add(Mapear(lector));
                }
            }

            return lista;
        }

        public virtual T ObtenerPorId(int id)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(SqlSelect + " WHERE " + FiltroPorId, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                        return Mapear(lector);
                }
            }
            return null;
        }

        public virtual void Insertar(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException("entidad");

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(SqlInsert + "; SELECT CAST(SCOPE_IDENTITY() AS INT);", conexion))
            {
                CargarParametros(comando, entidad);
                conexion.Open();
                object resultado = comando.ExecuteScalar();
                entidad.Id = Convert.ToInt32(resultado);
            }
        }

        public virtual void Actualizar(T entidad)
        {
            if (entidad == null)
                throw new ArgumentNullException("entidad");

            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(SqlUpdate, conexion))
            {
                CargarParametros(comando, entidad);
                comando.Parameters.AddWithValue("@Id", entidad.Id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public virtual void Eliminar(int id)
        {
            using (SqlConnection conexion = Conexion.Crear())
            using (SqlCommand comando = new SqlCommand(
                "DELETE FROM " + NombreTabla + " WHERE " + ColumnaId + " = @Id", conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }
    }
}