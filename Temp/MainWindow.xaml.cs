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
using System.IO.Ports;
using System.Threading;
using System.Media;
using System.Timers;



namespace Temp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serial = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        System.Timers.Timer checkPort = new System.Timers.Timer();


        public MainWindow()
        {
            InitializeComponent();
            
            serial.Open();
            serial.DataReceived += Serial_DataReceived;
           
            checkPort.Interval = 1000;
            checkPort.Elapsed += CheckPort_Elapsed;
            checkPort.Start();
            
           
             

        }

        private void CheckPort_Elapsed(object sender, ElapsedEventArgs e)
        {
            checkPort.Stop();
            if (!serial.IsOpen)
            {
                this.Dispatcher.Invoke(() =>
                {
                    SolidColorBrush red = new SolidColorBrush();
                    red.Color = Color.FromRgb(255, 0, 0);
                    portStatus.Fill = red;
                });
            }
            checkPort.Start();
        }

        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            String tempo = serial.ReadExisting();
            String[] buff = tempo.Split('-');
            this.Dispatcher.Invoke(() =>
            {
                if (buff.Length > 1)
                {
                    temp.Text = buff[0];
                    humid.Text = buff[1];
                   
                }
                
            });
            
        }

        public void updateUI()
        {
            

        }
    }
}
