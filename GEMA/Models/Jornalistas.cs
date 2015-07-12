using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    
    public class Jornalistas : Pessoas
    {
        public Jornalistas()
        {
            this.Materias = new HashSet<Materias>();
        }

        public virtual ICollection<Materias> Materias { get; set; }
    }
}