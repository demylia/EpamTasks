using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeBL;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create students
            List<Test> list1 = new List<Test>()
                  {
                    new Test(TestType.C_Sharp, DateTime.Now.Date, 1), 
                    new Test(TestType.JavaScript, DateTime.Now.Date, 1),
                    new Test(TestType.Swift, DateTime.Now.Date, 1)
                  };
            Student st1 = new Student("Vasya", list1);
            List<Test> list2 = new List<Test>()
                  {
                    new Test(TestType.C_Sharp, DateTime.Now.Date, 4), 
                    new Test(TestType.JavaScript, DateTime.Now.Date, 4),
                    new Test(TestType.Swift, DateTime.Now.Date, 4)
                  };
            Student st2 = new Student("Kolya", list2);
            List<Test> list3 = new List<Test>()
                  {
                    new Test(TestType.C_Sharp, DateTime.Now.Date, 5), 
                    new Test(TestType.JavaScript, DateTime.Now.Date,5),
                    new Test(TestType.Swift, DateTime.Now.Date, 5)
                  };
            Student st3 = new Student("Petya", list3);
            List<Test> list4 = new List<Test>()
                  {
                    new Test(TestType.C_Sharp, DateTime.Now.Date, 3), 
                    new Test(TestType.JavaScript, DateTime.Now.Date, 3),
                    new Test(TestType.Swift, DateTime.Now.Date, 3)
                  };
            Student st4 = new Student("Serg", list4);
            List<Test> list5 = new List<Test>()
                  {
                    new Test(TestType.C_Sharp, DateTime.Now.Date, 7), 
                    new Test(TestType.JavaScript, DateTime.Now.Date, 6),
                   // new Test(TestType.Swift, DateTime.Now.Date, 6)
                  };
            Student st5 = new Student("Jack", list5);
            List<Test> list6 = new List<Test>()
                  {
                    new Test(TestType.C_Sharp, DateTime.Now.Date,7), 
                    new Test(TestType.JavaScript, DateTime.Now.Date,7),
                    new Test(TestType.Swift, DateTime.Now.Date, 7)
                  };
            Student st6 = new Student("Petya", list6);

            // Create nodes and tree
            BinaryTreeNode<Student> node1 = new BinaryTreeNode<Student>(st1);
            BinaryTreeNode<Student> node2 = new BinaryTreeNode<Student>(st2);
            BinaryTreeNode<Student> node3 = new BinaryTreeNode<Student>(st3);
            BinaryTree<Student> tree = new BinaryTree<Student>();

            tree.Add(st1);
            tree.Add(st2);
            tree.Add(st3);
            tree.Add(st4);
            tree.Add(st5);
            tree.Add(st6);

            //WriteLine all students
            foreach (Student node in tree)
                Console.WriteLine(node.Name + node.AverageMark);
            Console.WriteLine();

           // tree.Remove(st2);
            //tree.Remove(st3);
            foreach (Student node in tree)
                Console.WriteLine(node.Name + node.AverageMark);
            Console.WriteLine();

            //Serialize Write/Read a student to/from binary file
            string filePath = "BinaryTree.dat";

            var st = tree.Deserialize(filePath);
            Console.WriteLine("*********");
            foreach (Student node in st)
                Console.WriteLine(node.Name + node.AverageMark);
            Console.WriteLine();
            
           
            ////LINQ
            foreach (var node in tree.BestStudentsUseExpression(4, 4))
                Console.WriteLine(node);
            Console.WriteLine();
            foreach (var node in tree.BestStudents(5, 4))
                Console.WriteLine(node);
            Console.WriteLine();
            foreach (var node in tree.AllTests())
                Console.WriteLine(node);
            Console.WriteLine();
            foreach (var node in tree.TestResult(TestType.Swift))
                Console.WriteLine(node);
            Console.WriteLine();

            Console.WriteLine(tree.TestsResultOfGroups());
            Console.ReadKey();
        }
    }
}
