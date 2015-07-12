using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Secoes
    {
        public Secoes()
        {
            this.Materias = new HashSet<Materias>();
        }

        public int Id { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Seção")]
        public string Secao { get; set; }

        [DisplayName("Gerente")]
        public virtual Gerentes Gerentes { get; set; }

        [DisplayName("Matérias")]
        public virtual ICollection<Materias> Materias { get; set; }
    }
}