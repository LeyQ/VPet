using LinePutScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPet.Plugin.UserStates
{
    public class Setting : Line
    {
        public Setting() : base("UserStates", "")
        {
        }
        public Setting(ILine line) : base(line)
        {
        }

        // C#特有的属性
        double tomato_worktime;
        public double Tomato_WorkTime
        {
            get => tomato_worktime;
            set
            {
                tomato_worktime = value;
                SetDouble("tomato_worktime", value);
            }
        }
    }
}
