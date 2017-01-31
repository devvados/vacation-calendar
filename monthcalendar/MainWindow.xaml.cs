using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MahApps.Metro.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.IO;
using System.Linq;
using System.Windows.Threading;
using System.Xml.Serialization;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace MonthCalendar
{
    public partial class MainWindow : MetroWindow
    {
        //Список оставшихся цветов после загрузки сотрудников
        public static Stack<Brush> avColors;
        public static List<Brush> usedColors = new List<Brush>();
        List<string> usedColorCodes = new List<string>();
        static string[] colorCodes;

        bool confirmed = false;

        static MainWindow()
        {
            usedColors = new List<Brush>();
            avColors = new Stack<Brush>();
            
            colorCodes = new string[]
            {
                #region Цвета
                "#FF0000", "#0000FF", "#00FF00", "#FFFF00", "#483D8B",
                "#5E2129", "#00FFFF", "#6600FF", "#006400", "#FA8072",
                "#008080", "#00BFFF", "#696969", "#800000", "#9370D8",
                "#ADD8E6", "#C71585", "#B2EC5D", "#D2691E", "#FF00FF",
                "#DC143C", "#E0B0FF", "#FC8080", "#FFD700", "#140300",
                "#47402E", "#158078", "#FF4F00", "#009900", "#72525C",
                "#FBCEB1", "#FDD9B5", "#B5B8B1", "#7FFFD4", "#78DBE2", "#E32636", "#FF2400", "#9F2B68", "#AB274F", "#F19CBB", "#E52B50", "#ED3CCA", "#CD2682", "#FF033E", "#9966CC", "#CD9575", "#293133", "#464451", "#44944A", "#2F4F4F",
                "#6A5ACD", "#A8E4A0", "#4E5754", "#614051", "#990066", "#6E5160", "#FAE7B5", "#CCCCFF", "#C5D0E6", "#FAEEDD", "#79553D", "#C1876B", "#6D6552", "#F5F5DC", "#A5A5A5", "#BDECB6", "#FFFAFA", "#FFFFFF", "#FAEBD7", "#FFDEAD",
                "#003153", "#77DDE7", "#1E5945", "#3F888F", "#30D5C8", "#FFE4C4", "#A5260A", "#3D2B1F", "#ABCDEF", "#FFDB8B", "#8D917A", "#89AC76", "#EEE8AA", "#B03F35", "#DDADAF", "#755C48", "#DABDAB", "#AE848B", "#FADADD", "#AFEEEE",
                "#957B8D", "#FFCBDB", "#ECEBBD", "#F0D698", "#FFC8A8", "#98FB98", "#FFDF84", "#AC7580", "#FFCA86", "#FDBDBA", "#8A7F8E", "#BC987E", "#919192", "#D87093", "#CED23A", "#FFCF40", "#8CCB5E", "#47A76A", "#FFDC33", "#2A8D9C",
                "#FFB841", "#FF97BB", "#62639B", "#DD80CC", "#009B76", "#4285B4", "#755D9A", "#9F8170", "#480607", "#ACB78E", "#641C34", "#9B2D30", "#D5D5D5", "#4C514A", "#3E5F8A", "#FFB02E", "#CD7F32", "#900020", "#45161C", "#343B29",
                "#D5713F", "#6495ED", "#9ACEEB", "#DAD871", "#34C924", "#DE4C8A", "#00FF7F", "#ECEABE", "#A7FC00", "#BD33A4", "#702963", "#414833", "#911E42", "#256D7B", "#0095B6", "#FFCF48", "#CC5500", "#B8B799", "#DCDCDC",
                "#DF73FF", "#F3A505", "#734222", "#C9A0DC", "#CDA4DE", "#C154C1", "#425E17", "#B57900", "#00541F", "#593315", "#F64A46", "#004524", "#9F8200", "#EF3038", "#A9203E", "#B94E48", "#FF4040", "#4D220E", "#490005", "#A91D11",
                "#641349", "#7B001C", "#142300", "#D76E00", "#C34D0A", "#6F0035", "#EB5284", "#1A153F", "#531A50", "#FF1493", "#00382B", "#002F55", "#240935", "#423189", "#606E8C", "#A2A2D0", "#80DAEB", "#0E294B", "#30BA8F",
                "#FFDB58", "#FD7C6E", "#F34723", "#2F353B", "#1C1C1C", "#474A51", "#C7D0CC", "#D71868", "#D1E231", "#EFD334", "#E49B0F", "#00693E", "#CA3767", "#1560BD", "#FF43A4", "#FC6C85", "#A2ADD0", "#F5F5F5", "#F4A900",
                "#FDBCB4", "#EDFF21", "#E1CC4F", "#9ACD32", "#C5E384", "#CDA434", "#ED760E", "#FFAE42", "#FADFAD", "#FFE4B2", "#8F8B66", "#FCE883", "#D6AE01", "#BDB76B", "#434B4D", "#FFE2B7", "#CAA885", "#EAE6CA",
                "#CB6586", "#1E90FF", "#FF47CA", "#7CFC00", "#01796F", "#BEBD7F", "#ADFF2F", "#F0E891", "#826C34", "#BFFF00", "#2E8B57", "#4D5645", "#1F3438", "#1164B4", "#3BB08F", "#29AB87", "#008000", "#1CAC78", "#ADDFAD",
                "#006633", "#2F4538", "#004953", "#4F7942", "#203A27", "#009A63", "#D0F0C0", "#F5E6CB", "#7A7666", "#181513", "#FCD975", "#DAA520", "#712F26", "#E7C697", "#321414", "#79443B", "#009B77", "#287233", "#50C878",
                "#4B0082", "#5D76CB", "#138808", "#CD5C5C", "#4CBB17", "#BDDA57", "#A25F2A", "#8B8C7A", "#FFFF99", "#1B5583", "#C41E3A", "#1CD3A2", "#960018", "#A2231D", "#EB4C42", "#FF0033", "#9D9101", "#633A34", "#BC5D58", "#99958C",
                "#6C6960", "#FF4D00", "#E34234", "#CB4154", "#884535", "#507D2A", "#D53032", "#1E213D", "#0047AB", "#F0DC82", "#FFA000", "#B32821", "#FF7F50", "#893F45", "#B15124", "#CD9A7B", "#503D33", "#140F0B", "#8A6642", "#A52A2A",
                "#C19A6B", "#39352A", "#781F19", "#C8385A", "#25221B", "#464531", "#964B00", "#B4674D", "#35170C", "#806B2A", "#8B4513", "#7B3F00", "#CA2C92", "#7851A9", "#4169E1", "#FFF8E7", "#414A4C", "#442D25", "#FDD5B1",
                "#B00000", "#80461B", "#592321", "#C93C20", "#FF5349", "#6D3F5B", "#922B3E", "#C0448F", "#CD5700", "#755A57", "#8B6C62", "#1E1112", "#C04000", "#CD4A4C", "#EE204D", "#F4A460", "#FFFDD0", "#FDF4E3", "#76FF7A",
                "#FB607F", "#E4A010", "#FBEC5D", "#E6E6FA", "#FCB4D5", "#FBA0E3", "#FEFE22", "#2A52BE", "#007BA7", "#025669", "#007FFF", "#1DACD6", "#32CD32", "#979AAA", "#B5A642", "#DBD7D2", "#228B22", "#534B4F", "#DB7093",
                "#C7B446", "#FFF44F", "#FFFACD", "#FDE910", "#2D572C", "#6DAE81", "#D95030", "#E55137", "#FF8C69", "#FF9BAA", "#FAF0E6", "#F80000", "#FFA420", "#7B917B", "#AAF0D1", "#F8F4FF", "#F664AF", "#EDD19C", "#4C9141",
                "#FFBD88", "#0BDA51", "#C51D34", "#B3446C", "#FF8243", "#E1523D", "#F28500", "#4C5866", "#4C2F27", "#8E402A", "#996666", "#904D30", "#B87333", "#DD9475", "#A98307", "#F0FFF0", "#00416A", "#EFDECD",
                "#21421E", "#EF0097", "#F400A1", "#FFE4B5", "#17806D", "#3CB371", "#F36223", "#54FF9F", "#9FE2BF", "#1C6B72", "#646B63", "#98FF98", "#497E76", "#20603D", "#F5FFFA", "#DC9D00", "#803E75", "#7F8F18", "#478430",
                "#E59E1F", "#95500C", "#FF7A5C", "#006B3C", "#CCA817", "#00677E", "#753313", "#7F180D", "#FFB961", "#9A366B", "#BF2233", "#0A4500", "#FF8E0D", "#EC7C26", "#B32851", "#F6768E", "#474389", "#FD7B7C", "#006D5B", "#00538A",
                "#53377A", "#734A12", "#F0FFFF", "#2271B3", "#7FC7FF", "#6B8E23", "#FFFF66", "#FFA343", "#00A86B", "#9DB1CC", "#252850", "#C9DC87", "#EA7E5D", "#AF2B1E", "#FF7F49", "#FDDB6D", "#1D334A", "#642424", "#59351F", "#999950",
                "#424632", "#BAB86C", "#6F4F28", "#4D4234", "#121910", "#808000", "#015D52", "#B784A7", "#FD5E53", "#F8D568", "#A65E2E", "#FF2B2B", "#FFCC99", "#FF9966", "#FFA500", "#FF7538", "#5B3A29", "#B32428", "#DA70D6", "#E6A8D7",
                "#FFBA00", "#49678D", "#355E3B", "#CC7722", "#AEA04B", "#955F20", "#E6BBC1", "#D8DEBA", "#CBBAC5", "#C1CACA", "#D8B1BF", "#002800", "#470736", "#4F0014", "#470027", "#320B35", "#20155E", "#E3A9BE", "#C6DF90", "#98C793",
                "#A3C6C0", "#BAACC7", "#A0D6B4", "#A6BDD7", "#EEBEF1", "#230D21", "#560319", "#132712", "#16251C", "#022027", "#270A1F", "#320A18", "#362C12", "#28071A", "#001D18", "#FFEBCD", "#C7FCEC", "#71BC78", "#3D642D", "#87CEEB",
                "#7FB5B5", "#EFA94A", "#77DD77", "#FF7514", "#FFD1DC", "#5D9B9B", "#A18594", "#316650", "#DEAA88", "#6A5D4D", "#6C6874", "#1C542D", "#705335", "#C35831", "#B44C43", "#721422", "#8673A1", "#2A6478", "#763C28", "#898176",
                "#102C54", "#193737", "#9C9C9C", "#828282", "#00A693", "#32127A", "#CC3333", "#FE28A2", "#FFE5B4", "#FFCFAB", "#CD853F", "#EFCDB8", "#C6A664", "#FCDD76", "#967117", "#00A550", "#31372B", "#F8173E", "#7F7679",
                "#FFEFD5", "#8A795D", "#003366", "#1A4876", "#191970", "#FF9218", "#131313", "#F8F8FF", "#FF00CC", "#FADBC8", "#75151E", "#88706B", "#4A192C", "#1B1116", "#9D81BA", "#7442C8", "#800080", "#F5DEB3", "#FF7E93", "#7D7F7D",
                "#B0E0E6", "#CC8899", "#FF496C", "#F3DA0B", "#587246", "#B7410E", "#FFF0F5", "#915F6D", "#993366", "#EF98AA", "#C8A696", "#FFAACC", "#AB4E52", "#FF77FF", "#B76E79", "#BC8F8F", "#905D5D", "#EE82EE", "#674846", "#FFC0CB",
                "#FC89AC", "#D36E70", "#AA98A9", "#65000B", "#997A8D", "#FDDDE6", "#FC74FD", "#F78FA7", "#9B111E", "#DE5D83", "#D77D31", "#99FF99", "#92000A", "#1D1E33", "#082567", "#E28B00", "#DDA0DD", "#E6D690", "#40E0D0", "#DE3163",
                "#DD4492", "#87CEFA", "#FFFFE0", "#FAFAD2", "#90EE90", "#FFEC8B", "#E66761", "#FFBCAD", "#987654", "#FFA812", "#ED9121", "#846A20", "#FDEAA8", "#BA7FA2", "#DCD0FF", "#F984EF", "#EA899A", "#FFB6C1", "#C9C0BB", "#84C3BE",
                "#B17267", "#D7D7D7", "#6C92AF", "#A6CAF0", "#D84B20", "#876C99", "#20B2AA", "#778899", "#FFD35F", "#2B6CC4", "#DCD36A", "#BB8B54", "#FFB28B", "#BAAF96", "#FFDE5A", "#649A9E", "#8B6D5C", "#A86540", "#AA6651",
                "#BB6C8A", "#E63244", "#945D0B", "#887359", "#FFA161", "#FFA8AF", "#C8A99E", "#837DA2", "#B48764", "#946B54", "#966A57", "#8B734B", "#B27070", "#C2A894", "#669E85", "#BEADA1", "#B0C4DE", "#D0D0D0", "#F0E68C", "#E0FFFF",
                "#704214", "#382C1E", "#A5694F", "#78858B", "#465945", "#332F2C", "#8A9597", "#C0C0C0", "#CDC5C2", "#9E9764", "#ACE1AF", "#403A3A", "#3E3B32", "#5F9EA0", "#B0B7C6", "#735184", "#CEA262", "#90845B", "#785840", "#D39B85",
                "#575E4E", "#C4A55F", "#5A3D30", "#5E3830", "#B85D43", "#7D4D5D", "#8C4743", "#52442C", "#8C4852", "#CC9293", "#413D51", "#CF9B8F", "#4A545C", "#46394B", "#48442D", "#9DA1AA", "#808080", "#686C5E", "#CADABA",
                "#95918C", "#6C7059", "#A0A0A4", "#6A5F31", "#CAC4B0", "#708090", "#E5BE01", "#317F43", "#6C3B2A", "#A52019", "#FF9900", "#969992", "#1E2460", "#924E7D", "#282828", "#A0522D", "#E97451", "#79A0C1", "#1F3A3D", "#0D98BA",
                "#8A2BE2", "#474B4E", "#6699CC", "#6C4675", "#7366BD", "#F9DFCF", "#7D746D", "#151719", "#007DFF", "#3A75C4", "#1F75FE", "#1FCECB", "#18A7B5", "#003399", "#4682B4", "#F0F8FF", "#C8A2C8", "#B565A7",
                "#FF6E4A", "#FC2847", "#FFBCD9", "#434750", "#660066", "#8E4585", "#F2DDC6", "#F2E8C9", "#FFFFF0", "#ACE5EE", "#F39F18", "#EFAF8C", "#2C5545", "#7BA05B", "#87A96B", "#AF4035", "#0067A5", "#817066", "#231A24",
                "#C08081", "#CFB53B", "#FDF5E6", "#EEDC82", "#D68A59", "#714B23", "#5D3954", "#9932CC", "#CB2821", "#116062", "#3B83BD", "#D8A903", "#B07D2B", "#013220", "#986960", "#CD5B45", "#654321", "#08457E", "#556832",
                "#FF8C00", "#FFDAB9", "#472A3F", "#E75480", "#483C32", "#27261A", "#49423D", "#002137", "#000080", "#00008B", "#1974D2", "#9400D3", "#8FBC8F", "#8B0000", "#177245", "#57A639", "#918151", "#304B26", "#3F2512",
                "#CC6C5C", "#BADBAD", "#9B8127", "#313830", "#45433B", "#003841", "#B8860B", "#310062", "#321011", "#9B2F1F", "#4F273A", "#523C36", "#681C23", "#8B008B", "#232C16", "#302112", "#C37629", "#03C03C", "#5B1E31", "#C76574",
                "#564042", "#1A162A", "#660099", "#C76864", "#26252D", "#A47C45", "#32221A", "#371F1C", "#482A2A", "#2B2517", "#464544", "#82898F", "#4C3C18", "#008B8B", "#452D35", "#FF7E00", "#909090", "#CF3476", "#E9967A", "#EA7500",
                "#2C3337", "#013A33", "#CC4E5C", "#4E3B31", "#1CA9C9", "#D53E07", "#A12312", "#FF6347", "#5DA130", "#35682D", "#FAD201", "#308446", "#CC0605", "#F54021", "#A03472", "#8D948D", "#063971", "#1E1E1E", "#45CEA2", "#6C7156",
                "#B57281", "#FFE4E1", "#DDBEC3", "#FF7518", "#20214F", "#120A8F", "#8A3324", "#48D1CC", "#5E490F", "#C0DCC0", "#66CDAA", "#7B68EE", "#00FA9A", "#8B8940", "#D79D41", "#657F4B", "#7D512D", "#EE9374", "#386646",
                "#C4A43D", "#30626B", "#673923", "#D35339", "#8C4566", "#AB343A", "#434B1B", "#64400F", "#F7943C", "#E8793E", "#A73853", "#E28090", "#423C63", "#7F4870", "#EE9086", "#674C47", "#2F6556", "#395778", "#543964",
                "#BA55D3", "#801818", "#B55489", "#4D5D53", "#EA8DF7", "#991199", "#F75394", "#8000FF", "#354D73", "#324AB2", "#8B00FF", "#926EAE", "#BEF574", "#F64A8A", "#123524", "#CC6666", "#F754E1", "#C364C5", "#C3B091", "#78866B",
                "#2E3A23", "#4E1609", "#45668E", "#3CAA3C", "#F7F21A", "#FFD800", "#D2B48C", "#FAA76C", "#1F4037", "#FFA474", "#834d18", "#505050", "#4E5452", "#008CF0", "#FFF5EE", "#FFFAF0", "#EEE6A3", "#CE2029", "#FFFDDF", "#E3256B",
                "#00ACED", "#3B5998", "#1A4780", "#FFF8DC", "#FFCC00", "#7D8471", "#F8F32B", "#EBC2AF", "#FFCBBB", "#343E40", "#212121", "#412227", "#3B3C36", "#23282B", "#18171C", "#000000", "#0A0A0A", "#141613", "#1F0E11",
                "#1D1018", "#161A1E", "#EBC7DF", "#D8BFD8", "#A08040", "#7FFF00", "#F5D033", "#F4C430", "#C54B8C", "#FB7EFD", "#45322E", "#CDB891", "#FF33CC", "#7DF9FF", "#CCFF00", "#CEFF1D", "#40826D", "#00CCCC", "#FFBF00",
                "#93AA00", "#FF845C", "#007D34", "#F4C800", "#F13A13", "#7E0059", "#C10020", "#FF8E00", "#FF6800", "#D5265B", "#943391", "#00836E", "#8F509D", "#08E8DE", "#FFB300", "#66FF00", "#F75E25", "#FFA089", "#FC0FC0", "#007CAD",
                #endregion
            };
        }

        //Таймер для отсылки уведомлений об отпусках
        DispatcherTimer dtNotify = new DispatcherTimer();

        private List<Employee> _emps = new List<Employee>();

        //Элементы управления (месяц, день месяца)
        private List<MonthOfYear> _months = new List<MonthOfYear>();
        private Dictionary<int, List<MonthOfYear>> _monthss = new Dictionary<int, List<MonthOfYear>>();
        private List<List<List<DateTime>>> yearsList = new List<List<List<DateTime>>>();
       
        //Загрузка праздников при запуске для отрисовки
        XmlSerializer serializerRD, serializerSD, serializerEmp;

        //Праздничные и короткие дни
        List<DateTime> shortDays = new List<DateTime>(),
                       redDays = new List<DateTime>();

        //Для отрисовки месяца
        DateTime curMonth;

        //Иконка в системном трее
        private System.Windows.Forms.NotifyIcon MyNotifyIcon;

        /// <summary>
        /// Сообщение в стиле Metro
        /// </summary>
        public async void ShowMessageBox(string text, string title)
        {
            MetroDialogSettings ms = new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Theme,
                AnimateShow = true,
                AnimateHide = true,
                DialogMessageFontSize = 24,
                DialogTitleFontSize = 30,
            };
            await this.ShowMessageAsync(title, text, MessageDialogStyle.Affirmative, ms);
        }

        public async void ShowClosingDialogBox(string text, string title, MetroDialogSettings ms)
        {
            var res = await this.ShowMessageAsync(title, text, MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, ms);
            if(res == MessageDialogResult.Affirmative)
            {
                dtNotify.Stop();
                confirmed = true;
                this.Close();         
            }
            else if(res == MessageDialogResult.Negative)
            {

            }
            else if(res == MessageDialogResult.FirstAuxiliary)
            {
                dtNotify.Stop();
                confirmed = true;
                this.Close();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            //добавляем иконку в трей
            DrawIconInTray();
  
            dtNotify.Tick += Show_ToolTip;
            dtNotify.Interval = new TimeSpan(0, 0, 0, 0, 0);
            dtNotify.Start();
            dtNotify.Interval = new TimeSpan(1, 0, 0, 0, 0);
        }

        /// <summary>
        /// Подкачка данных после загрузки окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка праздников при запуске
            DownloadHolidays();
            //загрузка имеющихся сотрудников при запуске
            DownloadEmployees();

            //определение оставшихся цветов
            try
            {
                foreach (Employee employee in _emps)
                {
                    SolidColorBrush colorBr = (SolidColorBrush)(new BrushConverter().ConvertFrom(employee.Hex));
                    string color = colorBr.Color.ToString();
                    usedColorCodes.Add(color);
                }
                for (int i = colorCodes.Length - 1; i > -1; i--)
                {
                    SolidColorBrush br = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorCodes[i]));
                    string color = br.Color.ToString();
                    if (usedColorCodes.Contains(color) == false)
                        avColors.Push((SolidColorBrush)(new BrushConverter().ConvertFrom(color)));
                    else
                        usedColors.Add((SolidColorBrush)(new BrushConverter().ConvertFrom(color)));
                }
            }
            catch
            {
                //десериализация пустого потока
                ShowMessageBox("Список не содержит цветов", "Ошибка при загрузке");
            }

            DrawCurrentYear();
            //SelectAllHolidays();
        }

        #region Действия при загрузке

        /// <summary>
        /// рисуем текущий год
        /// </summary>
        private void DrawCurrentYear()
        {
            DateTime curDate = new DateTime(DateTime.Now.Year, 1, 1);
            YearNum.Content = curDate.Year.ToString();

            for (var row = 0; row < GridYear.RowDefinitions.Count; row++)
            {
                for (var col = 0; col < GridYear.ColumnDefinitions.Count; col++)
                {
                    MonthOfYear month = new MonthOfYear(curDate);
                    month.ButtonClick += new EventHandler(MonthOfYear_ButtonClick);

                    Grid.SetColumn(month, col);
                    Grid.SetRow(month, row);
                    GridYear.Children.Add(month);

                    _months.Add(month);
                    curDate = curDate.AddMonths(1);
                }
            }
            curMonth = DateTime.Now;
        }

        /// <summary>
        /// загружаем праздники
        /// </summary>
        private void DownloadHolidays()
        {
            serializerRD = new XmlSerializer(redDays.ToArray().GetType());
            serializerSD = new XmlSerializer(shortDays.ToArray().GetType());

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

            redDays = new List<DateTime>(deserRD);
            shortDays = new List<DateTime>(deserSD);
        }

        /// <summary>
        /// загружаем сотрудников
        /// </summary>
        private void DownloadEmployees()
        {
            try
            {
                Employee[] serEmp = _emps.ToArray();
                Employee[] deserEmp = new Employee[] { };
                using (FileStream fs = new FileStream("Employees.xml", FileMode.OpenOrCreate))
                {
                    if (fs.Length == 0)
                        throw new Exception();
                    else
                    {
                        serializerEmp = new XmlSerializer(typeof(Employee[]));

                        deserEmp = (Employee[])serializerEmp.Deserialize(fs);
                    }
                }
                _emps = new List<Employee>(deserEmp);

                if (_emps.Count > 0)
                    FillTreeByEmployees(_emps);
            }
            catch
            {
                ShowMessageBox("Список сотрудников пуст. Заполните и сохраните его!", "Информация");
            }
        }

        /// <summary>
        /// отрисовываем загруженных сотрудников в дереве
        /// </summary>
        /// <param name="tEmpList">Список сотрудников</param>
        private void FillTreeByEmployees(List<Employee> tEmpList)
        {
            TreeViewWorkers.Items.Clear();
            List<Employee> fillingList = new List<Employee>(tEmpList);
            foreach(Employee employee in fillingList)
            {
                var dp = new DockPanel();

                dp.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/MonthCalendar;component/Icons/personal.ico")),
                    Height = 18,
                    Width = 18,
                    Margin = new Thickness(5, 0, 8, 0),
                    VerticalAlignment = VerticalAlignment.Center
                });
                dp.Children.Add(new Label
                {
                    Content = employee.Surname + " " + employee.Name,
                    VerticalAlignment = VerticalAlignment.Center
                });
                dp.Children.Add(new Label
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Width = 40,
                    //Margin = new Thickness(3),
                    Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(employee.hexademical)),
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness(1)
                });

                TreeViewItem tviEmp = new TreeViewItem
                {
                    Tag = employee,
                    Header = dp
                };
                if(employee.VacList.Count > 0)
                {
                    foreach(VacationPair vacpair in employee.VacList)
                    {
                        DeHighLightVac(vacpair, employee);
                        VacationPair tVac = CorrectVacation(new VacationPair(vacpair.Begin, vacpair.OriginalEndDate)); //vacpair
                        string vacString = tVac.Begin.Date.ToShortDateString() + " - " + tVac.End.Date.ToShortDateString();
                        var dp1 = new DockPanel();

                        dp1.Children.Add(new Image
                        {
                            Source = new BitmapImage(new Uri(@"pack://application:,,,/MonthCalendar;component/Icons/bag.ico")),
                            Height = 18,
                            Width = 18,
                            Margin = new Thickness(5, 0, 8, 0),
                            VerticalAlignment = VerticalAlignment.Center
                        });
                        dp1.Children.Add(new Label
                        {
                            Content = tVac.OriginalEndDate == tVac.End ? vacString : vacString + " <" + tVac.OriginalEndDate.ToShortDateString() + ">",
                            VerticalAlignment = VerticalAlignment.Center
                        });
                        tviEmp.Items.Add(new TreeViewItem
                        {
                            Tag = tVac,
                            Header = dp1
                        });
                    }
                }
                TreeViewWorkers.Items.Add(tviEmp);
            }
        }

        #endregion

        #region Таймер (напоминания об отпусках)

        /// <summary>
        /// отрисовка уведомления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_ToolTip(object sender, EventArgs e)
        {
            if (TimeSpan.Compare(dtNotify.Interval, new TimeSpan(0, 0, 0, 0, 0)) == 0)
            {
                dtNotify.Stop();
                dtNotify.Interval = new TimeSpan(1, 0, 0, 0, 0);
                dtNotify.Start();
            }
            Dictionary<Employee, DateTime> vacationSoon = new Dictionary<Employee, DateTime>();
            Dictionary<Employee, DateTime> atVacation = new Dictionary<Employee, DateTime>();

            for (int i = 0; i < _emps.Count; i++)
            {
                for (int j = 0; j < _emps[i].VacList.Count; j++)
                {
                    if (DateTime.Compare(DateTime.Now, _emps[i].VacList[j].Begin) <= 0)
                    {
                        if (_emps[i].VacList[j].Begin.DayOfYear - DateTime.Now.DayOfYear <= 8)
                        {
                            vacationSoon.Add(_emps[i], _emps[i].VacList[j].Begin);
                        }
                    }
                    else if(DateTime.Compare(DateTime.Now, _emps[i].VacList[j].Begin) >= 0)
                    {
                        if (RedDay.GetDatesBetween(_emps[i].VacList[j].Begin, _emps[i].VacList[j].End).ToList().Contains(DateTime.Now.Date))
                        {
                            atVacation.Add(_emps[i], _emps[i].VacList[j].End);
                        }
                    }
                }
            }

            List<string> toShowVacationSoon = new List<string>();
            List<string> toShowAtVacation = new List<string>();

            foreach (var kvp in vacationSoon)
            {
                toShowVacationSoon.Add(kvp.Key.Name + " " + kvp.Key.Surname + " отпуск с " + kvp.Value.ToShortDateString());
            }
            foreach (var kvp in atVacation)
            {
                toShowAtVacation.Add(kvp.Key.Name + " " + kvp.Key.Surname + " отпуск по " + kvp.Value.ToShortDateString());
            }

            if (toShowVacationSoon.Count > 0 || toShowAtVacation.Count > 0)
            {
                int windowsCount = 0;
                foreach (var wnd in Application.Current.Windows)
                {
                    if (wnd is NearestVacations)
                    {
                        //нашли открытое окно
                        //throw new Exception("Окно с напоминанием уже открыто. Пожалуйста, закройте его!");
                    }
                    else
                    {
                        windowsCount++;
                    }
                }

                if (windowsCount == Application.Current.Windows.Count)
                {
                    //это окно не открыто
                    var wnd = new NearestVacations(toShowVacationSoon, toShowAtVacation);
                    wnd.ShowDialog();
                }
            }
        }
        #endregion

        #region Состояние окна свернуто/развернуто

        /// <summary>
        /// отрисовка иконки приложения в трее
        /// </summary>
        private void DrawIconInTray()
        {
            System.Drawing.Image im = System.Drawing.Image.FromFile("calendar.ico");

            MyNotifyIcon = new System.Windows.Forms.NotifyIcon();
            MyNotifyIcon.Icon = System.Drawing.Icon.FromHandle(((System.Drawing.Bitmap)im).GetHicon());
            MyNotifyIcon.MouseDoubleClick +=
                new System.Windows.Forms.MouseEventHandler
                    (MyNotifyIcon_MouseDoubleClick);
            MyNotifyIcon.Visible = true;
        }

        /// <summary>
        /// двойной клик по иконке свернутого окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MyNotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// изменение положения окна (свернуто/развернуто)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                MyNotifyIcon.BalloonTipTitle = "Вы свернули приложение...";
                MyNotifyIcon.BalloonTipText = "Для того, чтобы развернуть, дважды кликните по иконке";
                MyNotifyIcon.ShowBalloonTip(400);
                //MyNotifyIcon.Visible = true;
            }
            else if (this.WindowState == WindowState.Normal)
            {
                //MyNotifyIcon.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        #endregion

        #region Операции над работниками

        /// <summary>
        /// добавление нового работника (команда с комбинацией клавиш)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddEmployee(object sender, ExecutedRoutedEventArgs e)
        {
            //this.Hide();
            var wnd = new AddEmployee();
            wnd.ShowDialog();

            if (wnd.DialogResult == true)
            {
                string name = wnd.name, surname = wnd.Surname;
                Brush brush = avColors.Pop();
                usedColors.Add(brush);

                try
                {
                    if (_emps.Find(
                        worker => worker.Name.ToLower() == name.ToLower() &&
                                  worker.Surname.ToLower() == surname.ToLower()) != null)
                    {
                        throw new Exception("Работник с таким именем уже существует в списке!");
                    }
                    var emp = new Employee(name, surname, brush);
                    _emps.Add(emp);

                    var dp = new DockPanel();

                    dp.Children.Add(new Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/MonthCalendar;component/Icons/personal.ico")),
                        Height = 18,
                        Width = 18,
                        Margin = new Thickness(5, 0, 8, 0),
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    dp.Children.Add(new Label
                    {
                        Content = emp.Surname + " " + emp.Name,
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    dp.Children.Add(new Label
                    {
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Width = 40,
                        //Margin = new Thickness(3),
                        Background = brush,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1)
                    });
                    TreeViewWorkers.Items.Add(new TreeViewItem
                    {
                        Tag = emp,
                        Header = dp
                    });
                }
                catch (Exception ex)
                {
                    ShowMessageBox(ex.Message, "Предупреждение");
                }
            }
        }

        /// <summary>
        /// удаление работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelItem_Click(object sender, RoutedEventArgs e)
        {
            var tvi = TreeViewWorkers.SelectedItem as TreeViewItem;
            var emp = (TreeViewWorkers.SelectedItem as TreeViewItem).Tag as Employee;
            foreach (VacationPair vp in emp.VacList)
            {
                DeHighLightVac(vp, emp);
            }
            usedColors.Remove(emp.BrColor);
            avColors.Push(emp.BrColor);
            _emps.Remove(emp);
            var itemsControl = tvi.Parent as ItemsControl;
            itemsControl.Items.Remove(tvi);
        }

        #endregion

        #region Контекстные меню

        /// <summary>
        /// клик для открытия контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem tvi = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (tvi != null)
            {
                tvi.Focus();
                if (tvi.Tag is Employee)
                    TreeViewWorkers.ContextMenu = TrViewContextMenuEmployee();
                if (tvi.Tag is VacationPair)
                    TreeViewWorkers.ContextMenu = TrViewContextMenuVacation();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Поиск элемента управления по иерархии
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
            {
                source = System.Windows.Media.VisualTreeHelper.GetParent(source);
            }

            return source as TreeViewItem;
        }

        /// <summary>
        /// контекстное меню для работника
        /// </summary>
        /// <returns></returns>
        private ContextMenu TrViewContextMenuEmployee()
        {
            ContextMenu conMenu = new ContextMenu();
            MenuItem addVac = new MenuItem { Header = "Добавить отпуск" };
            addVac.Click += AddVac_Click;
            conMenu.Items.Add(addVac);
            MenuItem delVac = new MenuItem { Header = "Удалить работника" };
            delVac.Click += DelItem_Click;
            conMenu.Items.Add(delVac);
            return conMenu;
        }

        /// <summary>
        /// контекстное меню для отпуска
        /// </summary>
        /// <returns></returns>
        private ContextMenu TrViewContextMenuVacation()
        {
            var conMenu = new ContextMenu();
            var delVac = new MenuItem { Header = "Удалить отпуск" };
            var editVac = new MenuItem { Header = "Редактировать" };
            var selectVac = new MenuItem { Header = "Подсветить на календаре" };
            var unselectVac = new MenuItem { Header = "Убрать подсветку" };

            delVac.Click += DelVac_Click;
            editVac.Click += EditVac_Click;
            selectVac.Click += HighLightVacAs_Click;
            unselectVac.Click += DeHighLightVac_Click;

            conMenu.Items.Add(delVac);
            conMenu.Items.Add(editVac);
            conMenu.Items.Add(selectVac);
            conMenu.Items.Add(unselectVac);

            return conMenu;
        }
        #endregion

        #region Операции над отпусками

        /// <summary>
        /// Добавление отпуска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddVac_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddVacation();
            wnd.ShowDialog();

            if (wnd.DialogResult == true)
            {
                DateTime dateBegin = wnd.DateBegin, dateEnd = wnd.DateEnd;
                var emp = (TreeViewWorkers.SelectedItem as TreeViewItem).Tag as Employee;
                TreeViewItem tvi = TreeViewWorkers.SelectedItem as TreeViewItem;
                VacationPair tVac = CorrectVacation(new VacationPair(dateBegin, dateEnd)); //new Vacation
                emp.VacList.Add(tVac);
                string vacString = tVac.Begin.Date.ToShortDateString() + " - " + tVac.End.Date.ToShortDateString();

                var dp = new DockPanel();

                dp.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/MonthCalendar;component/Icons/bag.ico")),
                    Height = 18,
                    Width = 18,
                    Margin = new Thickness(5, 0, 8, 0),
                    VerticalAlignment = VerticalAlignment.Center
                });
                dp.Children.Add(new Label
                {
                    Content = tVac.OriginalEndDate == tVac.End ? vacString : vacString + " <" + tVac.OriginalEndDate.ToShortDateString() + ">",
                    VerticalAlignment = VerticalAlignment.Center
                });
                tvi.Items.Add(new TreeViewItem
                {
                    Tag = tVac,
                    Header = dp
                });
            }
        }

        /// <summary>
        /// Удаление отпуска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DelVac_Click(object sender, RoutedEventArgs e)
        {
            var res = await this.ShowMessageAsync("Предупреждение", "Действительно хотите оставить работника без отпуска?", MessageDialogStyle.AffirmativeAndNegative);
            if (res == MessageDialogResult.Affirmative)
            {
                var tvi = TreeViewWorkers.SelectedItem as TreeViewItem;
                if (tvi != null)
                {
                    //прям велосипед
                    var emp = (tvi.Parent as TreeViewItem).Tag as Employee;
                    var vp = tvi.Tag as VacationPair;
                    int index = -1;
                    foreach(var v in emp.VacList)
                    {
                        if(v == vp)
                            index = emp.VacList.IndexOf(v);
                    }

                    emp.VacList.RemoveAt(index);
                    DeHighLightVac(vp, emp);
                    var itemsControl = tvi.Parent as ItemsControl;
                    itemsControl.Items.Remove(tvi);
                }
            }
        }

        /// <summary>
        /// Редактировать отпуск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditVac_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new AddVacation();
            wnd.ShowDialog();

            if (wnd.DialogResult == true)
            {
                DateTime dateBegin = wnd.DateBegin, dateEnd = wnd.DateEnd;
                var treeViewItem = TreeViewWorkers.SelectedItem as TreeViewItem;
                if (treeViewItem != null)
                {
                    var emp = (treeViewItem.Parent as TreeViewItem).Tag as Employee;
                    var vp = treeViewItem.Tag as VacationPair;
                    int index = -1;
                    foreach (var v in emp.VacList)
                    {
                        if (v == vp)
                            index = emp.VacList.IndexOf(v);
                    }

                    emp.VacList.RemoveAt(index);

                    var tVac = CorrectVacation(new VacationPair(dateBegin, dateEnd));
                    emp.VacList.Insert(index, tVac);

                    //снятие подсветки с отпуска
                    DeHighLightVac(vp, emp);
                    //подсвечивание
                    //HighlightVac(tVac, emp.BrColor, emp);

                    var itemsControl = treeViewItem.Parent as ItemsControl;
                    itemsControl.Items.Remove(treeViewItem);
                    string vacString = tVac.Begin.Date.ToShortDateString() + " - " + tVac.End.Date.ToShortDateString();

                    var dp = new DockPanel();

                    dp.Children.Add(new Image
                    {
                        Source = new BitmapImage(new Uri(@"pack://application:,,,/MonthCalendar;component/Icons/bag.ico")),
                        Height = 18,
                        Width = 18,
                        Margin = new Thickness(5, 0, 8, 0),
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    dp.Children.Add(new Label
                    {
                        Content = tVac.OriginalEndDate == tVac.End ? vacString : vacString + " <" + tVac.OriginalEndDate.ToShortDateString() + ">",
                        VerticalAlignment = VerticalAlignment.Center
                    });
                    itemsControl.Items.Add(new TreeViewItem
                    {
                        Tag = tVac,
                        Header = dp
                    });
                }
                TreeViewWorkers.Items.Refresh();
            }
        }

        /// <summary>
        /// Подсветить отпуск (событие)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HighLightVacAs_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = TreeViewWorkers.SelectedItem as TreeViewItem;
            Brush tBr = ((tvi.Parent as TreeViewItem).Tag as Employee).BrColor;
            if ((tvi.Tag as VacationPair).IsShown == false)
            {
                HighlightVac(tvi.Tag as VacationPair, tBr, (tvi.Parent as TreeViewItem).Tag as Employee);
            }
            else
            {
                ShowMessageBox("Отпуск уже отображен в календаре!", "Ошибка!");
            }
        }

        /// <summary>
        /// Убрать подсветку отпуска (событие)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeHighLightVac_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = TreeViewWorkers.SelectedItem as TreeViewItem;
            var emp = (tvi.Parent as TreeViewItem).Tag as Employee;
            var vac = tvi.Tag as VacationPair;
            if (vac.IsShown)
            {
                DeHighLightVac(vac, emp);
            }
            else
            {
                ShowMessageBox("Отпуска нет на календаре!", "Ошибка!");
            }
        }

        /// <summary>
        /// подсветить отпуск
        /// </summary>
        /// <param name="vac">Отпуск</param>
        /// <param name="color">Цвет</param>
        /// <param name="emp">Сотрудник</param>
        private void HighlightVac(VacationPair vac, Brush color, Employee emp)
        {
            List<VacationPair> lVac = DivideByMonth(vac);
            List<VacationPair> lVacCurYear = lVac.FindAll(x=>x.Begin.Year==curMonth.Year);
            List<VacationPair> lVacNotCurYear = lVac.FindAll(x => x.Begin.Year != curMonth.Year);
            for (int i = 0; i < lVacCurYear.Count; i++)
            {
                _months[lVacCurYear[i].Begin.Month - 1].AddVacation(lVacCurYear[i], color, emp);
            }
            for (int i = 0; i < lVacNotCurYear.Count; i++)
            {
                if (_monthss.ContainsKey(lVacNotCurYear[i].Begin.Year))
                    _monthss[lVacNotCurYear[i].Begin.Year][lVacNotCurYear[i].Begin.Month - 1].AddVacation(lVacNotCurYear[i], color, emp);
                else
                {
                    SaveCurentYear();
                    curMonth = curMonth.AddYears(1);
                    ChangeYear(curMonth.Year);
                    SaveCurentYear();
                    curMonth = curMonth.AddYears(-1);
                    ChangeYear(curMonth.Year);
                    _monthss[lVacNotCurYear[i].Begin.Year][lVacNotCurYear[i].Begin.Month - 1].AddVacation(lVacNotCurYear[i], color, emp);               
                }
            }
            vac.IsShown = true;
        }

        /// <summary>
        /// Снять подсветку у отпуска. Год/месяц доделать
        /// </summary>
        /// <param name="vac">Отпуск</param>
        /// <param name="emp">Сотрудник</param>
        private void DeHighLightVac(VacationPair vac, Employee emp)
        {
            //List<VacationPair> lVac = DivideByMonth(vac);
            //for (int i = 0; i < lVac.Count; i++)
            //{
            //    _months[lVac[i].Begin.Month - 1].DeleteVacation(lVac[i], emp);
            //}

            List<VacationPair> lVac = DivideByMonth(vac);
            List<VacationPair> lVacCurYear = lVac.FindAll(x => x.Begin.Year == curMonth.Year);
            List<VacationPair> lVacNotCurYear = lVac.FindAll(x => x.Begin.Year != curMonth.Year);
            for (int i = 0; i < lVacCurYear.Count; i++)
            {
                _months[lVacCurYear[i].Begin.Month - 1].DeleteVacation(lVacCurYear[i], emp);
            }
            try
            {
                for (int i = 0; i < lVacNotCurYear.Count; i++)
                {
                    _monthss[lVacNotCurYear[i].Begin.Year][lVacNotCurYear[i].Begin.Month - 1].DeleteVacation(lVacNotCurYear[i], emp);
                }
                vac.IsShown = false;
            }
            catch
            {
                //ShowMessageBox("Какая-то ошибка при удалении отпуска с календаря. Попробуй еще раз...", "Ошибка");
            }
        }

        /// <summary>
        /// Если отпуск попадает на 1+ месяцев
        /// </summary>
        /// <param name="vac">Отпуск</param>
        /// <returns></returns>
        private List<VacationPair> DivideByMonth(VacationPair vac)
        {
            VacationPair tVac = new VacationPair(vac.Begin, vac.End);
            List<VacationPair> retList = new List<VacationPair>();
            //DateTime tDT = new DateTime(vac.Begin.Year, vac.Begin.Month, 0);
            for (DateTime tDt = new DateTime(vac.Begin.Year, vac.Begin.Month, 1); tDt < vac.End; tDt = tDt.AddMonths(1))
            {
                DateTime tEnd = new DateTime(tVac.Begin.Year, tVac.Begin.Month,
                    DateTime.DaysInMonth(tVac.Begin.Year, tVac.Begin.Month));
                if (tEnd < tVac.End)
                    retList.Add(new VacationPair(tVac.Begin, tEnd));
                else
                    retList.Add(new VacationPair(tVac.Begin, tVac.End));
                tVac.Begin = tEnd.AddDays(1);
            }
            return retList;
        }

        #endregion

        #region Смена/отображение года

        /// <summary>
        /// отобразить год по номеру
        /// </summary>
        /// <param name="newYear"></param>
        private void ChangeYear(int newYear)
        {

            if (_monthss.ContainsKey(newYear))
            {
                GridYear.Children.Clear();
                _months = _monthss[newYear];
                DateTime curDate = new DateTime(newYear, 1, 1);
                YearNum.Content = newYear.ToString();
                for (int i = 0; i < _months.Count; i++)
                {
                    GridYear.Children.Add(_months[i]);
                }
            }
            else
            {
                //GridYear.Children.Clear();
                _months = new List<MonthOfYear>();
                DateTime curDate = new DateTime(newYear, 1, 1);
                YearNum.Content = newYear.ToString();
                for (var row = 0; row < GridYear.RowDefinitions.Count; row++)
                {
                    for (var col = 0; col < GridYear.ColumnDefinitions.Count; col++)
                    {
                        MonthOfYear month = new MonthOfYear(curDate);
                        month.ButtonClick += new EventHandler(MonthOfYear_ButtonClick);
                        Grid.SetColumn(month, col);
                        Grid.SetRow(month, row);
                        GridYear.Children.Add(month);

                        _months.Add(month);

                        curDate = curDate.AddMonths(1);
                    }
                }
            }
        }
        private void SaveCurentYear()
        {
            if (!_monthss.ContainsKey(curMonth.Year))
            {
                _monthss.Add(curMonth.Year, new List<MonthOfYear>());
            }
            _monthss[curMonth.Year] = _months;
        }
        private void RestoreYear(int restoreYear)
        {
            for (int i = 0; i < _monthss[restoreYear].Count; i++)
            {
                GridYear.Children.Add(_monthss[restoreYear][i]);
                _months = _monthss[restoreYear];
            }
        }
        private Grid BuildGridYear()
        {
            Grid retGrid = new Grid();
            retGrid.ColumnDefinitions.Add(new ColumnDefinition());
            retGrid.ColumnDefinitions.Add(new ColumnDefinition());
            retGrid.ColumnDefinitions.Add(new ColumnDefinition());
            retGrid.ColumnDefinitions.Add(new ColumnDefinition());
            retGrid.RowDefinitions.Add(new RowDefinition());
            retGrid.RowDefinitions.Add(new RowDefinition());
            retGrid.RowDefinitions.Add(new RowDefinition());
            retGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            Grid.SetColumn(retGrid, 1);
            retGrid.Margin = new Thickness(10);
            return retGrid;
        }

        /// <summary>
        /// следующий год
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bNextYear_Click(object sender, RoutedEventArgs e)
        {
            SaveCurentYear();
            curMonth = curMonth.AddYears(1);
            ChangeYear(curMonth.Year);
        }

        /// <summary>
        /// предыдущий год
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bPreviousYear_Click(object sender, RoutedEventArgs e)
        {
            SaveCurentYear();
            curMonth = curMonth.AddYears(-1);
            ChangeYear(curMonth.Year);
        }
        #endregion

        #region Смена/отображение месяца

        /// <summary>
        /// отобразить месяц
        /// </summary>
        /// <param name="n"></param>
        private void ShowMonth(int n)
        {
            GridYear.Children.Remove(_months[n]);
            GridYear.Visibility = Visibility.Collapsed;
            GridChangeYear.Visibility = Visibility.Collapsed;
            GridMonth.Children.Add(_months[n]);
            _months[n].ButMonth.IsEnabled = false;
            GridMonth.Visibility = Visibility.Visible;
            GridChangeMonth.Visibility = Visibility.Visible;
            MonthNum.Content = _months[n].Dt.ToString("MMMM") + " - " + _months[n].Dt.Year.ToString();
        }

        /// <summary>
        /// событие отображения месяца по клику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MonthOfYear_ButtonClick(object sender, EventArgs e)
        {
            MonthOfYear mofArg = sender as MonthOfYear;
            //MessageBox.Show("Вы выбрали <" + mofArg.Dt.ToString("MMMM") + ", " + mofArg.Dt.Year + ">!");

            int num = _months.FindIndex(x => x == mofArg);

            ShowMonth(num);
        }

        /// <summary>
        /// отобразить предыдущий месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bPreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            MonthOfYear mOY = GridMonth.Children[0] as MonthOfYear;
            int num = _months.IndexOf(mOY);
            if (num > 0)
            {
                GridMonth.Children.Remove(mOY);
                GridYear.Children.Add(mOY);
                mOY.ButMonth.IsEnabled = true;
                ShowMonth(num - 1);
                curMonth = curMonth.AddMonths(-1);
            }
            else
                ShowMessageBox("Вернитесь на вкладку года и перейдите к предыдущему для отображения предыдущего месяца", "Подсказка");

        }

        /// <summary>
        /// отобразить год
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bMonthNum_Click(object sender, RoutedEventArgs e)
        {
            MonthOfYear mOY = GridMonth.Children[0] as MonthOfYear;
            GridMonth.Children.Remove(mOY);
            GridYear.Children.Add(mOY);
            mOY.ButMonth.IsEnabled = true;

            GridYear.Visibility = Visibility.Visible;
            GridChangeYear.Visibility = Visibility.Visible;
            GridMonth.Visibility = Visibility.Collapsed;
            GridChangeMonth.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// отобразить следующий месяц
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bNextMonth_Click(object sender, RoutedEventArgs e)
        {
            MonthOfYear mOY = GridMonth.Children[0] as MonthOfYear;
            int num = _months.IndexOf(mOY);
            if (num < 11)
            {
                GridMonth.Children.Remove(mOY);
                GridYear.Children.Add(mOY);
                mOY.ButMonth.IsEnabled = true;
                ShowMonth(num + 1);
                curMonth = curMonth.AddMonths(1);
            }
            else
                ShowMessageBox("Вернитесь на вкладку года и перейдите к следующему для отображения следующего месяца", "Подсказка");

        }

        /// <summary>
        /// вернуться в представление ГОД
        /// </summary>
        /// <param name="mOY"></param>
        private void ReturnMonthToYear(MonthOfYear mOY)
        {
            GridYear.Children.Add(mOY);
        }

        /// <summary>
        /// отобразить отпуска заново
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        private void RebildVacsInMonth(int year, int month)
        {
            _months[month].Rebuild();
        }
        #endregion

        #region Кнопки меню

        /// <summary>
        /// Добавим праздничный день
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRedDay(object sender, ExecutedRoutedEventArgs e)
        {
            var wnd = new RedDay();
            wnd.ShowDialog();

            if (wnd.shortDates != null || wnd.redDates != null)
            {
                redDays = new List<DateTime>(wnd.redDates);
                shortDays = new List<DateTime>(wnd.shortDates);
            }
            //GridYear.Children.Clear();
            //DrawCurrentYear();
            //SelectAllHolidays();
            //foreach(Employee emp in _emps)
            //{
            //    for(int i = 0; i < emp.VacList.Count; i++)
            //    {
            //        emp.VacList[i] = CorrectVacation(emp.VacList[i]);
            //    }
            //}
            if(_emps.Count > 0)
                FillTreeByEmployees(_emps);
        }

        /// <summary>
        /// О программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About(object sender, ExecutedRoutedEventArgs e)
        {
            var wnd = new About();
            wnd.ShowDialog();
        }

        /// <summary>
        /// Помощь
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help(object sender, ExecutedRoutedEventArgs e)
        {
            //MessageBox.Show("Help");
            var wnd = new Help();
            wnd.ShowDialog();
        }

        /// <summary>
        /// Сохранить список сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butSaveEmpList_Click(object sender, RoutedEventArgs e)
        {
            foreach(var emp in _emps)
            {
             foreach(var vac in emp.VacList)
                {
                    vac.IsShown = false;
                }   
            }

            serializerEmp = new XmlSerializer(typeof(Employee[]));
            Employee[] em = _emps.ToArray();

            using (FileStream fs = new FileStream("Employees.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    serializerEmp.Serialize(fs, em);
                }
                catch (InvalidOperationException ioe)
                {
                    ShowMessageBox(ioe.InnerException.Message, "Ошибка");
                }
            }        
            ShowMessageBox("Сохранено", "Информация");
        }

        private void butRefreshEmpList_Click(object sender, RoutedEventArgs e)
        {
            if (_emps.Count > 0)
                FillTreeByEmployees(_emps);
            else
                ShowMessageBox("Список сотрудников пуст. Нечего обновлять.", "Информация");
        }

        private void MainMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MetroDialogSettings ms = new MetroDialogSettings
            {
                ColorScheme = MetroDialogColorScheme.Theme,
                AnimateShow = true,
                AnimateHide = true,
                AffirmativeButtonText = "Да",
                NegativeButtonText = "Нет, сейчас сохраню",
                FirstAuxiliaryButtonText = "Выйти сейчас",
                DialogMessageFontSize = 24, 
                DialogTitleFontSize = 30,
            };
            if(!confirmed)
            { 
                e.Cancel = true;
                ShowClosingDialogBox("Вы сохранили изменения перед выходом из программы?", string.Format("Предупреждение"), ms);
            }

            dtNotify.Stop();
        }

        #endregion

        #region Подсветка праздников

        /// <summary>
        /// Вырезать некоторый месяц
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="fromCut"></param>
        /// <returns></returns>
        private List<DateTime> CutMonth(int year, int month, List<DateTime> fromCut)
        {
            return fromCut.FindAll(x => x.Year == year && x.Month == month);
        }

        private VacationPair CorrectVacation(VacationPair cVac)
        {
            int count = 0, newCount = 1;

            //до тех пор, пока в отпуск перестану попадать праздничные дни
            while (newCount > count)
            {
                //сколько праздничных дней в промежутке
                newCount = redDays.Count(x => ((cVac.Begin < x || cVac.Begin == x) && (x < cVac.OriginalEndDate || cVac.OriginalEndDate == x)));
                cVac.End = cVac.OriginalEndDate.AddDays(newCount);

                count = newCount;
                //проверим, попадут ли в исправленный отпуск еще праздничные дни
                newCount = redDays.Count(x => ((cVac.Begin < x || cVac.Begin == x) && (x < cVac.OriginalEndDate || cVac.OriginalEndDate == x)));
            }
            return cVac;
        }

        /// <summary>
        /// подсветить все праздники на календаре
        /// </summary>
        private void SelectAllHolidays()
        {
            foreach (int kInt in _monthss.Keys)
            {
                foreach (MonthOfYear month in _monthss[kInt])
                {
                    month.HighlightRed(CutMonth(kInt, month.Dt.Month, redDays));
                    month.HighlightGreen(CutMonth(kInt, month.Dt.Month, shortDays));
                }
            }
            foreach (MonthOfYear month in _months)
            {
                month.HighlightRed(CutMonth(month.Dt.Year, month.Dt.Month, redDays));
                month.HighlightGreen(CutMonth(month.Dt.Year, month.Dt.Month, shortDays));
            }
        }
        #endregion
    }
}