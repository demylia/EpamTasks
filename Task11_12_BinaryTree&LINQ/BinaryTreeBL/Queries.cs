using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeBL
{
   public static class Queries
    {
       /// <summary>
        /// The method return specified count of students, which have mark bigger than specified.
       /// </summary>
       /// <param name="collection"></param>
       /// <returns></returns>
       public static List<string> BestStudents(this IEnumerable<Student> collection, double average,int countStudents)
       {
           return collection.Where(s=>s.AverageMark > average ).Select(s => s.Name + " "+ s.AverageMark).Take(countStudents).ToList();
       }
       public static List<string> BestStudentsUseExpression(this IEnumerable<Student> collection, double average, int countStudents)
       {
           //First expression
           ParameterExpression st = Expression.Parameter(typeof(Student), "Student");//student
           Expression averageMark = Expression.Property(st, "AverageMark");//student's property is "AverageMark"

           ConstantExpression aver = Expression.Constant(average);//const "average"
           BinaryExpression defference = Expression.GreaterThan(averageMark, aver);//expression body
           LambdaExpression exprForWhere = Expression.Lambda(defference, st);// lambda expression
           var func = (Func<Student, bool>)exprForWhere.Compile();//expression lambda 

           //Second expression
           Expression name = Expression.Property(st, "Name");//student's property
           var concatMethod = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });//method 
           ConstantExpression probel = Expression.Constant(" ");
           BinaryExpression nameProbel = Expression.Add(name, probel, concatMethod);//expression body
           var averageMarkToString = Expression.Convert(averageMark, typeof(string), typeof(Convert).GetMethod("ToString", new[] { typeof(double) }));//convert to string

           BinaryExpression res = Expression.Add(nameProbel, averageMarkToString, concatMethod);//expression body result
           LambdaExpression exprForSelect = Expression.Lambda(res, st);
           var func2 = (Func<Student, string>)exprForSelect.Compile();

           return collection.Where(func).Select(func2).Take(countStudents).ToList();
       }
       /// <summary>
       /// The method return all tests,which  students passed
       /// </summary>
       /// <param name="collection"></param>
       /// <returns></returns>
       public static List<string> AllTests(this IEnumerable<Student> collection)
       {
           return collection.Select(s=> 
                                        {
                                            string res = s.Name + " "+ s.AverageMark +"\n";
                                            foreach (Test item in s.Tests)
	   	                	                    res+= item.Type +" Date:"+ item.Date + " Mark:"+ item.Mark + "\n";
	                                        return res;
                                         }).ToList();
       }
       /// <summary>
       /// The method count average marks all students
       /// </summary>
       /// <param name="collection"></param>
       /// <returns></returns>
       public static double TestsResultOfGroups(this IEnumerable<Student> collection)
       {
           return collection.Aggregate(0, (double acc, Student i) => acc + i.AverageMark);
         
       }
       /// <summary>
       /// The method return one tests,which  students passed
       /// </summary>
       /// <param name="collection"></param>
       /// <param name="test"></param>
       /// <returns></returns>
        public static List<string> TestResult(this IEnumerable<Student> collection,TestType test)
       {
            return collection.Select(s=> 
                                        {
                                            string res = s.Name + " "+ s.AverageMark +"\n";
                                            foreach (Test item in s.Tests)
                                                if (item.Type == test)
                                                    res += item.Type + " Date:" + item.Date + " Mark:" + item.Mark + "\n";
                                           
	                                        return res;
                                         }).ToList();
          
       }
    }
}
