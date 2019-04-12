using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace TestDemo.FunctionTest
{
    public class AsyncForm : Form
    {
        Label label;
        Button button;

        public AsyncForm()
        {
            label = new Label() { Location = new Point(10, 20), Height = 40, Text = "Length" };
            button = new Button() { Location = new Point(10, 50), Text = "Click" };
            button.Click += DisplayWebSiteLength;
            AutoSize = true;
            Controls.Add(label);
            Controls.Add(button);
        }

        async void DisplayWebSiteLength(object sender, EventArgs e)
        {
            label.Text = "Fetching...";
            using (HttpClient client = new HttpClient())
            {
                //string text = await client.GetStringAsync("https://auth.alipay.com/login/index.htm");

                //Task<string> task = client.GetStringAsync("https://auth.alipay.com/login/index.htm");  //Start wait
                //string text = await task;

                Task<string> task = Task.Run(() => client.GetStringAsync("https://auth.alipay.com/login/index.htm"));
                string text = await task;  //Start wait

                label.Text = text.Length.ToString() + " " + text;
            }
        }
    }
}
