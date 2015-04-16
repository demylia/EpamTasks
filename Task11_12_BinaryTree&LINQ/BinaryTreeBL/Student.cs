using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeBL
{
    [Serializable]
   public class Student: IComparable<Student>
    {
       public string Name { get; set; }
       public List<Test> Tests { get; set; }
       public double AverageMark { get; private set; }
       public Student(string name, List<Test> tests)
        {
            
            Name = name;
            Tests = tests;
            if (tests != null)
                AverageMark = tests.Average(m => m.Mark);
        }
      
        
        public int CompareTo(Student st)
        {
            
            if (st == null)
                throw new ArgumentNullException();

            if (st.AverageMark == this.AverageMark)
                return 0;
            if (st.AverageMark > this.AverageMark)
                return 1;

            return -1;

        }
       
    }
}
