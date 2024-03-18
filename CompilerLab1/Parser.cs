using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerLab1
{
    enum States
    {
        START,
        LET,
        ARR,
        ASSIGNMENT,
        OPEN,
        STRING,
        COLON,
        STRING2,
        END,
        ERROR

    };
    internal class Parser
    {
        public States currentState;
        public Parser()
        {
            currentState = States.START;
        }

        public string getToken(States state)
        {
            if (state == States.START)
                return "let";
            if (state == States.LET)
                return "arr";
            if (state == States.ARR)
                return "=";
            if (state == States.ASSIGNMENT)
                return "{";
            if (state == States.OPEN)
                return "key";
            if (state == States.STRING)
                return ":";
            if (state == States.COLON)
                return "value";
            if (state == States.STRING2)
                return "}";
            if (state == States.END)
                return ";";
            return "";
        }

        public States Parse(Token token, bool isStateChangeable)
        {
            string type = token.Type;
            if (type == "SEPARATOR")
            {
                return currentState;
            }

            if (currentState == States.START && type == "KEYWORD")
            {
                if (isStateChangeable)
                    currentState = States.LET;
                return States.LET;
            }

            if (currentState == States.LET && type == "IDENTIFIER")
            {
                if (isStateChangeable)
                    currentState = States.ARR;
                return States.ARR;

            }

            if (currentState == States.ARR && type == "ASSIGNMENT")
            {
                if (isStateChangeable)
                    currentState = States.ASSIGNMENT;
                return States.ASSIGNMENT;
            }

            if (currentState == States.ASSIGNMENT && type == "OPEN_BRACE")
            {
                if (isStateChangeable)
                    currentState = States.OPEN;
                return States.OPEN;
            }

            if (currentState == States.OPEN && type == "LINE")
            {
                if (isStateChangeable)
                    currentState = States.STRING;
                return States.STRING;
            }

            if (currentState == States.STRING && type == "COLON")
            {
                if (isStateChangeable)
                    currentState = States.COLON;
                return States.COLON;
            }

            if (currentState == States.COLON && (type == "COMPLEX_NUMBER" || type == "NEGATIVE_NUMBER" || type == "NUMBER" || type == "LINE"))
            {
                if (isStateChangeable)
                    currentState = States.STRING2;
                return States.STRING2;
            }

            if (currentState == States.STRING2 && type == "COMMA")
            {
                if (isStateChangeable)
                    currentState = States.OPEN;
                return States.OPEN;
            }

            if (currentState == States.STRING2 && type == "CLOSE_BRACE")
            {
                if (isStateChangeable)
                    currentState = States.END;
                    return States.END;
            }

            if (currentState == States.END && type == "END_MASSIVE")
            {
                    if (isStateChangeable)
                        currentState = States.START;
                return States.START;
            }
            return States.ERROR;
        }
    }
}
