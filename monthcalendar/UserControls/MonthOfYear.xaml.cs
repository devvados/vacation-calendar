using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MonthCalendar
{
    public partial class MonthOfYear : UserControl
    {
        List<DayOfMonth> _listDays = new List<DayOfMonth>();

        public DateTime Dt
        {
            get; private set;
        }

        public MonthOfYear(DateTime dt)
        {
            InitializeComponent();

            Dt = dt;

            DateTime curDate = dt;
            int curRow = 1;
            ButMonth.Content = curDate.ToString("MMMM");
            for (var dayNum = 0; dayNum < DateTime.DaysInMonth(curDate.Year, dt.Month); dayNum++)
            {
                DayOfMonth dayOfMonth = new DayOfMonth();

                dayOfMonth.TbDayNumber.Text = (curDate.Day).ToString();
                _listDays.Add(dayOfMonth);

                if ((int)curDate.DayOfWeek == 0)
                {
                    dayOfMonth.HighlightRed();
                }
                if ((int)curDate.DayOfWeek == 6)
                {
                    dayOfMonth.HighlightRed();
                }

                if ((int)curDate.DayOfWeek == 0)    //это необходимо по той непонятной
                    Grid.SetColumn(dayOfMonth, 6);  //причине что вокресенье здесь 
                else                                //является нулевым днем
                    Grid.SetColumn(dayOfMonth, (int)curDate.DayOfWeek - 1);
                Grid.SetRow(dayOfMonth, curRow);                
                GridMonth.Children.Add(dayOfMonth);

                if ((int)curDate.DayOfWeek == 0)
                    curRow++;

                curDate = curDate.AddDays(1);
            }
        }
        
        //событие отображения мсяца по клику
        public event EventHandler ButtonClick;
        public void ButMonth_Click(object sender, EventArgs e)
        {
            //bubble the event up to the parent
            if (this.ButtonClick != null)
                this.ButtonClick(this, e);
        }

        //добавить отпуск
        public void AddVacation(VacationPair vac, Brush color, Employee emp)
        {
            List<List<int>> rowsInDays = new List<List<int>>();
            for (int i = vac.Begin.Day; i <= vac.End.Day; i++)
            {
                rowsInDays.Add(_listDays[i - 1].FreeRows());
                 //listDays[i-1].FreeRows().Find(x=>x.)             
            }
            int nRow = rowsInDays[0].FindIndex(x => rowsInDays.TrueForAll(y => y.Contains(x)));
            if (nRow == -1)
            {
                if (_listDays[0].Rows == 6)
                {
                    if (MessageBox.Show("Добавление более чем шести отпусков совпадающих по дате может отрицательно сказаться на удобстве представления информации\nВы уверены, что хотите продолжить?", "Внимание!", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                }
                foreach (DayOfMonth day in _listDays)
                {
                    day.AddRow();                   
                }
                rowsInDays[0].Add(_listDays[0].Rows - 1);
                nRow = rowsInDays[0].Count - 1;

            }
            int row = rowsInDays[0][nRow];
            for (int i = vac.Begin.Day; i <= vac.End.Day; i++)
            {
                _listDays[i - 1].AddRect(row, color, emp);
            }
        }

        //удаление отпуска
        public void DeleteVacation(VacationPair vac, Employee emp)
        {
            for (int i = vac.Begin.Day; i <= vac.End.Day; i++)
            {
                _listDays[i - 1].DeleteRect(emp);
            }
        }

        //отрисовать заново
        public void Rebuild()
        {

        }

        public void RemoveEmptyLine()
        {
            List<List<int>> rowsInDays = new List<List<int>>();
            for (int i = 0; i <= _listDays.Count; i++)
            {
                rowsInDays.Add(_listDays[i].FreeRows());
            }
            int foundIndex = 0;
            while (foundIndex != -1)
            {
                foundIndex = rowsInDays[0].FindIndex(x => rowsInDays.TrueForAll(y => y.Contains(x)));
                if (foundIndex != -1)
                {
                    for (int i = 0; i <= _listDays.Count; i++)
                    {
                        _listDays[i].DeleteRect(foundIndex);
                    }
                }
            }
        }

        //подсветить как короткий день
        public void HighlightGreen(List<DateTime> hDays)
        {
            for (int i = 0; i < hDays.Count; i++)
            {
                _listDays[hDays[i].Day - 1].HighlightGreen();
            }
        }
        //подсветить как праздник
        public void HighlightRed(List<DateTime> hDays)
        {
            for (int i = 0; i < hDays.Count; i++)
            {
                _listDays[hDays[i].Day - 1].HighlightRed();
            }
        }

    }
}
