using WBIS_2.Modules.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;

namespace WBIS_2.Modules
{
    public class WaitWindowHandler
    {
        private Thread StatusThread = null;

        WaitWindow Popup = null;

        public void Start()
        {
            //create the thread with its ThreadStart method
            this.StatusThread = new Thread(() =>
            {
                try
                {
                    Popup = new WaitWindow();
                    this.Popup.Show();
                    this.Popup.Closed += (lsender, le) =>
                    {
                        //when the window closes, close the thread invoking the shutdown of the dispatcher
                        this.Popup.Dispatcher.InvokeShutdown();
                        this.Popup = null;
                        this.StatusThread = null;
                    };

                    //this call is needed so the thread remains open until the dispatcher is closed
                    System.Windows.Threading.Dispatcher.Run();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
                }
            });

            //run the thread in STA mode to make it work correctly
            this.StatusThread.SetApartmentState(ApartmentState.STA);
            this.StatusThread.Priority = ThreadPriority.Normal;
            this.StatusThread.Start();
        }

        public void Stop()
        {
            while (this.Popup == null)
            { }
            //need to use the dispatcher to call the Close method, because the window is created in another thread, and this method is called by the main thread
            this.Popup.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Popup.Close();
            }));
        }
    }
}
