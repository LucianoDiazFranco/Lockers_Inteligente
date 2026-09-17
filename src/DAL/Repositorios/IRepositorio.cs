using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LockersInteligentes.Dominio.Entidades;

namespace LockersInteligentes.DAL.Repositorios
{
    public interface IRepositorio<T> where T : EntidadBase
    {
        void Insertar(T entidad);
        void Actualizar(T entidad);
        void Eliminar(int id);
        T ObtenerPorId(int id);
        IList<T> ObtenerTodos();
    }
}
