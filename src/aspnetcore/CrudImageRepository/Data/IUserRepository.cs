using System;
using CrudImageRepository.Models;

namespace CrudImageRepository.Data
{
    public interface IUserRepository
    {
        int Create(User c);

        User Read(int id);

        void Update(User c);

        bool Delete(int id);

        User[] ReadAll();
    }
}