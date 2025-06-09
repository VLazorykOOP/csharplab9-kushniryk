using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    class ExpressionToken : ICloneable
    {
        public string Value { get; }

        public ExpressionToken(string value)
        {
            Value = value;
        }

        public object Clone()
        {
            return new ExpressionToken(this.Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/" || token == "^";
        }
    }

    class PostfixToPrefixConverter : IEnumerable
    {
        private ArrayList tokens;

        public PostfixToPrefixConverter(string postfix)
        {
            tokens = new ArrayList();
            foreach (string token in postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                tokens.Add(new ExpressionToken(token));
            }
        }

        public string Convert()
        {
            Stack stack = new Stack();
            foreach (ExpressionToken token in tokens)
            {
                if (ExpressionToken.IsOperator(token.Value))
                {
                    if (stack.Count < 2) throw new InvalidOperationException("Невірний вираз.");
                    string op2 = stack.Pop().ToString();
                    string op1 = stack.Pop().ToString();
                    stack.Push($"{token.Value} {op1} {op2}");
                }
                else
                {
                    stack.Push(token.Value);
                }
            }

            if (stack.Count != 1)
                throw new InvalidOperationException("Помилка структури виразу.");
            return stack.Pop().ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return tokens.GetEnumerator();
        }
    }
}
