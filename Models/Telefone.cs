using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM.Pessoas.Models
{
    public class Telefone
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("numero")]
        public string Numero { get; set; }

        [Column("ddd")]
        public int DDD { get; set; }

        [Column("id_pessoa")]
        public int IdPessoa { get; set; }

        [Column("tipo")]
        public int TipoId { get; set; }

        public TipoTelefone Tipo { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
