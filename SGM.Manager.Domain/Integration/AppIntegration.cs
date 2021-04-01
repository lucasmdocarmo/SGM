using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities.Integration
{
    public sealed class AppIntegration :BaseEntity
    {
        public AppIntegration(string sistema, ESistema sistemaRaiz)
        {
            Sistema = sistema;
            ApiKey = Guid.NewGuid();
            SistemaRaiz = sistemaRaiz;
            AppIntegrationCode = CodigoGenerator.GenerateCode();
        }

        public string Sistema { get; set; }
        public Guid ApiKey { get; set; }
        public string AppIntegrationCode { get; set; }
        public ESistema SistemaRaiz { get; set; }
    }
    public enum ESistema
    {
        Cidadao = 1,
        Manager = 2,
        Saude = 3
    }
    internal static class CodigoGenerator
    {
        private static readonly Random _random = new Random();
        public static string GenerateCode()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return "App" + _random.Next(0, 999999) + _random.Next(0, chars.Length - 1);
        }
    }
}
