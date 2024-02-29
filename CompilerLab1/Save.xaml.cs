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

namespace CompilerLab1
{
    /// <summary>
    /// Логика взаимодействия для Save.xaml
    /// </summary>
    public partial class Save : Window
    {
        public event Action<bool> NeedSave;
        public string fileName { get; set; }
        public Save()
        {
            InitializeComponent();
            textBox.Text = "Желаете сохранить изменения в " + fileName;
        }
        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            NeedSave(true);
            this.Close();
        }
        private void No_Click(object sender, RoutedEventArgs e)
        {
            NeedSave(false);
            this.Close();
        }

    }
}
