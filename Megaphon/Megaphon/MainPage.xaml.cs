using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Megaphon.Services;

namespace Megaphon
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            iStream = DependencyService.Get<IAudioStream>();
            
        }
        private IAudioStream iStream;

        bool running = false;
        private async void BTN_StartStop_Clicked(object sender, EventArgs e)
        {
            if (!running)
            {
                iStream.Start();
                BTN_StartStop.Text = "Stop";
            } else
            {
                iStream.Stop();
                BTN_StartStop.Text = "Start";
            }
            running = !running;
        }
    }
}
