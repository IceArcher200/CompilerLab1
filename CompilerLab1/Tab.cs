using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CompilerLab1
{
    class Tab
    {
        public RichTextBox InputBox{get; set;}
        public string FilePath { get; set; }
        public bool TextChanged { get; set; }
        public Tab(RichTextBox richTextBox,string file) 
        {
            InputBox = richTextBox;
            FilePath = file;
            TextChanged = false;
        }
    }
}
