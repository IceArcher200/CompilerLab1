using System;
using System.Runtime.Serialization;

namespace CompilerLab1
{
    [Serializable]
    internal class UnrecognizedSymbolException : Exception
    {
		public char Symbol { get; private set; }
        public TokenPosition Position { get; set; }
        public UnrecognizedSymbolException(char symbol, TokenPosition position)
            : base(string.Format("Unrecognized symbol '{0}' at index {1} (line {2}, column {3}).", symbol, position.Index, position.Line, position.Column))
        {
            this.Symbol = symbol;
            this.Position = position;
        }
    }
}