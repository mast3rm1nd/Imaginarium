using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imaginarium
{    
    public partial class MainWindow : Window
    {
        static List<PhysicalObject[]> physicalObjectsList = new List<PhysicalObject[]>();
        static List<string> convertorNames = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            physicalObjectsList.Add(new PhysicalObject[]
            {
                new PhysicalObject { ComboboxName = "Земля", TextboxName = "Масс Земли", Value = 5.9736E24 },
                new PhysicalObject { ComboboxName = "Легковой автомобиль [2 т]", TextboxName = "Легковых автомобилей [2 т]", Value = 2000},
                new PhysicalObject { ComboboxName = "Человек [62 кг]", TextboxName = "Людей [62 кг]", Value = 62},
                new PhysicalObject { ComboboxName = "Солнце", TextboxName = "Солнечных масс", Value = 1.98892E30 },
                new PhysicalObject { ComboboxName = "Зерно [35 мг]", TextboxName = "Зёрен [35 мг]", Value = 35E-3},
                new PhysicalObject { ComboboxName = "Электрон", TextboxName = "Электронов", Value = 9.1093822E-31 },
                new PhysicalObject { ComboboxName = "Луна", TextboxName = "Лунных масс", Value =  7.3477E22},
                new PhysicalObject { ComboboxName = "Юпитер", TextboxName = "Масс Юпитера", Value =  1.8986E27},
                //new PhysicalObject { ComboboxName = "", TextboxName = "", Value =  },
            });
            convertorNames.Add("Масса");



            physicalObjectsList.Add(new PhysicalObject[]
            {
                new PhysicalObject { ComboboxName = "Футбольное поле", TextboxName = "Футбольных полей", Value = 105 },
                new PhysicalObject { ComboboxName = "Рост человека [170 см]", TextboxName = "Человеческих ростов [170 см]", Value =  1.7},
                new PhysicalObject { ComboboxName = "От Земли до Солнца", TextboxName = "Расстояний от Земли до Солнца", Value =  1.496E11},
                new PhysicalObject { ComboboxName = "От Земли до Луны", TextboxName = "Расстояний от Земли до Луны", Value = 384400E3 },
                new PhysicalObject { ComboboxName = "Диаметр солнечной системы", TextboxName = "Диаметров солнечной системы", Value = 9.09E12  },
                new PhysicalObject { ComboboxName = "Световой год", TextboxName = "Световых лет", Value =  9.4607E15},
                //new PhysicalObject { ComboboxName = "", TextboxName = "", Value =  },
            });
            convertorNames.Add("Длина");



            for (int i = 0; i < physicalObjectsList.Count; i++)            
                physicalObjectsList[i] = physicalObjectsList[i].OrderBy(o => o.Value).ToArray();
            
            


            PhysicalValue_comboBox.ItemsSource = convertorNames;
            PhysicalValue_comboBox.SelectedIndex = 0;
        }

        private void PhysicalValue_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object_comboBox.ItemsSource = physicalObjectsList[PhysicalValue_comboBox.SelectedIndex];

            Object_comboBox.SelectedIndex = 0;
        }

        private void Object_comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Object_comboBox.SelectedItem == null) return;

            var currObject = (PhysicalObject)Object_comboBox.SelectedItem;

            var results = new StringBuilder();

            foreach (var physicalObject in physicalObjectsList[PhysicalValue_comboBox.SelectedIndex])
            {
                if (physicalObject == currObject) continue;

                results.Append($"{physicalObject.TextboxName}: {currObject.Value / physicalObject.Value:N3}" + Environment.NewLine);
            }

            Results_textBox.Text = results.ToString();
        }
    }

    class PhysicalObject : IEquatable<PhysicalObject>
    {
        public string ComboboxName { get; set; }
        public string TextboxName { get; set; }
        public double Value { get; set; }

        public bool Equals(PhysicalObject other)
        {
            return
                (
                    this.ComboboxName == other.ComboboxName &&
                    this.TextboxName == other.TextboxName &&
                    this.Value == other.Value
                );
        }

        public override string ToString()
        {
            return ComboboxName;
        }
    }
}
