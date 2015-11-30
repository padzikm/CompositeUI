using System;
using HomeManager.Customers.Contracts.Commands;
using NServiceBus;

namespace HomeManager.Customers.Service
{
    public class CreateCustomerHandler : IHandleMessages<CreateCustomer>
    {
        public void Handle(CreateCustomer message)
        {
            Console.WriteLine("\nReceived CreateCustomer\n");
            Console.WriteLine("Id: {0}", message.Id);
            Console.WriteLine("Name: {0}", message.Name);
            Console.WriteLine("Surname: {0}", message.Surname);
        }
    }
}
