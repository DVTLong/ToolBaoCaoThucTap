using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolBaoCaoThucTap
{
    public class DanhSachTuan
    {
        private List<TuanThucTap> list;

        public DanhSachTuan()
        {
            list = new List<TuanThucTap>();
            DateTime start = new DateTime(2019, 5, 6);
            DateTime end = new DateTime(2019, 5, 12);
            for (int w = 1; w <= 10; w++)
            {
                TuanThucTap tuan = new TuanThucTap(w, start, end);
                start = start.AddDays(7);
                end = end.AddDays(7);
                list.Add(tuan);
            }            
        }

        public int GetWeekNow()
        {
            DateTime now = DateTime.Now;
            foreach (TuanThucTap tuan in list)
            {
                if (tuan.StartDay <= now && now <= tuan.EndDay)
                {
                    return tuan.Week;
                }
            }
            return -1;
        }
    }
}
