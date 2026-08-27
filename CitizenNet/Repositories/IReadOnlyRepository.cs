using System;
using System.Collections.Generic;
using System.Text;

namespace CitizenNet.API.Repositories
{
    public interface IReadOnlyRepository<T> where T : class
    {
        Task<T> Get(int index);

        Task<T> Get(string name);
    }
}