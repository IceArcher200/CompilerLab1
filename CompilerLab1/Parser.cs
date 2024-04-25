using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerLab1
{
    enum States
    {
        None,
        Left_Bracket,
        Plus,
        Minus,
        Multiply,
        Divide,
        Right_Bracket,
        Number,
        ERROR,
        Whitespace
    };

    internal class Parser
    {
        public States currentState;
        States previousState;
        int brackets = 0;
        public Parser()
        {
            currentState = States.None;
        }
        public States CurrentState { get { return currentState; } set { currentState = value; } }

        public int Brackets { get { return brackets; } set { brackets = value; } }
        public States PreviousState { get { return previousState; } set { previousState = value; } }

        public States MatchToken(TokenType type, States state)
        {
            if (type == TokenType.TOKEN_WHITESPACE || type == TokenType.TOKEN_WHITESPACE_R || type == TokenType.TOKEN_WHITESPACE_N)
            {
                return States.Whitespace;
            }



            return States.ERROR;
        }

        //public States ParseError(TokenType type)
        //{
        //    States temp = previousState;
        //    currentState = previousState;
        //    if (Parse(type) == States.ERROR)
        //    {
        //        currentState = States.ERROR;
        //        previousState = temp;
        //    }
        //    return currentState;
        //}
        public States Parse(TokenType type)
        {


            if (type == TokenType.TOKEN_WHITESPACE || type == TokenType.TOKEN_WHITESPACE_R || type == TokenType.TOKEN_WHITESPACE_N)
            {
                return States.Whitespace;
            }

            //if (currentState != States.ERROR)
            //{
            //    previousState = currentState;
            //}

            if (currentState == States.None && type == TokenType.TOKEN_NUMBER)
            {
                currentState = States.Number;
                return States.Number;
            }

            if (currentState == States.None && type == TokenType.TOKEN_LEFT_PARANTHESES)
            {
                brackets++;
                currentState = States.Left_Bracket;
                return States.Left_Bracket;
            }
            if (currentState == States.Left_Bracket && type == TokenType.TOKEN_LEFT_PARANTHESES)
            {
                brackets++;
                currentState = States.Left_Bracket;
                return States.Left_Bracket;
            }

            if (currentState == States.None && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Number && type == TokenType.TOKEN_PLUS)
            {
                currentState = States.Plus;
                return States.Plus;
            }

            if (currentState == States.Number && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Number && type == TokenType.TOKEN_MULTIPLY)
            {
                currentState = States.Multiply;
                return States.Multiply;
            }

            if (currentState == States.Number && type == TokenType.TOKEN_DIVIDE)
            {
                currentState = States.Divide;
                return States.Divide;
            }

            if (currentState == States.Number && type == TokenType.TOKEN_RIGHT_PARANTHESES)
            {
                if (brackets > 0)
                {
                    brackets--;
                    currentState = States.Right_Bracket;
                    return States.Right_Bracket;
                }
                else
                {
                    currentState = States.ERROR;
                    return States.ERROR;
                }
            }

            if (currentState == States.Left_Bracket && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Left_Bracket && type == TokenType.TOKEN_NUMBER)
            {
                currentState = States.Number;
                return States.Number;
            }

            if (currentState == States.Plus && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Plus && type == TokenType.TOKEN_NUMBER)
            {
                currentState = States.Number;
                return States.Number;
            }

            if (currentState == States.Plus && type == TokenType.TOKEN_LEFT_PARANTHESES)
            {
                brackets++;
                currentState = States.Left_Bracket;
                return States.Left_Bracket;
            }

            if (currentState == States.Minus && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Minus && type == TokenType.TOKEN_NUMBER)
            {
                currentState = States.Number;
                return States.Number;
            }


            if (currentState == States.Minus && type == TokenType.TOKEN_LEFT_PARANTHESES)
            {
                brackets++;
                currentState = States.Left_Bracket;
                return States.Left_Bracket;
            }

            if (currentState == States.Multiply && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Multiply && type == TokenType.TOKEN_NUMBER)
            {
                currentState = States.Number;
                return States.Number;
            }

            if (currentState == States.Multiply && type == TokenType.TOKEN_LEFT_PARANTHESES)
            {
                brackets++;
                currentState = States.Left_Bracket;
                return States.Left_Bracket;
            }

            if (currentState == States.Divide && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Divide && type == TokenType.TOKEN_NUMBER)
            {
                currentState = States.Number;
                return States.Number;
            }

            if (currentState == States.Divide && type == TokenType.TOKEN_LEFT_PARANTHESES)
            {
                brackets++;
                currentState = States.Left_Bracket;
                return States.Left_Bracket;
            }

            if (currentState == States.Right_Bracket && type == TokenType.TOKEN_MINUS)
            {
                currentState = States.Minus;
                return States.Minus;
            }

            if (currentState == States.Right_Bracket && type == TokenType.TOKEN_PLUS)
            {
                currentState = States.Plus;
                return States.Plus;
            }

            if (currentState == States.Right_Bracket && type == TokenType.TOKEN_MULTIPLY)
            {
                currentState = States.Multiply;
                return States.Multiply;
            }

            if (currentState == States.Right_Bracket && type == TokenType.TOKEN_DIVIDE)
            {
                currentState = States.Divide;
                return States.Divide;
            }

            if (currentState == States.Right_Bracket && type == TokenType.TOKEN_RIGHT_PARANTHESES)
            {
                if (brackets > 0)
                {
                    brackets--;
                    currentState = States.Right_Bracket;
                    return States.Right_Bracket;
                }
                else
                {
                    currentState = States.ERROR;
                    return States.ERROR;
                }
            }


            currentState = States.ERROR;
            return States.ERROR;
        }
    }
}