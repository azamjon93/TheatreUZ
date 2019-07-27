using Ninject.Modules;

namespace TheatreUZ
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<MSSQLRepository>();
        }
    }
}