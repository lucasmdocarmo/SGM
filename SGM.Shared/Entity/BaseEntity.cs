using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGM.Shared.Core.Entity
{
    public abstract class BaseEntity : Notifiable
    {
        public Guid Id { get; private set; }
        public DateTime LastUpdated { get; private set; }
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            LastUpdated = DateTime.Now;
        }
    }
}
