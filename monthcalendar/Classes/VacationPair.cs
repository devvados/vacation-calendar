using System;
using System.Xml.Serialization;

namespace MonthCalendar
{
    [System.Serializable]
    public class VacationPair
    {
        private bool isShown;
        private DateTime originalEndDate;
        // private DateTime begin, end;

        public VacationPair(DateTime beg, DateTime en)
        {
            Begin = beg;
            End = en;
            OriginalEndDate = End;
        }
        public VacationPair()
        { }
        [XmlElement("beginDate")]
        public DateTime Begin
        {
            get; set;
        }
        [XmlElement("endDate")]
        public DateTime End
        {
            get; set;
        }
        public bool IsShown
        {
            get
            {
                return isShown;
            }
            set
            {
                isShown = value;
            }
        }

        public DateTime OriginalEndDate
        {
            get
            {
                return originalEndDate;
            }
            set
            {
                originalEndDate = value;
            }
        }

        public static bool operator == (VacationPair v1, VacationPair v2)
        {
            if (v1.Begin == v2.Begin && v1.End == v2.End && v1.OriginalEndDate == v2.OriginalEndDate)
                return true;
            else return false;
        }

        public static bool operator != (VacationPair v1, VacationPair v2)
        {
            if (v1.Begin != v2.Begin || v1.End != v2.End || v1.OriginalEndDate != v2.OriginalEndDate)
                return true;
            else return false;
        }

        public bool IncludedInMonth(DateTime month)
        {
            if (Begin.Year == month.Year || month.Year == OriginalEndDate.Year)
            {
                if (Begin.Month == month.Month)
                    return true;
                if (OriginalEndDate.Month == month.Month)
                    return true;
                if (Begin.Month + 1 == month.Month && month.Month == OriginalEndDate.Month - 1)
                    return true;
            }
            return false;
        }

        public VacationPair CutMonth(DateTime desiredMonth)
        {
            if (Begin.Year == desiredMonth.Year || desiredMonth.Year == OriginalEndDate.Year)
            {
                if (Begin.Month == desiredMonth.Month && OriginalEndDate.Month == desiredMonth.Month)
                {
                    return new VacationPair(Begin, OriginalEndDate);
                }
                if (Begin.Month == desiredMonth.Month)
                {
                    return new VacationPair(Begin, new DateTime(Begin.Year, Begin.Month, DateTime.DaysInMonth(Begin.Year, Begin.Month)));
                }
                if (OriginalEndDate.Month == desiredMonth.Month)
                {
                    return new VacationPair(new DateTime(OriginalEndDate.Year, OriginalEndDate.Month, 1), OriginalEndDate);
                }
                if (Begin.Month == desiredMonth.Month - 1 && OriginalEndDate.Month == desiredMonth.Month + 1)
                {
                    return new VacationPair(
                        new DateTime(desiredMonth.Year, desiredMonth.Month, 1),
                        new DateTime(desiredMonth.Year, desiredMonth.Month, DateTime.DaysInMonth(desiredMonth.Year, desiredMonth.Month)));
                }
                else return null;
            }
            else return null;
        }
    }

}
