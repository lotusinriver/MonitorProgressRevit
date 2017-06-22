using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonitorProgressCSharp.Views
{
    /// <summary>
    /// Interaction logic for ProgressStatusUI.xaml
    /// </summary>
    public partial class ProgressStatusUI : Window
    {
        public bool ProcessCancelled { get; set; }

        private int currenPercent { get; set; }
        private string currentMessage { get; set; }
        
        private delegate void ProgressBarDelegate();


        public ProgressStatusUI()
        {
            InitializeComponent();
        }

        public void UpdateStatus(string message, int percentage)
        {
            currenPercent = percentage;
            currentMessage = message;

            Dispatcher.Invoke(new ProgressBarDelegate(DoEvents), System.Windows.Threading.DispatcherPriority.Background);
        }

        public void DoEvents()
        {
            progress.Value = currenPercent;
            txtStatus.Text = currentMessage;
        }

        public void JobCompleted()
        {
            btnOk.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Collapsed;

            if (ProcessCancelled)
                txtStatus.Text = "Job cancelled by the user!";
            else
                txtStatus.Text = "Job completed!";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ProcessCancelled = true;
            btnCancel.Visibility = Visibility.Collapsed;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ProcessCancelled = true;
        }
    }
}
