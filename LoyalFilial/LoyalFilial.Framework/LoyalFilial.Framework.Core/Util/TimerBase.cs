using System;
using System.Collections.Generic;
using System.Timers;

namespace LoyalFilial.Framework.Core.Util
{
    /// <summary>
    /// 定时任务基类
    /// </summary>
    public abstract class TimerBase<T> : MarshalByRefObject
    {

        protected Queue<T> MessageQueue { get; private set; }

        /// <summary>
        /// 无参构造方法
        /// </summary>
        protected TimerBase()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="interval">定时器间隔时间（毫秒）</param>
        protected TimerBase(int interval)
        {
            this.interval = interval;
        }

        private Timer scheduleTimer;
        private int timerStartCount;
        private readonly object lockTimer = new object();

        /// <summary>
        /// 日志定时器
        /// </summary>
        protected virtual Timer ScheduleTimer
        {
            get
            {
                if (this.scheduleTimer == null)
                {
                    lock (this.lockTimer)
                    {
                        if (this.scheduleTimer == null)
                        {
                            this.scheduleTimer = new Timer { AutoReset = false };
                        }
                    }
                }
                return this.scheduleTimer;
            }
        }

        private int interval = 1000;

        /// <summary>
        /// 定时器间隔（毫秒）
        /// </summary>
        protected virtual int Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }

        /// <summary>
        /// 开始定时
        /// </summary>
        protected virtual void StartTimer()
        {
            if (this.Interval > 0)
            {
                if (this.scheduleTimer == null)
                {
                    this.ScheduleTimer.Interval = this.Interval;
                    this.ScheduleTimer.Elapsed += this.OnTimer;
                }
                try
                {
                    this.ScheduleTimer.Start();
                }
                catch (ObjectDisposedException)
                {
                    this.scheduleTimer = null;
                    this.timerStartCount++;
                    if (this.timerStartCount <= 3)
                    {
                        this.StartTimer();
                    }
                    else
                    {
                        throw;
                    }
                }
                this.timerStartCount = 0;
            }
        }

        /// <summary>
        /// 时间到了后引发的操作
        /// </summary>
        /// <param name="sender"><see cref="System.Timers.Timer"/></param>
        /// <param name="e"><see cref="ElapsedEventArgs"/></param>
        protected virtual void OnTimer(object sender, ElapsedEventArgs e)
        {
            this.ScheduleTimer.Stop();
            if (this.OnTimer(e.SignalTime))
            {
                this.ScheduleTimer.Start();
            }
        }

        /// <summary>
        /// 时间到了后引发的操作
        /// </summary>
        /// <param name="signalTime">引发时的时间戳</param>
        /// <returns>是否继续定时</returns>
        protected virtual bool OnTimer(DateTime signalTime)
        {
            return this.OnTimerInternal();
        }

        /// <summary>
        /// 时间到了后引发的操作
        /// </summary>
        /// <returns>是否继续定时</returns>
        protected virtual bool OnTimerInternal()
        {
            if (this.MessageQueue != null)
            {
                var list = new List<T>();
                lock (this.MessageQueue)
                {
                    while (this.MessageQueue.Count > 0)
                    {
                        list.Add(this.MessageQueue.Dequeue());
                    }
                }
                foreach (var message in list)
                {
                    OnTimer(message);
                }
            }
            return true;
        }

        /// <summary>
        /// 时间到了后引发的操作
        /// </summary>
        protected abstract void OnTimer(T t);

        /// <summary>
        /// 添加待处理实例对象
        /// </summary>
        /// <param name="t">待处理对象</param>
        /// <returns>是否添加成功</returns>
        public virtual bool AddMessage(T t)
        {
            if (this.MessageQueue == null)
            {
                this.MessageQueue = new Queue<T>();
            }

            lock (this.MessageQueue)
            {
                this.MessageQueue.Enqueue(t);
            }
            this.StartTimer();
            return true;
        }
    }
}
