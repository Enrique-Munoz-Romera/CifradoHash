using CypherHash.Data;
using CypherHash.Helpers;
using CypherHash.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CypherHash.Repositories
{
    public class RepositoryHash
    {
        CypherHashContext context;
        public RepositoryHash(CypherHashContext context) { this.context = context; }

        private int GetMaxIdUser()
        {
            int query = this.context.Usuarios.Max(x => x.idUser) + 1;
            return query;
        }

        public void InsertUser( string nombre, string username, string password)
        {
            Usuario user = new Usuario();
            user.idUser = GetMaxIdUser();
            //en el 1º registro estamos obligados a meterle el
            //valor a cañon para que no nos falle el codigo en
            //el metodo incremental
            //user.idUser = 1;
            user.name = nombre;
            user.user = username;
            String salt = CypherService.GetSalt();
            user.salt = salt;
            byte[] respuesta = CypherService.CypherHashefficent(password, salt);
            user.pswd = respuesta;
            this.context.Usuarios.Add(user);
            this.context.SaveChanges();
        }

        //Comparative credentials
        public Usuario UserLogin(string UserName, string pswd)
        {
            Usuario user = this.context.Usuarios.Where(z => z.user == UserName).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.salt;
                byte[] Pswdbbdd = user.pswd;
                byte[] PswdTemporal = CypherService.CypherHashefficent(pswd, salt);
                bool answer = ToolKit.ArraysComparative(Pswdbbdd, PswdTemporal);
                if (answer == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Usuario> GetUsuarios()
        {
            return this.context.Usuarios.ToList();
        }
    }
}
