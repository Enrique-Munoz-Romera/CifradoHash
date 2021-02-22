using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CifradoHash.Models
{
    [Table("CYPHERHASH")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Column("IdUser")]
        public int idUser { get; set; }

        [Column("Nombre")]
        public string name { get; set; }

        [Column("Usuario")]
        public string user { get; set; }

        [Column("Pswd")]
        public byte[] pswd { get; set; }

        [Column("Salt")]
        public string salt { get; set; }

    }
}
