using System;
using System.Windows.Forms;

namespace WindowSharingHider
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.Hide(); // Скрываем окно сразу после его показа
            Application.Run();
        }
    }
}