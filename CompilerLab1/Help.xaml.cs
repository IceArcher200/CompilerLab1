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
    /// Логика взаимодействия для Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
            FlowDocument myFlowDoc = new FlowDocument();
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Создать' позволяет создать новый пустой файл")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Открыть' позволяет открыть существующий файл для последующей работы с ним")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Сохранить' позволяет сохранить изменения в существующий файл")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Сохранить как' позволяет сохранить изменения в новый файл")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Выход' позволяет закрыть программу")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Отменить' позволяет отменить последнее выполненное действие")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Повторить' позволяет вернуть последнее выполненное действие")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Вырезать' позволяет удалить выделенный текст, сохранив его в буфере обмена")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Копировать' позволяет сохранить выделенный текст в буфере обмена")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Вставить' позволяет вставить текст, сохранённый в буфере обмена,в выбранное место")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Удалить' позволяет удалить выделенный текст")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("'Выделить все' позволяет выделить весь текст")));
            
            HelpBox.Document = myFlowDoc;
        }
    }
}
