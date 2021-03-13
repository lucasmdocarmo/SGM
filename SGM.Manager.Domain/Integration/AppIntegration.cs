using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities.Integration
{
    public sealed class AppIntegration :BaseEntity
    {
        public AppIntegration(string sistema)
        {
            Sistema = sistema;
            ApiKey = Guid.NewGuid();
            AppIntegrationCode = CodigoGenerator.GenerateCode();
        }

        public string Sistema { get; set; }
        public Guid ApiKey { get; set; }
        public string AppIntegrationCode { get; set; }
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
