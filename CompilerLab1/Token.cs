using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerLab1
{

    internal class Token
    {
        public TokenPosition Position { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Token(string type, string value, TokenPosition position)
        {
            this.Type = type;
            this.Value = value;
            this.Position = position;
        }
        public override string ToString()
        {
            var value = this.Value.Replace("\r", "\\r").Replace("\n", "\\n");
            return string.Format("Token: {{ Type: \"{0}\", Value: \"{1}\", Position: {{ StartIndex: \"{2}\", EndIndex: \"{3}\" }} }}", this.Type, value, this.Position.Index, this.Position.Index + this.Value.Length);
        }

    }
}
