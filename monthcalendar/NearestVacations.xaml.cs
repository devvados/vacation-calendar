using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Serialization;
using MahApps.Metro.Controls;

namespace MonthCalendar
{
    /// <summary>
    /// Interaction logic for NearestVacations.xaml
    /// </summary>
    public partial class NearestVacations : MetroWindow
    {
        private List<Employee> _emps = new List<Employee>();
        //XmlSerializer serializerEmp;

        public NearestVacations(List<string> soon, List<string> now)
        {
            List<string> vacationSoon = soon;
            List<string> atVacation = now;


            InitializeComponent();

            if (vacationSoon.Count > 0)
            {
                for (int i = 0; i < vacationSoon.Count; i++)
                {
                    LBSoon.Items.Add(vacationSoon[i].ToString());
                }
            }

            if (atVacation.Count > 0)
            {
                for (int i = 0; i < atVacation.Count; i++)
                {
                    LBNow.Items.Add(atVacation[i].ToString());

                    ExpEmployeesAtVacation.IsExpanded = true;
                }
            }
        }
    }
}
