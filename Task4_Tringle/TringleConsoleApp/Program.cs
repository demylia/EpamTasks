using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figure;

namespace TringleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            Start1();
        }

        private static void Start1()
        {
            Tringle tr1 = new Tringle(new Point(2, 4), new Point(4, 4), new Point(3, 1));
            Tringle tr2 = new Tringle(new Point(2, 4), new Point(5, 5), new Point(3, 3));
            tr1.GetArea();
            tr2.GetArea();
            double a,b;
            Console.WriteLine("Type coordinate of pointAx");
            double.TryParse(Console.ReadLine(), out a);
            Console.WriteLine("Type coordinate of pointAy");
            double.TryParse(Console.ReadLine(), out b);
            //Вопрос по структурам: можем ведь и поумолчанию конструктор вызвать- тогда ошибка будет, как правильнее тут сделать?
            Tringle tr = new Tringle(new Point(a, b), new Point(4, 4), new Point(5, 5));
            var perimetr = tr.GetPerimetr();
            var area = tr.GetArea();
            Console.WriteLine("perimetr {0}, area {1}", perimetr, area);
           
            Start1();
        }
        
    }
}
