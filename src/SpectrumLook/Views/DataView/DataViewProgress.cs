using System.Windows.Forms;
using System.Threading;

namespace SpectrumLook.Views
{
    public partial class DataViewProgress : Form
    {
        public bool readyToClose;

        public DataViewProgress()
        {
            readyToClose = false;
            InitializeComponent();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
        }

        public void Start()
        {
            this.Show();

            while (!readyToClose)
            {
                Thread.Sleep(10);
            }
        }
    }
}
