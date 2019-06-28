using System;
using System.Net.Http;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;
using System.Threading;

namespace TestDemo.FunctionTest
{
    public class AsyncForm : Form
    {
        const string URL = "https://auth.alipay.com/login/index.htm";
        Button button;
        Label label;

        public AsyncForm()
        {
            button = new Button() { Location = new Point(10, 15), Text = "Click" };
            button.Click += DisplayWebSiteLength;
            label = new Label() { Location = new Point(10, 40), Height = 300, Width = 200, Text = "Length" };
            Controls.Add(button);
            Controls.Add(label);
            AutoSize = true;
            CenterToScreen();
        }

        async void DisplayWebSiteLength(object sender, EventArgs e)
        {
            label.Text = "Fetching...";
            HttpClient client = new HttpClient();
            try
            {
                //string text = await client.GetStringAsync(URL);

                //Task<string> task = client.GetStringAsync(URL);  //Start wait
                //string text = await task;

                //Task<string> task = Task.Run(() => client.GetStringAsync(URL));
                //string text = await task;  //Start wait

                Task<string> task = Task.Factory.StartNew<string>(() => DelayReturn(client, URL).Result);
                string text = await task;  //Start wait
                label.Text = text.Length.ToString() + "\n " + text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n", ex);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
            }
        }

        Task<string> DelayReturn(HttpClient client, string url)
        {
            Thread.Sleep(2 * 1000);
            return client.GetStringAsync(url);
        }
    }
}
