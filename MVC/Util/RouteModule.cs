using Ninject.Modules;
using BLL.Services;
using BLL.Interfaces;

namespace MVC.Util
{
    public class RouteModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRouteService>().To<RouteService>();
        }
    }
}