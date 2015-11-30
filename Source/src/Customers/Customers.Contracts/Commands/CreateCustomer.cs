using System;
using NServiceBus;

namespace HomeManager.Customers.Contracts.Commands
{
    public class CreateCustomer : ICommand
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}
