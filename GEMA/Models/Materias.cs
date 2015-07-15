using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Materias
    {
        public Materias()
        {
            this.Eventos = new HashSet<Eventos>();
            this.Comentarios = new HashSet<Comentarios>();
        }

        public int Id { get; set; }

        [DisplayName("Título")]
        public string Titulo { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime DataMateria { get; set; }

        [DisplayName("Matéria")]
        public string Materia { get; set; }

        [DisplayName("Condição")]
        public int Condicao { get; set; }

        [DisplayName("Seção")]
        public virtual Secoes Secoes { get; set; }

        [DisplayName("Eventos")]
        public virtual ICollection<Eventos> Eventos { get; set; }

        [DisplayName("Revisor")]
        public virtual Revisores Revisores { get; set; }

        [DisplayName("Jornalista")]
        public virtual Jornalistas Jornalistas { get; set; }

        [DisplayName("Gerente")]
        public virtual Gerentes Gerentes { get; set; }

        [DisplayName("Publicador")]
        public virtual Publicadores Publicadores { get; set; }

        [DisplayName("Comentários")]
        public virtual ICollection<Comentarios> Comentarios { get; set; }
    }
}