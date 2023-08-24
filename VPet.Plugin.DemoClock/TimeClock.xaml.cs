using Panuon.WPF.UI;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using static VPet.Plugin.DemoClock.DemoClock;
using System.Timers;
using LinePutScript.Localization.WPF;
using System.Diagnostics;

namespace VPet.Plugin.DemoClock
{
    /// <summary>
    /// TimeClock.xaml 的交互逻辑
    /// </summary>
    public partial class TimeClock : UserControl
    {
        DemoClock Master;
        public DispatcherTimer TimeTimer;
        public DispatcherTimer CountTimer;
        public DateTime StartTime;
        public TimeSpan PauseTime;
        Timer CloseTimer;

        public TimeClock(DemoClock master)
        {
            InitializeComponent();
            Master = master;
            Master.MW.Main.UIGrid.Children.Insert(0, this);

            TimeTimer = new DispatcherTimer();
            TimeTimer.Interval = TimeSpan.FromSeconds(1);
            TimeTimer.Tick += TimeTimer_Tick;
            TimeTimer.Start();

            CountTimer = new DispatcherTimer();
            //CountTimer.Interval = TimeSpan.FromSeconds(1);
            CountTimer.Tick += CountTimer_Tick;

            CloseTimer = new Timer()
            {
                Interval = 4000,
                AutoReset = false,
                Enabled = false
            };
            CloseTimer.Elapsed += CloseTimer_Elapsed;

            Opacity = master.Set.Opacity;
            Margin = new Thickness(0, master.Set.PlaceTop, 0, 0);
            Master.MW.Main.MouseEnter += UserControl_MouseEnter;
            Master.MW.Main.MouseLeave += UserControl_MouseLeave;
            TimeTimer_Tick();

        }

        private void CloseTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Opacity = Master.Set.Opacity;
                if (Master.Set.PlaceAutoBack && Master.MW.Main.UIGrid.Children.Contains(this))
                {
                    Master.MW.Main.UIGrid.Children.Remove(this);
                    Master.MW.Main.UIGrid_Back.Children.Add(this);
                }
            });
        }
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Master.Set.PlaceAutoBack && Master.MW.Main.UIGrid_Back.Children.Contains(this))
            {
                Master.MW.Main.UIGrid_Back.Children.Remove(this);
                Master.MW.Main.UIGrid.Children.Insert(0, this);
            }
            Opacity = 0.95;
            CloseTimer.Enabled = false;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseTimer.Start();
        }

        bool TimeSpanChanged = false;
        private void CountTimer_Tick(object sender = null, EventArgs e = null)
        {
            var diff = DateTime.Now - StartTime + PauseTime;
            if (diff.TotalMinutes > 2)
            {
                if (TimeSpanChanged)
                {
                    CountTimer.Interval = TimeSpan.FromSeconds(1);
                    TimeSpanChanged = false;
                }
                getTheProcesses();
            }
        }

        public Process[] getTheProcesses()
        {
            // 检查后台进程，是否在听歌
            Process[] music = Process.GetProcessesByName("cloudmusic");
            if(music.Length == 0)
            {
                TTimes.Text = "正在发呆".ToString();
            }
            foreach (var process in music)
            {
                if (process.ProcessName == "cloudmusic")
                {
                    if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        TTimes.Text = ("正在听 " + process.MainWindowTitle).ToString();
                    }
                }
            }
            return music;
        }

        private void TimeTimer_Tick(object sender = null, EventArgs e = null)
        {
            //相关UI更新
            var diff = DateTime.Now - StartTime + PauseTime;
            if (diff.TotalMinutes > 2)
            {
                if (TimeSpanChanged)
                {
                    CountTimer.Interval = TimeSpan.FromSeconds(1);
                    TimeSpanChanged = false;
                }
                getTheProcesses();
            }
        }
    }
}
