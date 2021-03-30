using System;
using System.Windows.Forms;
using SpectrumLook.Views;
using System.IO;
using PRISM;

namespace SpectrumLook
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // setup unhandled exception handling
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // setup and run the form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        // Handles the Exception by displaying an error message to the user
        private static void HandleException(Exception ex)
        {
            var result = DialogResult.Cancel;
            try
            {
                // Writes bugs into  ~\spectrumlook\Prototype4\SpectrumLook\bin\buglog.txt before displaying
                var exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                var sw = new StreamWriter(exeFolder + "\\buglog.txt", true);
                sw.WriteLine("Bug Date: " + DateTime.Now);
                sw.WriteLine(ex.ToString() + "\n\n");

                sw.Close();
                result = ShowThreadExceptionDialog("Error", ex);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Error",
                        "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }

            // Exits the program when the user clicks Abort.
            if (result == DialogResult.Abort)
                Application.Exit();
        }

        // Creates the error message and displays it.
        private static DialogResult ShowThreadExceptionDialog(string title, Exception ex)
        {
            var errorMsg = string.Format(
                "An application error occurred:\n\n{0}\n\nStack Trace:\n{1}",
                ex.Message,
                StackTraceFormatter.GetExceptionStackTraceMultiLine(ex));

            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}
