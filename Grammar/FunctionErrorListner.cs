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
    class FunctionErrorListner : BaseErrorListener, IAntlrErrorListener<int>
    {
        public FunctionErrorListner()
        {
            IsError = false;
        }

        public bool IsError { get; set; }

        public string SyntaxErrorMsg
        {
            get;
            set;
        }

        public string LexerErrorMsg { get; set; }

        public override void SyntaxError(TextWriter output, IRecognizer recognizer,
            IToken offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            //base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
            //Console.WriteLine("[{0}]", msg);
            SyntaxErrorMsg = msg;
            IsError = true;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer,
            int offendingSymbol, int line, int charPositionInLine, string msg,
            RecognitionException e)
        {
            //base.SyntaxError(output, recognizer, offendingSymbol, line, charPositionInLine, msg, e);
            //Console.WriteLine("^{0}^", msg);
            LexerErrorMsg = msg;
            IsError = true;
        }
    }
}
