using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompilerLab1
{
    internal class Lexer
    {
        private Regex _endOfLineRegex = new Regex(@"\r?\n", RegexOptions.Compiled);
        private List<TokenDefinition> _tokenDefinitions = new List<TokenDefinition>();

        /// <summary>
        /// Adds new definition to search for.
        /// </summary>
        /// <param name="tokenDefinition"></param>
        public void AddDefinition(TokenDefinition tokenDefinition)
        {
            _tokenDefinitions.Add(tokenDefinition);
        }

        /// <summary>
        /// Creates list of tokens from the given string, based on the
        /// added definitions.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<Token> Tokenize(string source)
        {
            var result = new List<Token>();

            var currentIndex = 0;
            var currentLine = 1;
            var currentColumn = 0;
            string? s = string.Empty;

            while (currentIndex < source.Length)
            {
                var definition = default(TokenDefinition);
                var length = 0;

                foreach (var rule in _tokenDefinitions)
                {
                    var match = rule.Regex.Match(source, currentIndex);

                    if (match.Success && (match.Index - currentIndex) == 0)
                    {
                        definition = rule;
                        length = match.Length;
                        break;
                    }
                }


                var value = source.Substring(currentIndex, length);

                if (definition == null)
                {
                    s = "";
                    s += source[currentIndex];
                    length = 1;
                    result.Add(new Token(TokenType.TOKEN_ERROR, s, new TokenPosition(currentIndex, currentIndex + (length - 1), currentColumn)));
                }
                else
                {
                    if (!definition.Ignore)
                        result.Add(new Token(definition.Type, value, new TokenPosition(currentIndex, currentIndex + (length - 1), currentColumn)));
                }
                var eolMatch = _endOfLineRegex.Match(value);
                if (eolMatch.Success)
                {
                    currentLine++;
                    currentColumn = value.Length - (eolMatch.Index + eolMatch.Length);
                }
                else
                {
                    currentColumn += length;
                }

                currentIndex += length;

            }
            return result;
        }
    }
}
