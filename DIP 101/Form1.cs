using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace DIP_101
{
    public partial class Form1 : Form
    {
        public delegate void AddListView1(string text);
        ThreadStart threadStart;
        Thread readThread;

        SerialPort _serialPort;

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Getting port available
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                listView1.Items.Add(port);


            threadStart = new ThreadStart(Read_Arduino);
            readThread = new Thread(threadStart);


             _serialPort = new SerialPort(ports[0]);

            _serialPort.BaudRate = 9600;

            _serialPort.ReadTimeout = 5000;
            _serialPort.WriteTimeout = 5000;
            
            _serialPort.StopBits = 0;
            _serialPort.Open();

            listView1.Items.Add(_serialPort.BaudRate.ToString());

            readThread.Start();


        }
        private void Read_Arduino(){
            AddListView1 DelUpdateUITextBox = new AddListView1(AddListView);
            while (true)
            {
                try
                {
                    listView1.BeginInvoke(DelUpdateUITextBox, _serialPort.ReadLine());
                }
                catch (System.InvalidOperationException)
                {

                }
                catch (System.IO.IOException)
                {
                    //TO DO INSERT ERROR MESSAGE SOMEWHERE
                    
                } 
                catch (System.TimeoutException)
                {
                    //TIME OUT ERROR
                }

            }
        }
        public void AddListView(string text)
        {
            this.listView1.Items.Add(text);
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
    }
}
