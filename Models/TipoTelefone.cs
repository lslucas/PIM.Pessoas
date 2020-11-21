using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM.Pessoas.Models
{
    public class TipoTelefone
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("tipo")]
        public string Nome { get; set; }

        public Telefone Telefone { get; set; }
    }
}
