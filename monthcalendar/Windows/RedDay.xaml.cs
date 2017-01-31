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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.IO;
using System.Xml.Serialization;
using MahApps.Metro.Controls.Dialogs;
using System.ComponentModel;

namespace MonthCalendar
{
    public partial class RedDay : MetroWindow
    {
        public List<DateTime> redDates = new List<DateTime>(); //праздничные дни
        public List<DateTime> shortDates = new List<DateTime>(); //короткие дни
        public string typeOfDay;

        XmlSerializer serializerRD, serializerSD; 

        public RedDay()
        {
            InitializeComponent();

            DownloadHolidays();
        }

        public async void ShowMessageBox(string text, string title)
        {
            MetroDialogSettings ms = new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Theme,
                AnimateShow = true,
                AnimateHide = true
            };
            await this.ShowMessageAsync(title, text, MessageDialogStyle.Affirmative, ms);
        }

        private ContextMenu ListBoxCM()
        {
            ContextMenu m = new ContextMenu();

            MenuItem mi1 = new MenuItem { Header = "Удалить" };
            mi1.Click += DelItemClick;
            m.Items.Add(mi1);

            return m;
        }

        private void DelItemClick(object sender, RoutedEventArgs e)
        {
            var lbItem = ListBoxOfDates.ItemContainerGenerator.ContainerFromItem(ListBoxOfDates.SelectedItem) as ListBoxItem;
            if (lbItem != null)
            {
                string selectedRangeOfDates = lbItem.Content.ToString();
                selectedRangeOfDates = selectedRangeOfDates.Remove(0, selectedRangeOfDates.IndexOf(" ") + 1);
                if (selectedRangeOfDates.Contains("-"))
                {
                    string[] splitDates = selectedRangeOfDates.Split(' ');
                    DateTime[] dtArrayToDelete = GetDatesBetween(Convert.ToDateTime(splitDates[0]), Convert.ToDateTime(splitDates[2]));
                    foreach (DateTime dt in dtArrayToDelete)
                    {
                        if (redDates.Contains(dt))
                        {
                            redDates.Remove(dt);
                        }
                    }
                }
                else
                {
                    redDates.Remove(Convert.ToDateTime(selectedRangeOfDates));
                }

                ListBoxOfDates.Items.Clear();
                DrawLoadedHolidays();
            }
        }

