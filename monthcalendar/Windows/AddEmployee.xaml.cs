using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MonthCalendar
{
    public partial class AddEmployee : MetroWindow
    {
        public static List<Brush> avColors, exAvColors;
        public static string[] colorCodes, extendedColorCodes;
        public string name = null, Surname = null;
        
        public AddEmployee()
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
            if (TbName.Text != "" && TbSurname.Text != "")
            {
                name = TbName.Text;
                Surname = TbSurname.Text;

                DialogResult = true;
            }
            else
            {
                ShowMessageBox("Заполните поля!", "Ошибка");
            }
        }
    }

}
