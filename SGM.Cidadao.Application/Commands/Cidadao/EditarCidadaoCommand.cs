using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Cidadao
{
    public class EditarCidadaoCommand : Notifiable, ICommand
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]

        public string Identidade { get; set; }
        [Required]
        public bool Sexo { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Profissao { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Celular { get; set; }

        public bool Validate()
        {
            return true;
        }
    }
}
