using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GEMA.Models
{
    public class Comentarios
    {
        public int Id { get; set; }

        [DisplayName("Comentário")]
        [Required(ErrorMessage = "O campo comentário é obrigatorio.")]
        public string Comentario { get; set; }

        [DisplayName("Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public System.DateTime DataComentario { get; set; }

        [DisplayName("Autor")]
        public virtual Pessoas Pessoas { get; set; }

        [DisplayName("Matéria")]
        public virtual Materias Materias { get; set; }
    }
}