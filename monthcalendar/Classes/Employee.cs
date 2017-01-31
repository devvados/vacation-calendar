using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MonthCalendar
{
    [System.Serializable]
    public class Employee
    {
        [XmlElement("name")]
        public string name;
        [XmlElement("surname")]
        public string surname;
        [XmlArray("vacations")]
        public List<VacationPair> _vacList = new List<VacationPair>();
        [XmlElement("color")]
        public string hexademical;
        //Brush brColor;

        
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
       
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
            }
        }
        
        public string Hex
        {
            get { return hexademical; }
            set { hexademical = value; }
        }
        [XmlIgnore]
        public System.Windows.Media.Brush BrColor 
        {
            get
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexademical));
            }
            set
            {
                hexademical = ((SolidColorBrush)value).Color.ToString();
            }
        }
        
        public List<VacationPair> VacList
        {
            get; set;
        }
        public Employee(string name, string surname, Brush br)
        {
            Name = name;
            Surname = surname;
            BrColor = br;
            VacList = new List<VacationPair>();
        }
        public Employee()
        {
        }
    }

}
