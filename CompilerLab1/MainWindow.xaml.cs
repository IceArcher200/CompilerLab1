using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Enumeration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
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

        string currentFilePath;
        int tabsCount = 0;
        List<Tab> listTabs = new List<Tab>();
        int maxLC = 1;
        public MainWindow()
        {
            InitializeComponent();
            Create_New_Tab(null);
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Create_New_Tab(null);
            listTabs[tabs.SelectedIndex].TextChanged = false;
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                Create_New_Tab(ofd.FileName);
                RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
                TextRange doc = new TextRange(inputBox.Document.ContentStart, inputBox.Document.ContentEnd);
                using FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                if (Path.GetExtension(ofd.FileName).ToLower() == ".txt")
                    doc.Load(fs, DataFormats.Text);
                else
                    doc.Load(fs, DataFormats.Xaml);
                listTabs[tabs.SelectedIndex].TextChanged = false;
                currentFilePath = ofd.FileName;
                MessageBox.Show(tabs.Items.Count.ToString());
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentFilePath == null)
                SaveAs();
            else
                Save();
        }
        private void Save()
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            TextRange doc = new TextRange(inputBox.Document.ContentStart, inputBox.Document.ContentEnd);
            using FileStream fs = new FileStream(currentFilePath, FileMode.Open);
            doc.Save(fs, DataFormats.Text);
            listTabs[tabs.SelectedIndex].TextChanged = false;
        }
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }
        private void SaveAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == true)
            {
                RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
                using FileStream fs = File.Create(sfd.FileName);
                TextRange doc = new TextRange(inputBox.Document.ContentStart, inputBox.Document.ContentEnd);
                if (Path.GetExtension(sfd.FileName).ToLower() == ".txt")
                    doc.Save(fs, DataFormats.Text);
                else
                    doc.Save(fs, DataFormats.Xaml);
                listTabs[tabs.SelectedIndex].FilePath = sfd.FileName;
                listTabs[tabs.SelectedIndex].TextChanged = false;
                currentFilePath = sfd.FileName;
            }
        }
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Undo();
        }
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Redo();
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Copy();
        }
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Cut();
        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Paste();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Selection.Text = "";
        }
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox inputBox = listTabs[tabs.SelectedIndex].InputBox;
            inputBox.Focus();
            inputBox.SelectAll();
        }
        private bool AskSave(bool textChanged, string fileName)
        {
            bool needSave = false;
            if (textChanged)
            {
                Save save = new Save();
                save.Owner = this;
                save.fileName = fileName;
                save.NeedSave += value => needSave = value;
                save.ShowDialog();
            }
            return needSave;
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            listTabs[tabs.SelectedIndex].TextChanged = true;
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            Help help = new Help();
            help.Owner = this;
            help.ShowDialog();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("На данный момент данная программа является GUI для будущего языкового редактора");
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < tabs.Items.Count; i++)
            {
                string fileName = Path.GetFileName(listTabs[tabs.SelectedIndex].FilePath);
                tabs.SelectedIndex = i;
                if (AskSave(listTabs[tabs.SelectedIndex].TextChanged, fileName))
                {
                    if (listTabs[tabs.SelectedIndex].FilePath == null)
                        SaveAs();
                    else
                    {
                        currentFilePath = listTabs[tabs.SelectedIndex].FilePath;
                        Save();
                    }
                }
            }
        }

        private void File_Drop(object sender, DragEventArgs e)
        {
            string[] filePath = e.Data.GetData(DataFormats.FileDrop) as string[];
            TextRange doc = Create_New_Tab(filePath[0]);
            using (FileStream fs = new FileStream(filePath[0], FileMode.Open))
            {
                doc.Load(fs, DataFormats.Text);
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            bool first = true;
            var lexer = new Lexer();
            lexer.AddDefinition(new TokenDefinition("KEYWORD", new Regex(@"let", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("IDENTIFIER", new Regex(@"[a-z]+[a-z0-9_]*", RegexOptions.Compiled | RegexOptions.IgnoreCase)));
            lexer.AddDefinition(new TokenDefinition("ASSIGNMENT", new Regex(@"=", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("OPEN_BRACE", new Regex(@"{", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("CLOSE_BRACE", new Regex(@"}", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("LINE", new Regex(@"""[^""\\]*(?:\\.[^""\\]*)*""", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("COMMA", new Regex(@",", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("COLON", new Regex(@":", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("COMPLEX_NUMBER", new Regex(@"\d+(\.\d+)", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("NEGATIVE_NUMBER", new Regex(@"-\d+", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("NUMBER", new Regex(@"\d+", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("END_MASSIVE", new Regex(@";", RegexOptions.Compiled)));
            lexer.AddDefinition(new TokenDefinition("SEPARATOR", new Regex(@" ", RegexOptions.Compiled), true));
            lexer.AddDefinition(new TokenDefinition("LINE_BREAK", new Regex(@"\r?\n", RegexOptions.Compiled), true));

            RichTextBox input = listTabs[tabs.SelectedIndex].InputBox;
            TextRange doc = new TextRange(input.Document.ContentStart, input.Document.ContentEnd);

            List<Token> tokens = lexer.Tokenize(doc.Text);

            var typeBinding = new Binding("Type")
            {
                Mode = BindingMode.Default,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            string resultString = "";
            ResultBox.Clear();
            Parser parser = new Parser();
            for (int i = 0;i < tokens.Count;i++)
            {
                bool isContains = false;
                bool flag = false;
                for(int j = i; j < tokens.Count;j++)
                {
                    if (parser.Parse(tokens[j], false) != States.ERROR)
                    {
                        isContains = true;
                    }
                }
                if (!isContains)
                {
                    flag = false;
                    States tempState = parser.currentState;
                    States temp2State;
                    while(parser.currentState != States.ERROR && flag == false)
                    {
                        if (parser.Parse(tokens[i],false) != States.ERROR) // Найден символ продолжение
                        {
                            while(tempState != parser.currentState)
                            {
                                ResultBox.Text += "Ожидалось '" + parser.getToken(tempState) + "' перед " + tokens[i] + "\n";
                                resultString += parser.getToken(tempState) + " ";
                                tempState++;
                            }
                            resultString += tokens[i].Value + " ";
                            parser.Parse(tokens[i], true);
                            flag = true;
                            if (i == tokens.Count - 1)
                            {
                                temp2State = parser.currentState;
                                while ((temp2State != States.ERROR) && parser.currentState != States.START)
                                {
                                    ResultBox.Text += "Ожидалось '" + parser.getToken(temp2State) + "' после " + tokens[i] + "\n";
                                    resultString += parser.getToken(temp2State) + " ";
                                    temp2State++;
                                }
                                tempState = States.START;
                            }
                        }
                        if (!flag) 
                            parser.currentState++;
                    }
                    if (!flag)
                    {
                        parser.currentState = tempState;
                        if (i == tokens.Count - 1)
                        {
                            if (parser.Parse(tokens[i], true) == States.ERROR)
                                ResultBox.Text += "Ошибка " + tokens[i].ToString() + "\n";
                            while ((parser.currentState != States.ERROR) && parser.currentState != States.START)
                            {
                                ResultBox.Text += "Ожидалось '" + parser.getToken(parser.currentState) + "' после " + tokens[i] + "\n";
                                resultString += parser.getToken(parser.currentState) + " ";
                                parser.currentState++;
                            }
                            parser.currentState = States.START;
                        }
                        else if (parser.Parse(tokens[i], false) == States.ERROR)
                        {
                            ResultBox.Text += "Ошибка " + tokens[i].ToString() + "\n";
                        }
                        else
                        {
                            resultString += tokens[i].Value + " ";
                            parser.Parse(tokens[i], true);
                        }
                    }
                }
                else if (i == tokens.Count - 1)
                {
                    resultString += parser.getToken(parser.currentState) + " ";
                    if (parser.Parse(tokens[i], true) == States.ERROR)
                        ResultBox.Text += "Ошибка " + tokens[i].ToString() + "\n";
                    while ((parser.currentState != States.ERROR) && parser.currentState != States.START)
                    {
                        ResultBox.Text += "Ожидалось '" + parser.getToken(parser.currentState) + "' после " + tokens[i] + "\n";
                        resultString += parser.getToken(parser.currentState) + " ";
                        parser.currentState++;
                    }
                    
                    parser.currentState = States.START;
                }
                else if (parser.Parse(tokens[i], false) == States.ERROR)
                {  
                    ResultBox.Text += "Ошибка " + tokens[i].ToString() + "\n";
                }
                else
                {
                    resultString += tokens[i].Value + " ";
                    parser.Parse(tokens[i], true);
                }

            }
            if (ResultBox.Text == "")
                ResultBox.Text += "No Errors";
            ResultBox.Text += "\n Исправленная строка: \n" + resultString;
        }

        private TextRange Create_New_Tab(string filePath)
        {
            Grid grid = new Grid();

            RichTextBox input = new RichTextBox();
            input.Margin = new Thickness(30, 10, 10, 10);
            input.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            Paragraph p = input.Document.Blocks.FirstBlock as Paragraph;
            p.LineHeight = 1;
            input.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            input.HorizontalAlignment = HorizontalAlignment.Right;
            input.TextChanged += TextChanged;
            /*
            DataGrid resultTable = new DataGrid();
            resultTable.Columns.Add(new DataGridTextColumn {Header = "Type"});
            resultTable.Columns.Add(new DataGridTextColumn {Header = "Value"});
            resultTable.Columns.Add(new DataGridTextColumn {Header = "Index"});
            resultTable.Columns.Add(new DataGridTextColumn {Header = "Line"});
            resultTable.Columns.Add(new DataGridTextColumn {Header = "Column"});
            grid.Children.Add(resultTable);*/

            grid.Children.Add(input);

            TextRange doc = new TextRange(input.Document.ContentStart, input.Document.ContentEnd);
            string fileName = Path.GetFileName(filePath);
            if (fileName == null) fileName = "Безымянный";
            listTabs.Add(new Tab(input, filePath));
            TabItem tab = new TabItem
            {
                Header = fileName,
                Content = grid,
                TabIndex = tabsCount
            };
            tabs.Items.Add(tab);
            tab.Focus();
            tabsCount++;
            return doc;

        }

        private void TabControl_SelectedItem(object sender, SelectionChangedEventArgs e)
        {
            currentFilePath = listTabs[tabs.SelectedIndex].FilePath;
        }
    }
}
