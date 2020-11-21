using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM.Pessoas.Models
{
    public class Endereco
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("numero")]
        public int Numero { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }

        [Column("cidade")]
        public string Cidade { get; set; }

        [Column("cep")]
        public string CEP { get; set; }

        [Column("estado")]
        public string UF { get; set; }

        [Column("pessoa_ref")]
        public int PessoaREF { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
