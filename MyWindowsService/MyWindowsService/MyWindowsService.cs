using System;
using System.CodeDom;
using System.Globalization;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace MyWindowsService
{
    public partial class MyWindowsService : ServiceBase
    {
        private readonly Timer _timer;
        public MyWindowsService()
        {
            _timer = new Timer(1000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var message = "My service is started!";
            WriteLog(message);
            _timer.Start();
        }

        protected override void OnStop()
        {
            _timer.Stop();
            var message = "My service is stopped!";
            WriteLog(message);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            var message = "My service is running...";
            WriteLog(message);
        }

        private void WriteLog(string message)
        {
            var lines = new string[] { DateTime.Now.ToString(CultureInfo.InvariantCulture) + ": " + message };
            File.AppendAllLines(@"D:\GitHub\WindowsServiceExample\MyWindowsServiceLog.log", lines);
        }
    }
}
