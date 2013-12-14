using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace CpuTempService
{
    public partial class CpuTempService : ServiceBase
    {
        public CpuTempService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            WeiboString.log("CpuTempService Start.");
            Thread workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
        }

        protected override void OnStop()
        {
            WeiboString.log("CpuTempService Stop.");
            workerObject.RequestStop();
        }

        private Worker workerObject = new Worker();
    }

    public class Worker
    {
        // This method will be called when the thread is started.
        public void DoWork()
        {
            ct.run();
            while (!_shouldStop)
            {
                ct.accept();
                System.Threading.Thread.Sleep(interval);
            }
            ct.close();
        }
        public void RequestStop()
        {
            _shouldStop = true;
        }
        // Volatile is used as hint to the compiler that this data
        // member will be accessed by multiple threads.
        private volatile bool _shouldStop = false;
        private CpuTemperature ct = new CpuTemperature();
        private const int interval = 1800000;    // 30 minutes
    }
}
