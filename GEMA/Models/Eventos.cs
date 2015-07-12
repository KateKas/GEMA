using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Eventos
    {
        public int Id { get; set; }

        [DisplayName("Evento")]
        public string Evento { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEvento { get; set; }

        [DisplayName("Pessoa")]
        public virtual Pessoas Pessoas { get; set; }

        [DisplayName("Matéria")]
        public virtual Materias Materias { get; set; }
    }
}