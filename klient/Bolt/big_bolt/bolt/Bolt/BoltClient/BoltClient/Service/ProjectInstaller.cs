using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using LMS.Server.WindowsService;

namespace BoltClient
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            // Set InteractiveState checkbox for BoltClient.
            WindowsServiceHelper.ChangeInteractiveState(Params.ServiceName, true);

            // Start "BoltClient".
            //ServiceController scBC = new ServiceController(Params.ServiceName);
            //scBC.Start();

        }
    }

}