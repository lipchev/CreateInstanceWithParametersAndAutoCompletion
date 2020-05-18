using System;
using Catel;
using Catel.IoC;
using Catel.Logging;

namespace CreateInstanceWithParametersAndAutoCompletion
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LogManager.AddDebugListener();

            var myConfig = new MyConfig();

            var dr = DependencyResolverManager.Default.DefaultDependencyResolver;

            var serviceLocator = dr.Resolve<IServiceLocator>();

            serviceLocator.RegisterInstance(myConfig);

            var typeFactory = dr.Resolve<ITypeFactory>();
            try
            {
                // this works
                Argument.IsNotNull("MyBasicPage", typeFactory.CreateInstanceWithParametersAndAutoCompletion<MyBasicPage>());

                var myObject = new MyWorkObject();

                // this does not
                Argument.IsNotNull("MyWorkPage", typeFactory.CreateInstanceWithParametersAndAutoCompletion<MyWorkPage>(myObject));
                // this would work
                Argument.IsNotNull("MyWorkPage", typeFactory.CreateInstanceWithParametersAndAutoCompletion<MyWorkPage>(myConfig, myObject));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }

        public class MyConfig
        {
        }

        public class MyWorkObject
        {
        }

        public class MyBasicPage
        {
            public MyBasicPage(MyConfig config)
            {
            }
        }

        public class MyWorkPage
        {
            public MyWorkPage(MyConfig config, MyWorkObject currentItem)
            {
            }
        }
    }
}