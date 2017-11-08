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


namespace Temp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort serial = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);


        public MainWindow()
        {
            InitializeComponent();
            
            serial.Open();
            serial.DataReceived += Serial_DataReceived;
            
           
             

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

        public void updateUI(String Tem, String hum)
        {
            

        }
    }
}
