using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Pessoas
    {
        public Pessoas()
        {
            this.Comentarios = new HashSet<Comentarios>();
            this.Eventos = new HashSet<Eventos>();
        }

        public int Id { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }

        public virtual ICollection<Comentarios> Comentarios { get; set; }
        public virtual ICollection<Eventos> Eventos { get; set; }

        public virtual Papeis Papeis { get; set; }
    }
}