using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Revisores : Pessoas
    {
        public Revisores()
        {
            this.Materias = new HashSet<Materias>();
        }

        public virtual ICollection<Materias> Materias { get; set; }
    }
}