using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIM.Pessoas.Models
{
    [Table("Pessoa")]
    public class Pessoa
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("cpf")]
        public string CPF { get; set; }

        public Endereco Endereco { get; set; }
        public IList<Telefone> Telefones { get; set; }

        public override string ToString()
        {
            return $"[{Id}]: {Nome} {CPF}";
        }

        // Retorna somente um número de telefone completo
        public string Telefone()
        {
            return null;
        }

        // Retorna um endereço completo
        public string Logradouro()
        {
            return null;
        }
    }
}
