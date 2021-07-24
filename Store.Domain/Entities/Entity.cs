using System;
using System.Collections.Generic;
using Flunt.Notifications;

namespace Store.Domain.Entites 
{
    public class Entity : Notifiable
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }

    }
}