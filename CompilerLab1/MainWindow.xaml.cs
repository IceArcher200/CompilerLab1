using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Help.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\About.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void Classification_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Classification.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void Grammar_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Grammar.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void Literature_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\LiteratureList.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void Irons_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Irons.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }

        private void Analysis_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Analysis.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }
        private void Task_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Task.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }
        private void Examples_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "\\Examples.html")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
        }
        private void Source_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"https://github.com/IceArcher200/CompilerLab1/tree/Coursework")
            {
                UseShellExecute = true
            };
            p.Start();
            p.Close();
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

        string lexText(string text)
        {

            StringBuilder finalTextString = new StringBuilder();
            Parser parser = new Parser();
            List<Error> errors = FindErrors(text, finalTextString, ref parser);

            if (errors.Count > 0)
            {
                for (int i = 0; i < errors.Count; i++)
                {
                    finalTextString.Append("Ошибка: " + errors[i].Token + " - " + errors[i].Text + " - " + " position" +
                            " [" + errors[i].Start_pos + "," + errors[i].End_pos + "]" + " line: " + errors[i].Current_line + "\n");
                }
            }
            else if (parser.currentState != States.Number && parser.currentState != States.Right_Bracket)
            {
                return "Не хватает числа или скобки";
            }
            else if (parser.Brackets != 0)
            {
                return "Не хватает закрывающей скобки";
            }
            else
            {
                return "Ошибок нет";
            }
            return finalTextString.ToString();
        }

        List<Error> FindErrors(string text, StringBuilder finalText, ref Parser parser)
        {
            string temp = string.Empty;
            int current_line = 0;
            int start_pos = 0;
            int end_pos = 0;
            Token currentToken = lexer(text[0].ToString());
            Token tempToken;

            List<Error> errors = new List<Error>();
            States tempState = States.None;
            for (int i = 0; i < text.Length; i++)
            {
                tempToken = lexer(text[i].ToString());
                if (currentToken.Type == TokenType.TOKEN_NUMBER && tempToken.Type == TokenType.TOKEN_ERROR)
                {
                    tempToken = currentToken;
                }
                if (tempToken.Type != currentToken.Type || tempToken.Type == TokenType.TOKEN_WHITESPACE
                    || tempToken.Type == TokenType.TOKEN_RIGHT_PARANTHESES || tempToken.Type == TokenType.TOKEN_LEFT_PARANTHESES && temp != string.Empty)
                {
                    currentToken = lexer(temp);
                    end_pos--;

                    tempState = parser.Parse(currentToken.Type);
                    if (tempState == States.ERROR)
                    {
                        errors.Add(new Error(start_pos, end_pos, temp, currentToken.Type, current_line));
                    }

                    if (temp == "\n")
                    {
                        current_line++;
                        start_pos = 0;
                        end_pos = 0;
                        temp = string.Empty;
                        currentToken = tempToken;
                    }
                    else
                    {
                        end_pos++;
                        start_pos = end_pos;
                        temp = string.Empty;
                        currentToken = tempToken;
                    }
                }
                //current_error = false;
                temp += text[i];
                end_pos++;
            }

            currentToken = lexer(temp);
            end_pos--;
            tempState = parser.Parse(currentToken.Type);
            if (tempState == States.ERROR)
            {
                if (tempState == States.ERROR)
                {
                    errors.Add(new Error(start_pos, end_pos, temp, currentToken.Type, current_line));
                }
            }

            return errors;
        }

        Token lexer(string strToLex)
        {
            TokenPosition a = null;
            switch (strToLex)
            {
                case "(": return new Token(TokenType.TOKEN_LEFT_PARANTHESES, "(", a);
                case "+": return new Token(TokenType.TOKEN_PLUS, "+", a);
                case "-": return new Token(TokenType.TOKEN_MINUS, "-", a);
                case "*": return new Token(TokenType.TOKEN_MULTIPLY, "*", a);
                case "/": return new Token(TokenType.TOKEN_DIVIDE, "/", a);
                case ")": return new Token(TokenType.TOKEN_RIGHT_PARANTHESES, ")", a);
                case " ": return new Token(TokenType.TOKEN_WHITESPACE, " ", a);
                case "\r": return new Token(TokenType.TOKEN_WHITESPACE_R, " ", a);
                case "\n": return new Token(TokenType.TOKEN_WHITESPACE_N, " ", a);
                default: break;
            }
            Regex regex = new Regex("[1-9]([0-9])*");
            Match match = regex.Match(strToLex);
            string ident = match.Value;
            if (ident != string.Empty)
            {
                return new Token(TokenType.TOKEN_NUMBER, "number", a);
            }
            return new Token(TokenType.TOKEN_ERROR, "error", a);
        }


        private void Run_Click(object sender, RoutedEventArgs e)
        {
            RichTextBox input = listTabs[tabs.SelectedIndex].InputBox;
            TextRange doc = new TextRange(input.Document.ContentStart, input.Document.ContentEnd);
            string result = lexText(doc.Text);
            ResultBox.Text = result;
            Mather rpn = new Mather(doc.Text);
            if (result == "Ошибок нет") ResultBox.Text += "\n" + rpn.Calc();
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
