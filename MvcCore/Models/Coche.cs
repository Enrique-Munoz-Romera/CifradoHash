using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Models
{
    [Table("Coches")]
    public class Coche
    {
        [Key]
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Column("idcoche")]
        public int id { get; set; }

        [Column("marca")]
        public string marca { get; set; }

        [Column("modelo")]
        public string modelo { get; set; }

        [Column("conductor")]
        public string conductor { get; set; }

        [Column("imagen")]
        public string imagen { get; set; }
    }
}
