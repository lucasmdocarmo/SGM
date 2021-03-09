using SGM.Shared.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Manager.Domain.Entities.Integration
{
    public sealed class Integration :BaseEntity
    {
        public string Sistema { get; set; }
        public Guid ApiKey { get; set; }

    }
}
