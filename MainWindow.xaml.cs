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
using EasyModbus;
using System.Windows.Threading;

namespace SAW_TRAY_IO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModbusClient modbusClient;
        bool Connect_flag;
        public MainWindow()
        {
            InitializeComponent();
            modbusClient = new ModbusClient(tb_Modbus_Server_IP.Text, int.Parse(tb_Modbus_Server_Port.Text));
            modbusClient.LogFileFilename = "Modbus_IO_Log.txt";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)(sender);
            string content_bt = bt.Content.ToString();
            switch (content_bt)
            {
                case "Connect":
                    try
                    {
                        // update latest IP and Port
                        modbusClient = new ModbusClient(tb_Modbus_Server_IP.Text, int.Parse(tb_Modbus_Server_Port.Text));
                        modbusClient.LogFileFilename = "Modbus_IO_Log.txt";
                        // do connection
                        modbusClient.Connect();
                        //modbusClient.ConnectionTimeout = 5000;
                        lb_Connection_Status.Content = "Connected!";
                        Connect_flag = true;
                        // Read all Coils and update to check boxs
                        bool[] _ReadCoils = modbusClient.ReadCoils(0, 8);
                        CheckBox[] _CheckBoxes = { Cb_DO_00, Cb_DO_01, Cb_DO_02, Cb_DO_03, Cb_DO_04, Cb_DO_05, Cb_DO_06, Cb_DO_07 };
                        int _index = 0;
                        foreach (CheckBox _checkBox in _CheckBoxes)
                        {
                            _checkBox.IsChecked = _ReadCoils[_index];
                            _index++;
                        }
                    }
                    catch (Exception)
                    {
                        lb_Connection_Status.Content = "Fail to Connect!";
                    }
                    break;
                case "Disconnect":
                    try
                    {
                        modbusClient.Disconnect();
                        lb_Connection_Status.Content = "Disconnected!";
                        Connect_flag = false;
                    }
                    catch(Exception)
                    {
                        lb_Connection_Status.Content = "Fail to Disconnect!";
                    }
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(100);
            dt.Tick += dtTicker;
            dt.Start();
        }

        private void dtTicker(object sender, EventArgs e)
        {
            if (Connect_flag == true)
            {
                CheckBox[] _Checkbox = { Cb_DI_00, Cb_DI_01, Cb_DI_02, Cb_DI_03, Cb_DI_04, Cb_DI_05, Cb_DI_06, Cb_DI_07 };
                // Read all Coils and update to check boxs
                bool[] _ReadCoils = modbusClient.ReadDiscreteInputs(0, 8);
                int _index = 0;
                foreach (CheckBox _CheckBox in _Checkbox)
                {
                    _CheckBox.IsChecked = _ReadCoils[_index];
                    _index++;
                    
                }
            }
        }

        // DO CheckBox_Unchecked
        private void Cb_DO_00_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(0, true);
        }

        private void Cb_DO_01_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(1, true);
        }

        private void Cb_DO_02_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(2, true);
        }

        private void Cb_DO_03_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(3, true);
        }

        private void Cb_DO_04_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(4, true);
        }

        private void Cb_DO_05_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(5, true);
        }

        private void Cb_DO_06_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(6, true);
        }
        private void Cb_DO_07_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(7, true);
        }

        // DO CheckBox_Unchecked
        private void Cb_DO_00_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(0, false);
        }

        private void Cb_DO_01_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(1, false);
        }
        private void Cb_DO_02_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(2, false);
        }
        private void Cb_DO_03_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(3, false);
        }
        private void Cb_DO_04_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(4, false);
        }
        private void Cb_DO_05_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(5, false);
        }
        private void Cb_DO_06_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(6, false);
        }
        private void Cb_DO_07_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            modbusClient.WriteSingleCoil(7, false);
        }
    }
}
