#region 程序集 Facepunch.Steamworks.Win64, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// D:\GITHUB\VPet\packages\Facepunch.Steamworks.2.3.3\lib\net46\Facepunch.Steamworks.Win64.dll
// Decompiled with ICSharpCode.Decompiler 7.1.0.6543
#endregion

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
using System.Diagnostics;
using Steamworks;

namespace VPet.Plugin.UserStates
{
    public class UserStates : MainPlugin
    {
        public Setting Set;
        public System.Timers.Timer timer;
        public UserStates(IMainWindow mainwin) : base(mainwin)
        {
            timer = new System.Timers.Timer(10000);
            timer.Elapsed += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender = null, EventArgs e = null)
        {
            getTheProcesses();
        }

        public Process[] getTheProcesses()
        {
            // 检查后台进程，是否在听歌
            Process[] music = Process.GetProcessesByName("cloudmusic");
            if (music.Length == 0)
            {
                // https://partner.steamgames.com/doc/api/ISteamFriends#richpresencelocalization
                // steam状态API
                Console.WriteLine("正在发呆".ToString());
                SteamFriends.SetRichPresence("steam_display", "正在发呆".ToString());
            }
            foreach (var process in music)
            {
                if (process.ProcessName == "cloudmusic")
                {
                    if (!string.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        Console.WriteLine(("正在听 " + process.MainWindowTitle).ToString());
                        SteamFriends.SetRichPresence("steam_display", ("正在听 " + process.MainWindowTitle).ToString());
                    }
                }
            }
            return music;
        }

        public override void LoadPlugin()
        {
            var line = MW.Set.FindLine("UserStates");
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

            //foreach (MenuItem mi in WPFTimeClock.CM.Items)
            //    menuItem.Items.Add(mi);
        }
        public override void LoadDIY()
        {
            //MW.Main.ToolBar.MenuDIY.Items.Add(menuItem);
        }

        public override string PluginName => "UserStates";
    }
}
