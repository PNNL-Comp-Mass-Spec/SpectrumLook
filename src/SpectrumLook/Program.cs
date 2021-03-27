using System;
using System.Windows.Forms;
using SpectrumLook.Views;
using System.IO;

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
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // setup and run the form
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandeException(e.ExceptionObject as Exception);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandeException(e.Exception);
        }

        // Handles the Exception by displaying an error message to the user
        private static void HandeException(Exception e)
        {
            var result = DialogResult.Cancel;
            try
            {
                // Writes bugs into  ~\spectrumlook\Prototype4\SpectrumLook\bin\buglog.txt before displaying
                var exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                var sw = new StreamWriter(exeFolder + "\\buglog.txt", true);
                sw.WriteLine("Bug Date: " + DateTime.Now.ToString());
                sw.WriteLine(e.ToString() + "\n\n");

                sw.Close();
                result = ShowThreadExceptionDialog("Error", e);
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
        private static DialogResult ShowThreadExceptionDialog(string title, Exception e)
        {
            var errorMsg = "An application error occurred. Please contact the adminstrator " +
                           "with the following information:\n\n";
            errorMsg = errorMsg + e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg, title, MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }
    }
}
