using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
//using System.Windows.Shapes;

namespace CompilerLab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentFileStream;
        string currentFileFormat;
        bool textChanged = false;
        public MainWindow()
        {
            InitializeComponent();
            FlowDocument myFlowDoc = new FlowDocument();
            myFlowDoc.Blocks.Add(new Paragraph(new Run("Something")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("Anything")));
            myFlowDoc.Blocks.Add(new Paragraph(new Run("??thing")));
            ResultBox.Document = myFlowDoc;
        }


        private void Create_Click(object sender, RoutedEventArgs e)
        {
            AskSave(sender, e);
            textChanged = false;
            TextRange doc = new TextRange(InputBox.Document.ContentStart, InputBox.Document.ContentEnd);
            doc.Text = "";
            currentFileStream = null;
            currentFileFormat = "";
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            AskSave(sender, e);
            textChanged = false;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                TextRange doc = new TextRange(InputBox.Document.ContentStart, InputBox.Document.ContentEnd);
                using FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                    doc.Load(fs, DataFormats.Text);   
                else
                    doc.Load(fs, DataFormats.Xaml);
                currentFileStream = ofd.FileName;
                currentFileFormat = Path.GetExtension(ofd.FileName).ToLower();
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            textChanged = false;
            TextRange doc = new TextRange(InputBox.Document.ContentStart, InputBox.Document.ContentEnd);
            if (currentFileStream == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                if (sfd.ShowDialog() == true)
                {
                    
                    using FileStream fs = File.Create(sfd.FileName);
                    if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                        doc.Save(fs, DataFormats.Text);
                    else
                        doc.Save(fs, DataFormats.Xaml);
                    currentFileStream = sfd.FileName;
                    currentFileFormat = Path.GetExtension(sfd.FileName).ToLower();
                }
            }
            else
            {
                using FileStream fs = new FileStream(currentFileStream, FileMode.Open);
                if (currentFileFormat == ".txt")
                    doc.Save(fs, DataFormats.Text);
                else
                    doc.Save(fs, DataFormats.Xaml);
            }
        }
        private void SaveHow_Click(object sender, RoutedEventArgs e)
        {
            textChanged = false;
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == true)
            {

                using FileStream fs = File.Create(sfd.FileName);
                TextRange doc = new TextRange(InputBox.Document.ContentStart, InputBox.Document.ContentEnd);
                if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                    doc.Save(fs, DataFormats.Text);
                else
                    doc.Save(fs, DataFormats.Xaml);
                currentFileStream = sfd.FileName;
                currentFileFormat = Path.GetExtension(sfd.FileName).ToLower();
            }
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Undo();
        }
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Redo();
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Copy();
        }
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Cut();
        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Paste();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Selection.Text = "";
        }
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Focus();
            InputBox.SelectAll();
        }

        private void AskSave(object sender, RoutedEventArgs e)
        {
            if (textChanged)
            {
                bool needSave = false;
                Save save = new Save();
                save.Owner = this;
                save.NeedSave += value => needSave = value;
                save.ShowDialog();
                if (needSave)
                    Save_Click(sender, e);
            }
        }
        
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            AskSave(sender, e);
            this.Close();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!textChanged) textChanged = true;
            
        }
        
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.Owner = this;
            help.ShowDialog();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutProgram aboutProgram = new AboutProgram();
            aboutProgram.Owner = this;
            aboutProgram.ShowDialog();
        }
    }
}
