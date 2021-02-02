using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCore.Data;
using MvcCore.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

#region StoreProcedure

//Create procedure EditCar
//(@idcoche int,@marca nvarchar(50),@modelo nvarchar(50),@conductor nvarchar(50),@imagen nvarchar(50))
//as
//    update coches set marca = @marca, modelo = @modelo, conductor = @conductor, imagen = @imagen
//      where idcoche=@idcoche
//go

//create procedure InsertCar
//(@marca nvarchar(50),@modelo nvarchar(50),@conductor nvarchar(50),@imagen nvarchar(50))
//as
//    Declare @idcoche int
//    set @idcoche = (select MAX(idcoche) as idcoche from coches) + 1

//	insert into coches values (@idcoche, @marca, @modelo, @conductor, @imagen)
//	select* from coches where idcoche = @idcoche

//go

//alter procedure DeleteCar
//(@idcoche int, @marca nvarchar(50))
//as
//    delete from coches where marca = @marca and idcoche = @idcoche;
//go
#endregion

namespace MvcCore.Repositories
{
    public class RepositoryCochesSql : IRepositoryCoches
    {
        CochesContext context;

        public RepositoryCochesSql(CochesContext Context) { this.context = Context; }

        public List<Coche> GetCoches()
        {
            var query = from datos in this.context.Coches
                        select datos;
            return query.ToList();
        }

        public Coche GetCocheId(int idcoche)
        {
            try
            {
                var query = from datos in this.context.Coches.Where(x => x.id == idcoche)
                            select datos;
                if (query.Count() != 0)
                {
                    return query.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }catch(Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return null;           
        }

        public bool EditarCoche(int id,string marca,string modelo,string conductor,string imagen)
        {
            try
            {
                string query = "EditCar @idcoche,@marca,@modelo,@conductor,@imagen";

                SqlParameter pamId = new SqlParameter("@idcoche",id);
                SqlParameter pamMarca = new SqlParameter("@marca", marca);
                SqlParameter pamModelo = new SqlParameter("@modelo", modelo);
                SqlParameter pamConductor = new SqlParameter("@conductor", conductor);
                SqlParameter pamImagen = new SqlParameter("@imagen", imagen);

                this.context.Database.ExecuteSqlRaw(query,pamId, pamMarca, pamModelo, pamConductor, pamImagen);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return false;
        }

        public void Insertarcoche(string marca, string modelo, string conductor, string imagen)
        {
            try
            {
               string query = "InsertCar @marca,@modelo,@conductor,@imagen";
                               
                SqlParameter pamMarca = new SqlParameter("@marca", marca);
                SqlParameter pamModelo = new SqlParameter("@modelo", modelo);
                SqlParameter pamConductor = new SqlParameter("@conductor", conductor);
                SqlParameter pamImagen = new SqlParameter("@imagen", imagen);
                this.context.Database.ExecuteSqlRaw(query,  pamMarca, pamModelo, pamConductor, pamImagen);                

            }
            catch(Exception e)
            {
                Console.WriteLine("Error al insertar: " + e.Message);
            }
            
        }

        public void EliminarCocheId(int id)
        {
            try
            {
                Coche car = this.GetCocheId(id);
                this.context.Coches.Remove(car);
                this.context.SaveChanges();
            }catch(Exception e)
            {
                Console.WriteLine("Exception" + e.Message);
            }
        }

        public bool DeleteCar(int idcoche,string marca)
        {
            try
            {
                string query = " DeleteCar @idcoche,@marca";
                SqlParameter pamId = new SqlParameter("@idcoche", idcoche);
                SqlParameter pamMarca = new SqlParameter("@marca", marca);
                this.context.Database.ExecuteSqlRaw(query, pamId, pamMarca);
                this.context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception" + e.Message);

            }
            return false;
        }
    }
}
