﻿using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using SGM.Saude.Domain.Entities;
using SGM.Saude.Domain.Entities.Clinicas;
using SGM.Saude.Domain.Entities.Consulta;
using SGM.Shared.Core.Contracts.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Saude.Infra.Context
{
    public sealed class SaudeContext :DbContext, IUnitOfWork
    {
        public SaudeContext(DbContextOptions<SaudeContext> options) : base(options) { }

        public DbSet<Consultas> Consultas { get; set; } //ok
        public DbSet<Paciente> Paciente { get; set; } //ok
        public DbSet<Prescricao> Prescricao { get; set; }
        public DbSet<Clinica> Clinica { get; set; } //ok
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Medicos> Medicos { get; set; } //ok

        public void Rollback() => Database.RollbackTransaction();
        public void Begin() => Database.BeginTransaction();
        public async Task<bool> Commit() => await base.SaveChangesAsync() > 0;
        public bool CheckIfDatabaseExists() => Database.CanConnect();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }
        
    }
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var paciente1 = new Paciente("Lucas Mariano", Convert.ToDateTime("1992-03-17"), "11640810633", "13944854", true, "Paciente Teste", "Dermatologia");
            var paciente2 = new Paciente("Joao Da Silva", Convert.ToDateTime("1995-03-11"), "11506871102", "11323123", true, "Paciente Teste", "Urologia");
            var paciente3 = new Paciente("Ronaldo Coelho", Convert.ToDateTime("1998-03-17"), "11502321102", "1231231", true, "Paciente Teste", "Geral");
            var paciente4 = new Paciente("Cesar Junior", Convert.ToDateTime("1977-03-17"), "11211221102", "123123123", true, "Paciente Teste", "Dentista");
            var paciente5 = new Paciente("Igor da Silva", Convert.ToDateTime("1988-03-17"), "11623233233", "456456456", true, "Paciente Teste", "Pscicologo");
            var paciente6 = new Paciente("Sebastiao souza", Convert.ToDateTime("1933-03-17"), "98987899633", "3423443", true, "Paciente Teste", "Neurologista");
            var paciente7 = new Paciente("Arnold Pinto", Convert.ToDateTime("1998-03-17"), "21214081033", "678686778", true, "Paciente Teste", "Geral");
            var paciente8 = new Paciente("Sergio da silva", Convert.ToDateTime("2001-03-17"), "11621123633", "6786786", true, "Paciente Teste", "Dermatologia");
            var paciente9 = new Paciente("Vinicius da silva", Convert.ToDateTime("1966-03-17"), "11211230633", "45465457", true, "Paciente Teste", "Cardiologista");
            var paciente10 = new Paciente("Everton da Silva", Convert.ToDateTime("1976-03-17"), "21640812632", "978967573", true, "Paciente Teste", "Oftamologista");

            modelBuilder.Entity<Paciente>().HasData(paciente1, paciente2, paciente3, paciente4, paciente5, paciente6, paciente7, paciente8, paciente9, paciente10);

            var clinica1 = new Clinica("Clinica Contagem", "33939393");

            modelBuilder.Entity<Clinica>().HasData(clinica1);

            var timespaninicio = new TimeSpan();
            var timespantermino = new TimeSpan();
            TimeSpan.TryParse("08:00", out timespaninicio);
            TimeSpan.TryParse("18:00", out timespantermino);

            var medico1 = new Medicos("Dr. Joao", "Medico", "234234234", "Dermatologista", timespaninicio, timespantermino, true, 
                                        "joao@clicaEldorado.com", "312929929", 300M, true, clinica1.Id);

            var medico2 = new Medicos("Dr. Lucas", "Medico", "343434", "Urologia", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "lucas@clicaEldorado.com", "312929929",
                                300M, true, clinica1.Id);
            var medico3 = new Medicos("Dr. Felipe", "Medico", "674324", "Geral", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "felipe@clicaEldorado.com", "312929929",
                                500M, true, clinica1.Id);
            var medico4 = new Medicos("Dr. Carlos", "Medico", "2093873", "Dentista", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "carlos@clicaEldorado.com", "312929929",
                                100M, true, clinica1.Id);
            var medico5 = new Medicos("Dra. Joana", "Medico", "2039840", "Pscicologo", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "Joana@clicaEldorado.com", "312929929",
                                800M, true, clinica1.Id);
            var medico6 = new Medicos("Dr. Vitor", "Medico", "98232812", "Geral", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "Vitor@clicaEldorado.com", "312929929",
                                900M, true, clinica1.Id);
            var medico7 = new Medicos("Dr. Jose", "Medico", "89723921", "Dermatologia", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "Jose@clicaEldorado.com", "312929929",
                                300M, true, clinica1.Id);
            var medico8 = new Medicos("Dr. Silva", "Medico", "928374012", "Cardiologista", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "Silva@clicaEldorado.com", "312929929",
                                900M, true, clinica1.Id);
            var medico9 = new Medicos("Dr. Ronaldo", "Medico", "23894711", "Oftamologista", DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay, true, "Ronaldo@clicaEldorado.com", "312929929",
                                600M, true, clinica1.Id);

            modelBuilder.Entity<Medicos>().HasData(medico1, medico2, medico3, medico4, medico5, medico6, medico7, medico8, medico9);

            //var consulta_a = new Consultas("Dermatologia", "Consuta Médica", "", Convert.ToDateTime("04-29-2021"), paciente1.Id, medico1.Id);
            //var consulta_n = new Consultas("Dermatologia", "Consuta Médica", "", Convert.ToDateTime("04-25-2021"), paciente1.Id, medico7.Id);
            //var consulta_o = new Consultas("Dermatologia", "Consuta Médica", "", Convert.ToDateTime("2021-04-27"), paciente1.Id, medico7.Id);

            //var consulta_r = new Consultas("Pscicologo", "Retorno", "", Convert.ToDateTime("2021-04-29"), paciente1.Id, medico5.Id);
            //var consulta_c = new Consultas("Pscicologo", "Retorno", "", Convert.ToDateTime("2021-04-26"),  paciente1.Id, medico5.Id);
            //var consulta_k = new Consultas("Pscicologo", "Análise", "", Convert.ToDateTime("2021-04-22"), paciente1.Id, medico5.Id);

            //var consulta_l = new Consultas("Cardiologista", "Checkup", "", Convert.ToDateTime("2021-04-23"),  paciente1.Id, medico8.Id);
            //var consulta_d = new Consultas("Cardiologista", "Pós Operatorio", "", Convert.ToDateTime("2021-04-17"), paciente1.Id, medico8.Id);

            //var consulta_e = new Consultas("Oftamologista", "Checkup", "", Convert.ToDateTime("2021-04-15"), paciente1.Id, medico9.Id);
            //var consulta_m = new Consultas("Oftamologista", "Consuta Médica", "", Convert.ToDateTime("2021-04-24"),  paciente1.Id, medico9.Id);

            //var consulta_f = new Consultas("Urologia", "Consuta Médica", "", Convert.ToDateTime("2021-04-16"), paciente1.Id, medico2.Id);
            //var consulta_g = new Consultas("Urologia", "Consuta Médica", "", Convert.ToDateTime("2021-04-18"),  paciente1.Id, medico2.Id);
            //var consulta_h = new Consultas("Urologia", "Consuta Médica", "", Convert.ToDateTime("2021-04-19"),  paciente1.Id, medico2.Id);
            //var consulta_i = new Consultas("Urologia", "Consuta Médica", "", Convert.ToDateTime("2021-04-20"),  paciente1.Id, medico2.Id);

            //var consulta_p = new Consultas("Geral", "Consuta", "", Convert.ToDateTime("2021-04-27"),  paciente1.Id, medico3.Id);
            //var consulta_q = new Consultas("Cardiologista", "Consuta Médica", "", Convert.ToDateTime("2021-04-29"),  paciente1.Id, medico8.Id);

            //var consulta_b = new Consultas("Geral", "Checkup", "", Convert.ToDateTime("2021-04-29"),  paciente1.Id, medico6.Id);
            //var consulta_s = new Consultas("Geral", "Retorno", "", Convert.ToDateTime("2021-04-27"),  paciente1.Id, medico6.Id);
            //var consulta_t = new Consultas("Geral", "Retorno", "", Convert.ToDateTime("2021-04-27"),  paciente1.Id, medico6.Id);
            //var consulta_j = new Consultas("Geral", "Checkup", "", Convert.ToDateTime("2021-04-21"),  paciente1.Id, medico6.Id);

            //var consulta_u = new Consultas("Dentista", "Consuta Médica", "", Convert.ToDateTime("2021-04-30"), paciente1.Id, medico4.Id);
            //var consulta_v = new Consultas("Dentista", "Consuta Médica", "", Convert.ToDateTime("2021-04-30"),  paciente1.Id, medico4.Id);
            //var consulta_x = new Consultas("Dentista", "Consuta Médica", "", Convert.ToDateTime("2021-04-30"),  paciente1.Id, medico4.Id);


            //modelBuilder.Entity<Consultas>().HasData(consulta_a, consulta_n);
            //    //, consulta_o, consulta_r, consulta_c, consulta_k, consulta_l, consulta_d, consulta_e,
            //    //                                     consulta_f, consulta_g, consulta_h, consulta_i, consulta_p, consulta_q, consulta_b, consulta_s, consulta_t,
            //    //                                     consulta_j, consulta_u, consulta_v, consulta_x, consulta_m);


            //var presc_1 = new Prescricao("Prescricao Consulta Para Paciente ", true, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_a.Id);
            //var presc_2 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_n.Id);
            //var presc_3 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_o.Id);
            //var presc_4 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_r.Id);
            //var presc_5 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_c.Id);
            //var presc_6 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_k.Id);
            //var presc_7 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_l.Id);
            //var presc_8 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_e.Id);
            //var presc_9 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_f.Id);
            //var presc_10 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_g.Id);
            //var presc_11 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_h.Id);
            //var presc_12 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_i.Id);

            //var presc_13 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_p.Id);
            //var presc_14 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_q.Id);
            //var presc_15 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_s.Id);
            //var presc_16 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_t.Id);
            //var presc_17 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_j.Id);
            //var presc_18 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_u.Id);
            //var presc_19 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_v.Id);
            //var presc_20 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_x.Id);
            //var presc_21 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_m.Id);
            //var presc_22 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_b.Id);
            //var presc_24 = new Prescricao("Prescricao Consulta Para Paciente ", false, "Consulta Teste", DateTime.Now, Convert.ToDateTime("05-29-2021"), consulta_d.Id);

            //modelBuilder.Entity<Consultas>().HasData(presc_1, presc_2);
                //, presc_3, presc_4, presc_5, presc_6, presc_7, presc_8, presc_9, presc_10, presc_11, presc_12,
                //                                    presc_13, presc_14, presc_15, presc_16, presc_17, presc_18, presc_19, presc_20, presc_21, presc_22, presc_24);

        }
    }
}
