using Microsoft.EntityFrameworkCore;
using MvcCore.Data;
using MvcCore.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Repositories
{
    public class RepositoryCochesMySql : IRepositoryCoches
    {
        CochesContext context;

        public RepositoryCochesMySql(CochesContext Context) { this.context = Context; }


        //private int idcoche()
        //{
        //    var query = from datos in this.context.Coches.OrderByDescending<Coche>(x=>x.id)
        //                select datos;
              
        //    return query.FirstOrDefault();
        //}

        public List<Coche> GetCoches()
        {
            return this.context.Coches.ToList();
        }

        public Coche GetCocheId(int id)
        {
            return this.context.Coches.Where(x => x.id == id).FirstOrDefault();
        }


        public void EditarCoche(int id, string marca, string modelo, string conductor, string imagen)
        {
            try
            {
                Coche car = this.GetCocheId(id);
                car.id = id;
                car.marca = marca;
                car.modelo = modelo;
                car.conductor = conductor;
                car.imagen = imagen;
                this.context.Coches.Add(car);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            
        }

        //public void Insertarcoche(string marca, string modelo, string conductor, string imagen)
        //{
        //    Lamentablememnte no tengo tiempo para solucionarlo e investigar
        //    string query = "InsertCar @marca,@modelo,@conductor,@imagen";

        //    MySqlParameter pamMarca = new MySqlParameter("@marca", marca);
        //    MySqlParameter pamModelo = new MySqlParameter("@modelo", modelo);
        //    MySqlParameter pamConductor = new MySqlParameter("@conductor", conductor);
        //    MySqlParameter pamImagen = new MySqlParameter("@imagen", imagen);
        //      aqui nos viene el fallo :)
        //    this.context.Database.ExecuteMySqlRaw(query, pamMarca, pamModelo, pamConductor, pamImagen);
        //}

        public void Insertarcoche(string marca, string modelo, string conductor, string imagen)
        {
            //Coche car = new Coche();
            //car.id = this.idcoche();
            //car.marca = marca;
            //car.modelo = modelo;
            //car.conductor = conductor;
            //car.imagen = imagen;
            //this.context.Coches.Add(car);
            //this.context.SaveChanges();

            using (DbCommand com =
                    this.context.Database.GetDbConnection().CreateCommand())
            {
                String sql = "InsertCar @marca, @modelo, @conductor, @imagen";
                com.CommandType = System.Data.CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Parameters.Add(new MySqlParameter("@marca",marca));
                com.Parameters.Add(new MySqlParameter("@modelo",modelo));
                com.Parameters.Add(new MySqlParameter("@conductor",conductor));
                com.Parameters.Add(new MySqlParameter("@imagen",imagen));
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();
                
                reader.Close();
                com.Parameters.Clear();
                com.Connection.Close();              
            }

        }

        public void EliminarCoche(int id)
        {
            Coche coche = this.GetCocheId(id);
            this.context.Coches.Remove(coche);
            this.context.SaveChanges();
        }

        public void DeleteCar(int id,string marca)
        {

        }
    }
}
