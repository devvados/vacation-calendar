using System;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace MonthCalendar
{
    public partial class AddVacation : MetroWindow
    {

        public DateTime DateBegin, DateEnd;
        public AddVacation()
        {
            InitializeComponent();
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

        private void bOK_Click(object sender, RoutedEventArgs e)
        {
            if (DpBegin.SelectedDate != null && DpEnd.SelectedDate != null)
            {
                DateBegin = DpBegin.SelectedDate ?? DateTime.Now.Date;
                DateEnd = DpEnd.SelectedDate ?? DateTime.Now.Date;
                if (DateTime.Compare(DateBegin, DateEnd) < 0)
                {
                    DialogResult = true;
                }
                else
                {
                    ShowMessageBox("Выберите верные даты!", "Ошибка");
                }
            }
            else
            {
                ShowMessageBox("Введите дату", "Ошибка");
            }
        }
    }
}
