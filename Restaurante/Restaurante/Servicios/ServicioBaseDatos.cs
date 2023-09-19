using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Restaurante.Datos;
using Restaurante.Modelos;

namespace Restaurante.Servicios
{
    public class ServicioBaseDatos<T> : IServicioBaseDatos<T> where T : class
    {
        BaseDatos bd;

        public ServicioBaseDatos()
        {
            bd = App.BD;
        }

        public async Task<bool> CheckNombreExistsAsync(string nombre)
        {
            // Verificar si existe una sede con el mismo nombre en la base de datos
            var existingSede = await bd.Set<Sede>().FirstOrDefaultAsync(s => s.Nombre == nombre);
            return existingSede != null;
        }

        public virtual async Task<List<T>> ObtenerTabla()
        {
            return await bd.Set<T>().ToListAsync();
        }

        public virtual async Task<T> BuscarPorId(int id)
        {
            return await bd.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Agregar(T dato)
        {
            await bd.Set<T>().AddAsync(dato);
            await bd.SaveChangesAsync();
            return dato;
        }
        public virtual async Task<T> Actualizar(T dato)
        {
            bd.Set<T>().Update(dato);
            await bd.SaveChangesAsync();
            return dato;
        }

        public virtual async Task<bool> Eliminar(int id)
        {
            var entity = await BuscarPorId(id);
            bd.Set<T>().Remove(entity);
            await bd.SaveChangesAsync();
            return true;
        }
    }
}
