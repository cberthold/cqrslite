using CQRSlite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.BoundedContext.Domain.Api.Services
{
    public class ApiService : IApiService
    {
        private readonly IRepository repository;

        public ApiService(IRepository repository)
        {
            this.repository = repository;
        }

    }
}
