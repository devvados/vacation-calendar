using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonthCalendar
{
    public partial class DayOfMonth : UserControl
    {
        List<Rectangle> _rects = new List<Rectangle>();
        public DayOfMonth()
        {
            InitializeComponent();
        }

        public int Rows
        {
            get { return GridVacations.Children.Count; }
        }

        //добавить строку (отпуск)
        public void AddRow()
        {
            GridVacations.RowDefinitions.Add(new RowDefinition());
            Rectangle tRect = new Rectangle();
            Grid.SetRow(tRect, GridVacations.RowDefinitions.Count-1);
            _rects.Add(tRect);
            GridVacations.Children.Add(tRect);
        }

        //поиск пустых строк
        public List<int> FreeRows()
        {
            List<int> tRet = new List<int>();
            for (int i = 0; i < _rects.Count; i++)
            {
                if (_rects[i].Tag == null)
                    tRet.Add(i);
            }
            return tRet;
        }

        //добавить (графически)
        public void AddRect(int row, Brush color, Employee emp)
        {
            _rects[row].Fill = color;
            _rects[row].Tag = emp;
            _rects[row].ToolTip = emp.Surname + " " + emp.Name;
        }

        //удаление (графически)
        public void DeleteRect(Employee emp)
        {
            int row = _rects.FindIndex(x => ((x.Tag as Employee) == emp));
            if (row != -1)
            {

                _rects[row].Fill = Brushes.White;
                _rects[row].Tag = null;
                _rects[row].ToolTip = null;
            }
        }
        public void DeleteRect(int row)
        {
            GridVacations.Children.Remove(_rects[row]);
            GridVacations.RowDefinitions.RemoveAt(Grid.GetRow(_rects[row]));
            _rects.RemoveAt(row);
        }

        //подсветить как короткий день
        public void HighlightGreen()
        {
            TbDayNumber.Foreground = Brushes.Orange;
        }
        //подсветить как праздник
        public void HighlightRed()
        {
            TbDayNumber.Foreground = Brushes.Red;
        }
    }
}
