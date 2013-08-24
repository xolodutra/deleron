using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Text;

namespace BoltClient
{
    public partial class MainService : ServiceBase
    {
        EngineTimer eTimer = new EngineTimer();

        public MainService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            eTimer.Start();
        }

        //protected override void OnStop()
        //{
        //    eTimer.Stop();
        //}

        //protected override void OnPause()
        //{
        //    eTimer.Stop();
        //}

        //protected override void OnContinue()
        //{
        //    eTimer.Start();
        //}

        protected override void OnShutdown()
        {
            eTimer.gotoShutdown();
            //eTimer.Stop();
        }
    }
}
