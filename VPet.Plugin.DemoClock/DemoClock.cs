using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows;
using VPet_Simulator.Windows.Interface;
using System.Windows.Threading;
using LinePutScript.Localization.WPF;

namespace VPet.Plugin.DemoClock
{
    /// <summary>
    /// Demo 时钟
    /// </summary>
    public class DemoClock : MainPlugin
    {

        public enum Mode
        {
            None,
            Timing
        }
        /// <summary>
        /// 当前时钟模式
        /// </summary>
        public TimeClock WPFTimeClock;
        public Setting Set;

        public MenuItem mCountDown;

        public long CountDownLength;
        public DemoClock(IMainWindow mainwin) : base(mainwin)
        {
        }

        public override void LoadPlugin()
        {
            var line = MW.Set.FindLine("DemoClock");
            if (line == null)
            {
                Set = new Setting();
            }
            else
            {
                Set = new Setting(line);
                MW.Set.Remove(line);
            }
            MW.Set.Add(Set);

            WPFTimeClock = new TimeClock(this);

            //foreach (MenuItem mi in WPFTimeClock.CM.Items)
            //    menuItem.Items.Add(mi);
        }
        public override void LoadDIY()
        {
            //MW.Main.ToolBar.MenuDIY.Items.Add(menuItem);
        }

        public override string PluginName => "DemoClock";
    }
}
