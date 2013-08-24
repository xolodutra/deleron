using System;
using System.Drawing;
using System.Windows.Forms;
using gma.System.Windows;

namespace BoltClient
{
    public partial class BlockedForm : Form
    {
        private bool blocked = false;
        UserActivityHook actHook = new UserActivityHook();

        public BlockedForm()
        {
            InitializeComponent();

            TopMost = true;

            mainTextLabel.Left =
                (SystemInformation.PrimaryMonitorSize.Width - mainTextLabel.Width) / 2;
            mainTextLabel.Top =
                (SystemInformation.PrimaryMonitorSize.Height - mainTextLabel.Height) / 2;
        }

        public void Off()
        {
            if (!blocked) return;
            blocked = false;

            UnBlockKeyboard();
            UnBlockSreen();
        }

        public void On()
        {
            if (blocked) return;
            blocked = true;

            BlockKeyboard();
            BlockScreen();
        }

        private void BlockScreen()
        {
            ShowDialog();
        }

        private void UnBlockSreen()
        {
            Hide();
        }

        private void BlockKeyboard()
        {
            actHook.Start();
        }

        private void UnBlockKeyboard()
        {
            actHook.Stop();
        }
    }
}