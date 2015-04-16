using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeBL
{
    public enum TestType
    {
        C_Sharp,
        Swift,
        Objective_C,
        JavaScript,
    }
    [Serializable]
    public class Test 
    {
        public TestType Type { get; set; }
        public string Date { get; set; }
        public double Mark{ get; set; }

        public Test( TestType test, DateTime date, double mark)
        {
            if (test == null || date == null)
                throw new ArgumentNullException();
           Type = test;
           Date = date.ToShortDateString();
           Mark = mark;
        }
    }
}
