using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerLab1
{
    enum States
    {
        beginStmt,
        stmtList,
        stmt,
        assgStmt,
        arithExpr,
        ERROR

    };
    internal class Parser
    {
        //public States currentState;
        public Parser()
        {
        }
        int i = 0;
        List<Token> tokens = new List<Token>();
        string result;
        public string Parse(List<Token> token)
        {
            tokens = token;
            result = "";
            string type = tokens[i].Type;
            if (type == "SEPARATOR")
            {
                i++;
            }
            else
            { 
                beginStmt(tokens[i]);
            }
            return result;
        }
        public void beginStmt(Token token)
        {
            if (i < tokens.Count)
            {
                if (token.Type == "KEYWORD")
                {
                    result += token.ToString() + "\n";
                    i++;
                    if (i < tokens.Count)
                        stmtList(tokens[i]);
                    if (i < tokens.Count)
                        result += tokens[i] + "\n";
                }
            }
        }
        public void stmtList(Token token)
        {
            if ((i+1) < tokens.Count ) 
            {
                stmt(token);
                if ((i + 1) < tokens.Count)
                {
                    if (tokens[i + 1].Type == "SEMICOLON")
                    {
                        i++;
                        result += tokens[i] + "\n";
                        i++;
                        stmtList(tokens[i]);
                    }
                }
            }
        }

        public void stmt(Token token)
        {
            if (i < tokens.Count)
            {
                if (token.Type == "KEYWORD")
                    beginStmt(token);
                else if (token.Type == "VAR")
                {
                    assgStmt(tokens[i]);
                }
                else result += "ERROR\n";
            }
        }
        public void assgStmt(Token token)
        {
            if (i < tokens.Count)
            {
                result += tokens[i].ToString() + "\n";
                i++;
                result += tokens[i].ToString() + "\n";
                i++;
                if (i < tokens.Count)
                    arithExpr(tokens[i]);
            }
        }
        public void arithExpr(Token token)
        {
            

                if (token.Type == "OPEN_BRACE")
                {
                    result += tokens[i].ToString() + "\n";
                    i++;
                    if (i < tokens.Count)
                        arithExpr(tokens[i]);
                    i++;
                    if (i < tokens.Count)
                        result += tokens[i].ToString() + "\n";
                    i++;
                    return;
                }
                if ((i + 1) < tokens.Count)
                {
                    if (tokens[i + 1].Type == "PLUS" || tokens[i + 1].Type == "MULTY")
                    {
                        i++;
                        arithExpr(token);
                        result += tokens[i].ToString() + "\n";
                        i++;
                        arithExpr(tokens[i]);
                        return;
                    }
                }
                result += token.ToString() + "\n";
        }
    }
}
