
using Peasant.Console;

namespace Peasant.Desktop
{
    internal static class Program
    {
        private static Service _service = null;
        public static Service service
        {
            get
            {
                if(_service == null)
                {
                    _service = new Service(new string[0]);
                }
                return _service;
            }
        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new ControlPanel());
        }

        public static void StartService()
        {
            Program.service.Start();
        }

        public static void StopService()
        {
            Program.service.Stop();
        }
    }
}