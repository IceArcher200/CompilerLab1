using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerLab1
{
    internal class TokenDefinition
    {
		public TokenType Type { get; private set; }
        public Regex Regex { get; private set; }
        public bool Ignore { get; private set; }
        public TokenDefinition(TokenType type, Regex regex)
            : this(type, regex, false){}
        public TokenDefinition(TokenType type, Regex regex, bool ignore)
        {
            this.Type = type;
            this.Regex = regex;
            this.Ignore = ignore;
        }
    }
}
