using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;

namespace Grammar
{
    

    public class FunctionGrammar 
    {
        AntlrInputStream input = null;
        functionLexer lexer = null;
        CommonTokenStream tokens = null;
        functionParser parser = null;

        FunctionErrorListner error_listner = new FunctionErrorListner();

        public bool IsError {
            get
            {
                parser.function();
                return error_listner.IsError;
            }
            set
            {
                error_listner.IsError = value;
            }
        }

        public string SyntaxErrorMsg
        {
            get
            {
                return error_listner.SyntaxErrorMsg;
            }
            set
            {
                error_listner.SyntaxErrorMsg = value;
            }
        }

        public string LexerErrorMsg
        {
            get
            {
                return error_listner.LexerErrorMsg;
            }
            set
            {
                error_listner.LexerErrorMsg = value;
            }
        }

        string input_str=" ";
        public string Input
        {
            get
            {
                return input_str;
            }
            set
            {
                input_str = value;
                // В качестве входного потока символов устанавливаем полученную строку
                input = new AntlrInputStream(Input);
                // Настраиваем лексер на этот поток
                lexer = new functionLexer(input);
                // Создаем поток токенов на основе лексера
                tokens = new CommonTokenStream(lexer);
                // Создаем парсер
                parser = new functionParser(tokens);
                parser.RemoveErrorListeners(); // remove ConsoleErrorListener
                parser.AddErrorListener(error_listner); // add ours
                lexer.RemoveErrorListeners();
                lexer.AddErrorListener(error_listner);
                IsError = false;
                SyntaxErrorMsg = null;
                LexerErrorMsg = null;
                
            }
        }

        public double Func(double x)
        {
            if (Input == null)
            {
                throw (new ArgumentNullException("input"));
            }

            // И запускаем первое правило грамматики!!!
            if (parser != null)
            {
                parser.Reset();
                parser.Var = x;
                parser.function();
                return parser.Ans;
            }
            return 0;
        }
    }
}
