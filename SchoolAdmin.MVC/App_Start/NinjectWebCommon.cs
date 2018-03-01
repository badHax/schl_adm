[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SchoolAdmin.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(SchoolAdmin.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace SchoolAdmin.MVC.App_Start
{
    using log4net;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using SchoolAdmin.DataAccess.DbContext;
    using SchoolAdmin.DataAccess.Interfaces;
    using SchoolAdmin.DataAccess.Repositories;
    using SchoolAdmin.Domain;
    using SchoolAdmin.Services;
    using SchoolAdmin.Util.Interfaces;
    using System;
    using System.Reflection;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //database context
            kernel.Bind<ISchoolDbContext>().To<SchoolDbContext>();

            //repositoies
            kernel.Bind<IStudentRepository>().To<StudentRepository>();
            kernel.Bind<ICourseRepository>().To<CourseRepository>();
            kernel.Bind<ITeacherRepository>().To<TeacherRepository>();
            kernel.Bind<IGradeRepository>().To<GradeRepository>();
            kernel.Bind<ICourseEnrollmentRepository>().To<CourseEnrollmentRepository>();

            //services
            kernel.Bind<IGradeService>().To<GradeService>();
            kernel.Bind<IStudentService>().To<StudentService>();
            kernel.Bind<ICourseService>().To<CourseService>();
            kernel.Bind<ITeacherService>().To<TeacherService>();
            kernel.Bind<IGPACalculator1>().To<GPACalculator42Scale>();

            //logger
            kernel.Bind<ILog>().ToMethod(l => LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType)).InSingletonScope();
        }        
    }
}
