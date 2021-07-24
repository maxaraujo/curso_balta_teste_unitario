using System;
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