using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _4_2
{
    //定义滴答事件委托
    public delegate void TickEventHandler(object sender, EventArgs e);
    //定义响铃事件委托
    public delegate void AlarmEventHandler(object sender, EventArgs e);

    public class AlarmClock
    {
        //定义滴答事件
        public event TickEventHandler Tick;
        //定义响铃事件
        public event AlarmEventHandler Alarm;

        private int alarmTime;

        public AlarmClock(int alarmTime)
        {
            this.alarmTime = alarmTime;
        }
        public void Start()
        {
            int currentTime = 0;
            while(currentTime<alarmTime)
            {
                Thread.Sleep(1000);//指定线程暂停的时间长度是1000毫秒
                Tick(this,EventArgs.Empty);
                currentTime++;

            }
            Alarm(this, EventArgs.Empty);
        }
    }
    public class Form
    {
        public AlarmClock clock = new AlarmClock(5);
        public Form()
        {
            clock.Tick += OnTick;
            clock.Alarm += OnAlarm;
        }
        void OnTick(object sender, EventArgs e)
        {
            Console.WriteLine("滴答……");
        }
        void OnAlarm(object sender,EventArgs e)
        {
            Console.WriteLine("时间到！");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.clock.Start();
            Console.ReadKey();
        }
    }
}
