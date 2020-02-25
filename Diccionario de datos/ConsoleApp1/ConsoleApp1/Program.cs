using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream(Environment.CurrentDirectory + @"\copy.s", FileMode.Open);

            AntlrInputStream input = new AntlrInputStream(fs);

            Combined1Lexer lex = new Combined1Lexer(input);

            CommonTokenStream tokens = new CommonTokenStream(lex);
            Combined1Parser par = new Combined1Parser(tokens);

            par.compileUnit();





            
            Console.ReadLine();
        }
    }
}
