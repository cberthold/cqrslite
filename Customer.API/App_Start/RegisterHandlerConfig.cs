using CQRSlite.Bus;
using CQRSlite.Config;
using Customer.BoundedContext.Handlers;
using Customer.BoundedContext.ReadModel.Handlers;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Customer.API
{
    public partial class Startup
    {
        public void ConfigureCommandHandlers(IAppBuilder app)
        {
            var busResolver = new BusResolver(DependencyResolver.Current);
            var registrar = new BusRegistrar(busResolver);
            registrar.Register(typeof(CustomerCommandHandlers));

            var handlerRegistrar = DependencyResolver.Current.GetService<IHandlerRegistrar>();
            
           
        }
    }

    public class BusResolver : IServiceLocator
    {

        private IDependencyResolver _resolver;

        public BusResolver(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }

        public object GetService(Type type)
        {
            var service = _resolver.GetService(type);
            return service;
        }

        public T GetService<T>()
        {
            var service = _resolver.GetService<T>();
            return service;
        }
    }
}
