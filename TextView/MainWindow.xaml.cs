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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIHelper;

namespace TextView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Model = new MyModel();
            this.DataContext = Model;
        }
        private MyModel Model;
        private void onOpen_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenTextFileDialog aDlg = new OpenTextFileDialog();
            if (aDlg.ShowDialog() != true) return;
            try
            {
                Model.Load(aDlg.FileName, aDlg.CurrentEncoding);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onStartFilte_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Model.StartFilte();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void onExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onStartFilte_canExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model != null && Model.CanStartFilte;
        }

        private void onOpenImage_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                Model.OpenImage();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
