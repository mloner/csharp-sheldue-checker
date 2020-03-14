using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SheldueChecker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public string GetTime()
        {
            string urlAddress = "http://www.vstu.ru/student/raspisaniya/zanyatiy/index.php?dep=fevt";
            string data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            string seq = "ОН_ФЭВТ_2 курс.xls\">2 курс.xls</a> (";
            data = data.Substring(data.IndexOf(seq) + seq.Length, 40);
            return data;
        }

        private void button_clicked(object sender, EventArgs e)
        {
            label1.Text = GetTime();
        }
    }
}
