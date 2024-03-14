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
        States currentState;
        public Parser()
        {
            currentState = States.START;
        }

        public States Parse(Token token)
        {
            string type = token.Type;
            if (type == "SEPARATOR")
            {
                return currentState;
            }

            if (currentState == States.START && type == "KEYWORD")
            {
                currentState = States.LET;
                return States.LET;
            }

            if (currentState == States.LET && type == "IDENTIFIER")
            {
                currentState = States.ARR;
                return States.ARR;

            }

            if (currentState == States.ARR && type == "ASSIGNMENT")
            {
                currentState = States.ASSIGNMENT;
                return States.ASSIGNMENT;
            }

            if (currentState == States.ASSIGNMENT && type == "OPEN_BRACE")
            {
                currentState = States.OPEN;
                return States.OPEN;
            }

            if (currentState == States.OPEN && type == "LINE")
            {
                currentState = States.STRING;
                return States.STRING;
            }

            if (currentState == States.STRING && type == "COLON")
            {
                currentState = States.COLON;
                return States.COLON;
            }

            if (currentState == States.COLON && (type == "COMPLEX_NUMBER" || type == "NEGATIVE_NUMBER" || type == "NUMBER" || type == "LINE"))
            {
                currentState = States.STRING2;
                return States.STRING2;
            }

            if (currentState == States.STRING2 && type == "COMMA")
            {
                currentState = States.OPEN;
                return States.OPEN;
            }


            if (currentState == States.STRING2 && type == "CLOSE_BRACE")
            {
                currentState = States.END;
                return States.END;
            }

            if (currentState == States.END && type == "END_MASSIVE")
            {
                currentState = States.START;
                return States.START;
            }
            currentState = States.ERROR;
            return States.ERROR;
        }
    }
}
