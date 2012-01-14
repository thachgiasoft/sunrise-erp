using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

using Sunrise.ERP.Lang;

namespace Sunrise.ERP.BasePublic
{
    /// <summary>
    /// 系统公共方法类
    /// </summary>
    public class SysPublic
    {
        /// <summary>
        /// 检测控件Text值是否为空
        /// </summary>
        /// <param name="ctl">需要验证的控件</param>
        /// <param name="title">提示内容</param>
        /// <returns>True-为空，False-不为空</returns>
        public static bool CheckNotNull(Control ctl,string title)
        {
            if (ctl.Text == "")
            {
                Sunrise.ERP.BaseControl.Public.SystemInfo(title + LangCenter.Instance.GetSystemMessage("NotNull"));
                return true;
            }
            else
            {
                return false;
            }

        }        

        /// <summary>
        /// 取得当前的中文星期几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ChsWeek(DateTime date)
        {
            string week = date.DayOfWeek.ToString();
            switch (week)
            {
                case "Monday": return "星期一";
                case "Tuesday": return "星期二";
                case "Wednesday": return "星期三";
                case "Thursday": return "星期四";
                case "Friday": return "星期五";
                case "Saturday": return "星期六";
                default: return "星期日";
            }
        }

        /// <summary>
        /// 取得当前时间的农历日期
        /// </summary>
        /// <returns></returns>
        public static string CNDate()
        {
            System.Globalization.ChineseLunisolarCalendar l = new System.Globalization.ChineseLunisolarCalendar();
            DateTime dt = DateTime.Today; //转换当日的日期
            string[] aMonth = { "", "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "腊月", "腊月" };

            string[] a10 = { "初", "十", "二十", "三十" };
            string[] aDigi = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string sYear = "", sYearArab = "", sMonth = "", sDay = "", sDay10 = "", sDay1 = "", sLuniSolarDate = "";
            int iYear, iMonth, iDay;
            iYear = l.GetYear(dt);
            iMonth = l.GetMonth(dt);
            iDay = l.GetDayOfMonth(dt);

            //Format Year
            sYearArab = iYear.ToString();
            for (int i = 0; i < sYearArab.Length; i++)
            {
                sYear += aDigi[Convert.ToInt16(sYearArab.Substring(i, 1))];
            }

            //Format Month
            int iLeapMonth = l.GetLeapMonth(iYear); //获取闰月

            if (iLeapMonth > 0 && iMonth <= iLeapMonth)
            {
                aMonth[iLeapMonth] = "闰" + aMonth[iLeapMonth - 1];
                sMonth = aMonth[l.GetMonth(dt)];
            }
            else if (iLeapMonth > 0 && iMonth > iLeapMonth)
            {
                sMonth = aMonth[l.GetMonth(dt) - 1];
            }
            else
            {
                sMonth = aMonth[l.GetMonth(dt)];
            }


            //Format Day
            sDay10 = a10[iDay / 10];
            sDay1 = aDigi[(iDay % 10)];
            sDay = sDay10 + sDay1;

            if (iDay == 10) sDay = "初十";
            if (iDay == 20) sDay = "二十";
            if (iDay == 30) sDay = "三十";

            //Format Lunar Date
            sLuniSolarDate = sMonth + sDay;
            return sLuniSolarDate;
        }

        /// <summary>
        /// DataRowView转换为DataTable
        /// </summary>
        /// <param name="drv">DataRowView</param>
        /// <returns>DataTable</returns>
        public static DataTable ConvertDataRowViewToTable(DataRowView drv)
        {
            DataTable dt = drv.DataView.Table.Clone();
            DataRow dr = dt.NewRow();
            for (int i = 0; i < drv.DataView.Table.Columns.Count; i++)
            {
                dr[i] = drv.Row[i];
            }
            dt.Rows.Add(dr);
            return dt;
        }
    }
}
