using System;
using System.Timers;
using System.IO;
using System.ServiceProcess;
using System.Runtime.InteropServices;

namespace BoltClient
{
    class EngineTimer
    {
        const int defaultCheckStateInterval = 5 * 1000;
        const int quickCheckStageInterval = 2 * 1000;

        bool started = false;
        bool connected = true;
        int timeCounter = 0;

        System.Timers.Timer tmrCheckState = new System.Timers.Timer();
        System.Timers.Timer tmrSendBattaryState = new System.Timers.Timer();
        System.Timers.Timer tmrCheckFeederState = new System.Timers.Timer();

        BlockedForm bForm = new BlockedForm();
        ApacheConductor connectConductor = new ApacheConductor();

        public EngineTimer()
        {
            tmrCheckState.Elapsed += new ElapsedEventHandler(tmrCheckState_Elapsed);
            tmrCheckState.Interval = defaultCheckStateInterval;

            tmrSendBattaryState.Elapsed += new ElapsedEventHandler(tmrSendBattaryState_Elapsed);
            tmrSendBattaryState.Interval = 15 * 1000; // 0,25 min

            tmrCheckFeederState.Elapsed += new ElapsedEventHandler(tmrCheckFeederState_Elapsed);
            tmrCheckFeederState.Interval = 200;
        }

        public void Start()
        {
            tmrCheckState.Start();
            tmrSendBattaryState.Start();
            tmrCheckFeederState.Start();
        }

        public void Stop()
        {
            bForm.On();
            tmrCheckState.Stop();
            tmrSendBattaryState.Stop();
            tmrCheckFeederState.Stop();
        }

        [DllImport("advapi32.dll", EntryPoint = "InitiateSystemShutdownEx")]
        private static extern int InitiateSystemShutdownEx(
            string lpMachineName,
            string lpMessage,
            int dwTimeout,
            bool bForceAppsClosed,
            bool bRebootAfterShutdown,
            int dwReason);

        public void gotoShutdown()
        {
            Stop();
            try
            {
                connectConductor.SendShutDownNow();
            }
            catch (Exception e)
            {
            }
        }

        private void tmrCheckFeederState_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                ServiceController sc = new ServiceController(Params.FeederName);
                if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    sc.Start();

                }
            }
            catch (Exception ex)
            {
                InitiateSystemShutdownEx("127.0.0.1", ex.Message, 1, true, true, 1);
            }
        }

        private void tmrCheckState_Elapsed(object sender, ElapsedEventArgs e)
        {
            int state = 0;
            try
            {
                if (!started)
                {
                    connectConductor.SendShutUp();
                    started = true;
                }
                state = connectConductor.CheckState();
            }
            catch (DisconnectException de)
            {
                ChangeToConnectIsOff();
                return;
            }

            if (state == 1) bForm.Off();
            else if (state == 2)
            {
                InitiateSystemShutdownEx("127.0.0.1", "Shutdown signal", 1, true, true, 1);
                return;
            }
            else bForm.On();

            ChangeToConnectIsOn();
        }

        private void tmrSendBattaryState_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                connectConductor.SendBattaryState();
            }
            catch (DisconnectException de)
            {
                ChangeToConnectIsOff();
            }
        }

        private void ChangeToConnectIsOff()
        {
            if (timeCounter < 60 * 1000)
            {
                timeCounter += (int)tmrCheckState.Interval;
                return;
            }

            if (!connected) return;
            connected = false;

            bForm.On();
            //tmrSendBattaryState.Stop();
            tmrCheckState.Interval = quickCheckStageInterval;
        }

        private void ChangeToConnectIsOn()
        {
            timeCounter = 0;

            if (connected) return;
            connected = true;

            //tmrSendBattaryState.AutoReset = true;
            //tmrSendBattaryState.Start();
            tmrCheckState.Interval = defaultCheckStateInterval;
        }
    }
}
