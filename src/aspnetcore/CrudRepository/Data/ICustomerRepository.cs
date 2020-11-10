using System;
using CrudRepository.Models;

namespace CrudRepository.Data
{
    public interface ICustomerRepository
    {
        Guid Create(Customer c);

        Customer Read(Guid id);

        void Update(Customer c);

        bool Delete(Guid id);

        Customer[] ReadAll();
    }
}