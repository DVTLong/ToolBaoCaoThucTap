using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBaoCaoThucTap
{
    public class TuanThucTap
    {
        private int week;
        private DateTime startDay;
        private DateTime endDay;

        public TuanThucTap(int week, DateTime start, DateTime end)
        {
            this.Week = week;
            this.StartDay = start;
            this.EndDay = end;
        }

        public int Week { get => week; set => week = value; }
        public DateTime StartDay { get => startDay; set => startDay = value; }
        public DateTime EndDay { get => endDay; set => endDay = value; }
    }
}
