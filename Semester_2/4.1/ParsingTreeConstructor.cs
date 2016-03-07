using System;

namespace ParsingTree
{
    /// Has one public static method that builds a Parsing Tree by sentence
    /// and returns its head.
    public class ParsingTreeConstructor
    {
        public static Operand ConstructParsingTree(String sentence)
        {
            bool ifDecimal = ifDecimalCheck(sentence);
            if (ifDecimal)
            {
                return new Operand(ConvertToInt(ref sentence));
            }
            else
            {
                return WorkRealSentence(sentence);
            }
        }

        private static bool ifDecimalCheck(String sentence)
        {
            bool ifDecimal = true;
            for (int i = 0; i < sentence.Length; ++i)
            {
                ifDecimal &= sentence[i] >= '0' && sentence[i] <= '9';
            }
            return ifDecimal;
        }

        private static int ConvertToInt(ref String sentence)
        {      
            int number = 0;      
            while (sentence.Length != 0 && sentence[0] >= '0' && sentence[0] <= '9')
            {
                number *= 10;
                number += Convert.ToInt32(sentence[0]) - Convert.ToInt32('0');
                sentence = sentence.Remove(0, 1);
            }
            return number;
        }

        private static Operand WorkRealSentence(String sentence)
        {
            int number = 0;
            ParsingStack stack = new ParsingStack();
            CheckWentWrongThrowingException(sentence[1]);
            Operation result = new Operation(sentence[1]);
            stack.Push(result);
            sentence = sentence.Remove(0, 3);
            while (sentence.Length != 0)
            {
                try
                {
                    while (stack.Top().GetCount() == 2)
                    {
                        stack.Pop();
                    }
                }
                catch(StackNullExeption)
                {
                    return result;
                }
                switch (sentence[0])
                {
                    case '(':
                    {
                        CheckWentWrongThrowingException(sentence[1]);
                        Operation pushed = new Operation(sentence[1]);
                        AddChildToStackTop(stack.Top(), pushed);
                        stack.Push(pushed);
                        sentence = sentence.Remove(0, 2);
                        break;
                    }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    {
                        number = ConvertToInt(ref sentence);
                        Operand added = new Operand(number);
                        number = 0;
                        AddChildToStackTop(stack.Top(), added);
                        break;
                    }
                    case ')':
                    case ' ':
                    {
                        sentence = sentence.Remove(0, 1);
                        break;
                    }
                }
            }
            throw new ConstructorFailException("Didn't manage...");
        }

        private static void CheckWentWrongThrowingException(char simb)
        {
            if (CheckWentWrong(simb))
            {
                throw new WrongInputException("Wrong Input!");
            }
        }

        private static void AddChildToStackTop(ParsingStackElement top, Operand added)
        {
            if (top.GetCount() == 0)
            {
                top.GetValue().SetLeft(added);
            }
            else
            {
                top.GetValue().SetRight(added);
            }
            top.IncreaseCount();
        }

        private static bool CheckWentWrong(char simb)
        {
            return simb != '+' &&
                simb != '-' &&
                simb != '*' &&
                simb != '/';
        }
    }
}