        #region Работа с датами
        //получение дат в промежутке
        public static DateTime[] GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                allDates.Add(date);
            return allDates.ToArray();
        }

        //добавление промежутка/дня
        private void AddSelectedDay(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Добавили!");
            try
            {
                var tempDates = CalendarControl.SelectedDates.ToList();

                List<DateTime> dates = null;
                if (tempDates.Count > 1)
                    dates = CheckDatesOrder(tempDates);
                else
                    dates = new List<DateTime>(tempDates);

                if (CheckContainedDates(dates) != true)
                {
                    if (dates == null)
                        throw new Exception("Выберите дату!");
                    else
                    {
                        if (RbRedDay.IsChecked == true)
                        {
                            typeOfDay = RbRedDay.Content.ToString();
                            foreach (var day in dates)
                            {
                                redDates.Add(day);
                            }
                        }
                        if (RbShortDay.IsChecked == true)
                        {
                            typeOfDay = RbShortDay.Content.ToString();
                            foreach (var day in dates)
                            {
                                shortDates.Add(day);
                            }
                        }
                        redDates = SortAscending(redDates);
                        shortDates = SortAscending(shortDates);

                        AddLabel(dates, typeOfDay);
                    }
                }
            }
            catch(Exception ex)
            {
                ShowMessageBox(ex.Message, "Предупреждение");
            }
        }

        static List<DateTime> SortAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        //проверка порядка дат в выделенном промежутке
        private List<DateTime> CheckDatesOrder(List<DateTime> dates)
        {
            var tDates = new List<DateTime>(dates);
            if (DateTime.Compare(tDates.First(), tDates.Last()) > 0)
            {
                tDates.Reverse();
            }
            return tDates;
        }

        //проверка выделенного промежутка/даты на наличие в списке
        private bool CheckContainedDates(List<DateTime> dates)
        {
            bool contains = false;
            var tDates = new List<DateTime>(dates);
            string errorMessage = "Этот день (или день из выбранного промежутка) уже существует в списке праздничных/сокращенных дней!";
            try
            {
                foreach (var tday in tDates)
                {
                    if (redDates.Contains(tday))
                    {
                        contains = true;
                        throw new Exception(errorMessage);
                    }
                    if (shortDates.Contains(tday))
                    {
                        contains = true;
                        throw new Exception(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, "Предупреждение");
            }
            return contains;
        }

        //Разбиваем массив с датами на интервалы
        private List<List<DateTime>> CutArray(List<DateTime> dates)
        {
            List<List<DateTime>> ranges = new List<List<DateTime>>();
            List<DateTime> inaRow = new List<DateTime>();
            if (dates.Count > 0)
            {
                for (int i = 0; i < dates.Count; i++)
                {
                    if (i + 1 < dates.Count)
                    {
                        if (dates[i].DayOfYear - dates[i + 1].DayOfYear == -1)
                        {
                            if (!inaRow.Contains(dates[i]))
                            {
                                inaRow.Add(dates[i]);
                                if (i + 1 < dates.Count)
                                    inaRow.Add(dates[i + 1]);
                            }
                            if (inaRow.Contains(dates[i]) && !inaRow.Contains(dates[i + 1]))
                            {
                                inaRow.Add(dates[i + 1]);
                            }
                        }
                        else
                        {
                            inaRow.Add(dates[i]);
                            ranges.Add(new List<DateTime>(inaRow));
                            inaRow.Clear();
                        }
                    }
                    else if (i + 1 == dates.Count)
                    {
                        if (dates.Count > 1)
                        {
                            if (dates[i - 1].DayOfYear - dates[i].DayOfYear == -1)
                            {
                                if (!inaRow.Contains(dates[i]))
                                {
                                    inaRow.Add(dates[i]);
                                    ranges.Add(inaRow);
                                }
                                else ranges.Add(inaRow);
                            }
                            else
                            {
                                var temp = new List<DateTime> { dates[i] };
                                ranges.Add(temp);
                                inaRow.Clear();
                            }
                        }
                        else if(dates.Count == 1)
                        {
                            inaRow.Add(dates[i]);
                            ranges.Add(inaRow);
                        }
                        break;
                    }
                }
            }
            return ranges;
        }
        #endregion

        #region Отрисовка в ListBox
        //добавление элементов в ListBox
        private void AddLabel(List<DateTime> d, string day)
        {
            if(d.Count == 1)
            {
                ListBoxOfDates.Items.Add(new Label
                {
                    Content = d.First().ToShortDateString(),
                    Foreground = (typeOfDay == "Праздничный") ? Brushes.Red : Brushes.Green,
                    FontWeight = FontWeights.Bold,
                    FontSize = 15
                });
            }
            else if(d.Count > 1)
            {
                ListBoxOfDates.Items.Add(new Label
                {
                    Content = d.First().ToShortDateString() + " - " + d.Last().ToShortDateString(),
                    Foreground = (typeOfDay == "Праздничный") ? Brushes.Red : Brushes.Green,
                    FontWeight = FontWeights.Bold,
                    FontSize = 15
                });
            }
        }

        //Отрисовка загруженного списка
        private void DrawLoadedHolidays()
        {
            var redDatesRanges = CutArray(redDates);
            var shortDatesRanges = CutArray(shortDates);

            if (redDatesRanges.Count > 0 || shortDatesRanges.Count > 0)
            {
                for (int i = 0; i < redDatesRanges.Count; i++)
                {
                    ListBoxOfDates.Items.Add(new Label
                    {
                        Content = redDatesRanges[i].Count > 1 
                                        ? redDatesRanges[i].First().ToShortDateString() + " - " + redDatesRanges[i].Last().ToShortDateString() 
                                        : redDatesRanges[i].First().ToShortDateString(),
                        Foreground = Brushes.Red,
                        FontWeight = FontWeights.Bold,
                        FontSize = 15
                    });
                }
                for (int i = 0; i < shortDatesRanges.Count; i++)
                {
                    ListBoxOfDates.Items.Add(new Label
                    {
                        Content = shortDatesRanges[i].Count > 1 
                                        ? shortDatesRanges[i].First().ToShortDateString() + " - " + shortDatesRanges[i].Last().ToShortDateString() 
                                        : shortDatesRanges[i].First().ToShortDateString(),
                        Foreground = Brushes.Green,
                        FontWeight = FontWeights.Bold,
                        FontSize = 15
                    });
                }
            }
        }
        #endregion

        #region Сериализация/десериализация
        //Десериализация списка сотрудников
        private void DownloadHolidays()
        {
            //MessageBox.Show("Загружаем...");

            serializerRD = new XmlSerializer(redDates.ToArray().GetType());
            serializerSD = new XmlSerializer(shortDates.ToArray().GetType());

            DateTime[] deserRD = new DateTime[] { }, deserSD = new DateTime[] { };
            using (FileStream fs = new FileStream("redDays.xml", FileMode.Open))
            {
                if (fs.Length > 0)
                    deserRD = (DateTime[])serializerRD.Deserialize(fs);
            }
            using (FileStream fs = new FileStream("shortDays.xml", FileMode.Open))
            {
                if (fs.Length > 0)
                    deserSD = (DateTime[])serializerSD.Deserialize(fs);
            }

            redDates = new List<DateTime>(deserRD);
            shortDates = new List<DateTime>(deserSD);

            DrawLoadedHolidays();
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                if (redDates.Count < 1 && shortDates.Count < 1)
                    throw new Exception(@"Праздничных\коротких дней не назначено.");
            }
            catch(Exception ex)
            {
                ShowMessageBox(ex.Message, "Информация");
            }
            finally
            {
                SaveHolidays();
            }
        }

        //Сериализация списка
        private void SaveHolidays()
        {
            //MessageBox.Show("Сохраняем...");
        
            XmlSerializer
                serializerRD = new XmlSerializer(redDates.GetType()),
                serializerSD = new XmlSerializer(shortDates.GetType());

            TextWriter swRD = new StreamWriter("redDays.xml");
            TextWriter swSD = new StreamWriter("shortDays.xml");
            try
            {
                serializerRD.Serialize(swRD, redDates);
                serializerSD.Serialize(swSD, shortDates);
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.InnerException.Message);
            }
            finally
            {
                if (swRD != null) swRD.Close();
                if (swSD != null) swSD.Close();
            }
        }
        #endregion
    }
}