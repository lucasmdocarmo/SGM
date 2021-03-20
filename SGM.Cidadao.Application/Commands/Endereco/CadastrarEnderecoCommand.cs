using Flunt.Notifications;
using SGM.Shared.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Cidadao.Application.Commands.Endereco
{
    public class CadastrarEnderecoCommand : Notifiable, ICommand
    {
        public CadastrarEnderecoCommand(string cEP, string logradouro, string numero, string complemento, 
            string cidade, string estado, Guid cidadaoId)
        {
            CEP = cEP;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cidade = cidade;
            Estado = estado;
            CidadaoId = cidadaoId;
        }

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid CidadaoId { get;  set; }

        public bool Validate()
        {
            return true;
        }

       
    }
}
