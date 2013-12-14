using System;
using System.Collections.Generic;
using System.Text;
using OpenHardwareMonitor.Hardware;
using OpenHardwareMonitor.Collections;

namespace CpuTempService
{
    public class CpuTemperature
    {
        private Computer m_computer;

        public CpuTemperature()
        {
            m_computer = new Computer();
            m_computer.CPUEnabled = true;
            m_computer.FanControllerEnabled = false;
            m_computer.GPUEnabled = false;
            m_computer.HDDEnabled = false;
            m_computer.MainboardEnabled = false;
            m_computer.RAMEnabled = false;
        }

        public void run()
        {
            m_computer.HardwareAdded += new HardwareEventHandler(HardwareAdded);
            m_computer.HardwareRemoved += new HardwareEventHandler(HardwareRemoved);
            m_computer.Open();
        }

        public void accept()
        {
            UpdateVisitor vis = new UpdateVisitor();
            m_computer.Accept(vis);
        }

        public void close()
        {
            m_computer.Close();
        }

        private void HardwareAdded(IHardware hardware)
        {
            foreach (IHardware subHardware in hardware.SubHardware)
            {
                // System.Console.WriteLine(subHardware.Name);
            }
        }

        private void HardwareRemoved(IHardware hardware)
        {
            WeiboString.log(hardware.Name);
            foreach (IHardware subHardware in hardware.SubHardware)
            {
                // System.Console.WriteLine(subHardware.Name);
            }
        }
    }

    public class WeiboString
    {
        public static string creator(IHardware hardware)
        {
            StringBuilder r = new StringBuilder();
            r.AppendFormat("主人，我是{0}，我现在温度是：", hardware.Name);
            int index = 0;
            foreach (ISensor s in hardware.Sensors)
            {
                if (s.SensorType == SensorType.Temperature)
                {
                    if (index > 0) { r.Append(" | "); }
                    r.AppendFormat("{0}({1:F1}°C)", s.Name.Replace("#", ""), s.Value);
                    ++index;
                }
            }
            return r.ToString();
        }

        public static string log_filename()
        {
            return "weibo_log.txt";
        }

        public static void log(string log_msg)
        {
            string strTempFileName = System.IO.Path.GetTempPath() + WeiboString.log_filename();
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(strTempFileName, true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + log_msg);
            }
        }
    }
}
