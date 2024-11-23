using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowSharingHider
{
    public partial class MainWindow : Form
    {
        public class WindowInfo
        {
            public string Title { get; set; }
            public IntPtr Handle { get; set; }
            public bool stillExists = false;

            public override string ToString()
            {
                return Title;
            }
        }

        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenu;
        private bool flagToPreserveSettings = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
            var timer = new Timer();
            timer.Interval = 500;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeTrayIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                Visible = true,
                Text = "Window Sharing Hider"
            };

            contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Открыть", null, ShowWindow);
            contextMenu.Items.Add("Выход", null, ExitApplication);

            notifyIcon.ContextMenuStrip = contextMenu;

            notifyIcon.DoubleClick += (sender, e) => ShowWindow(sender, e);
        }

        private void ShowWindow(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (WindowInfo window in windowListCheckBox.Items)
                window.stillExists = false;

            var currWindows = WindowHandler.GetVisibleWindows();
            foreach (var window in currWindows)
            {
                var existingWindow = windowListCheckBox.Items.Cast<WindowInfo>().FirstOrDefault(i => i.Handle == window.Key);
                if (existingWindow != null)
                {
                    existingWindow.stillExists = true;
                    existingWindow.Title = window.Value;
                }
                else
                {
                    windowListCheckBox.Items.Add(new WindowInfo { Title = window.Value, Handle = window.Key, stillExists = true });
                }
            }

            foreach (var window in windowListCheckBox.Items.Cast<WindowInfo>().ToArray())
            {
                if (!window.stillExists)
                    windowListCheckBox.Items.Remove(window);
            }

            foreach (var window in windowListCheckBox.Items.Cast<WindowInfo>().ToArray())
            {
                var status = WindowHandler.GetWindowDisplayAffinity(window.Handle) > 0;
                var target = windowListCheckBox.GetItemChecked(windowListCheckBox.Items.IndexOf(window));
                if (target != status && flagToPreserveSettings)
                {
                    WindowHandler.SetWindowDisplayAffinity(window.Handle, target ? 0x11 : 0x0);
                    status = WindowHandler.GetWindowDisplayAffinity(window.Handle) > 0;
                }
                windowListCheckBox.SetItemChecked(windowListCheckBox.Items.IndexOf(window), status);
            }

            flagToPreserveSettings = true;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
            base.OnFormClosing(e);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }
    }
}