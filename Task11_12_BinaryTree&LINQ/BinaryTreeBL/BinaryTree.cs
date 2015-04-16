using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinaryTreeBL
{
    [Serializable]
    public class BinaryTreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        public BinaryTreeNode<T> Left { get; set; }
        public BinaryTreeNode<T> Right { get; set; }

        public T Value { get; private set; }
        public BinaryTreeNode(T value)
        {
            Value = value;
        }
       
        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
      

    }
    [Serializable]
     public class BinaryTree<T> :IEnumerable<T>, IComparable<T>, ICollection<T> where T : class, IComparable<T>
    {
        private BinaryTreeNode<T> _head;
        private int _count;

        #region  ICollection Interface

        public int Count { get{return _count;} }
        public bool IsReadOnly { get{return false;} }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Adding a new node

        public void Add(T value)
        {
            if (IsReadOnly)
                throw new NotSupportedException("The System.Collections.Generic.ICollection<T> is read-only");//

            var node = new BinaryTreeNode<T>(value);
            if (_head == null)
            {
                _head = node;
                return;
            }
            BinaryTreeNode<T> current = _head, parent = null;
            while (current != null)
            {
                parent = current;
                current = value.CompareTo(current.Value) < 0 ? current.Left : current.Right;
            }
            if (value.CompareTo(parent.Value) < 0)
                parent.Left = node;
            else
                parent.Right = node;

            _count++;
        }

        #endregion
        
        public void Clear() 
        {
            if (IsReadOnly)
                throw new NotSupportedException("The System.Collections.Generic.ICollection<T> is read-only");
            _head = null;
            _count = 0;
        }
       
        public bool Contains(T item)
        {

            foreach (T node in this)
                if (node.CompareTo(item) == 0)
                    return true;

            return false;
            //BinaryTreeNode<T> current = _head;
            //while (current != null)
            //{
            //    int result = current.CompareTo(item);
            //    if (result > 0)
            //       current = current.Left;
            //      else if (result < 0)
            //         current = current.Right;
            //         else break;
            //}
            //return (current != null) ? true: false;
        }
        //
        // Summary:
        //     Copies the elements of the System.Collections.Generic.ICollection<T> to an
        //     System.Array, starting at a particular System.Array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.Generic.ICollection<T>. The System.Array must
        //     have zero-based indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     arrayIndex is less than 0.
        //
        //   System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.ICollection<T>
        //     is greater than the available space from arrayIndex to the end of the destination
        //     array.
        public void CopyTo(T[] array, int arrayIndex)
       {
           if (array == null)
               throw new ArgumentNullException("array is null");
           if (arrayIndex < 0)
               throw new ArgumentOutOfRangeException("arrayIndex is less than 0");
           if (array.Length - arrayIndex < this.Count)
               throw new ArgumentException("The number of elements in the source System.Collections.Generic.ICollection<T> is greater than the available space from arrayIndex to the end of the destination array.");
           
            List<T> tree = new List<T>();
            foreach (T node in this)
	       	     tree.Add(node);
            for (int i = 0; i < tree.Count; i++)
                array[arrayIndex + i] = tree.ElementAt(i);
     
       }
        //
        // Summary:
        //     Removes the first occurrence of a specific object from the System.Collections.Generic.ICollection<T>.
        //
        // Parameters:
        //   item:
        //     The object to remove from the System.Collections.Generic.ICollection<T>.
        //
        // Returns:
        //     true if item was successfully removed from the System.Collections.Generic.ICollection<T>;
        //     otherwise, false. This method also returns false if item is not found in
        //     the original System.Collections.Generic.ICollection<T>.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.Generic.ICollection<T> is read-only.
        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
           
            BinaryTreeNode<T> current = _head;
            parent = null;

            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                    break;
            }

            return current;
        }
        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException("The System.Collections.Generic.ICollection<T> is read-only");

            if (Contains(item) == false) 
                return false;

            BinaryTreeNode<T> parent;
            BinaryTreeNode<T> current = FindWithParent(item, out parent); 
            _count--;

            //First variant
            if (current.Right == null)
                if (parent == null) 
                    _head = current.Left;
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                        parent.Left = current.Left;
                    else if (result < 0)
                        parent.Right = current.Left;
                }
                //Second variant
           else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                    _head = current.Right;
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                        parent.Left = current.Right;
                    else if (result < 0)
                        parent.Right = current.Right;
                }
            }
                //Third varient
             else
            {

                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                leftmostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                    _head = leftmost;
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                        parent.Left = leftmost;
                    else if (result < 0)
                        parent.Right = leftmost;
                }
            }

            return true;
        }
       
            
        #endregion

        #region Symmetrical round

        public IEnumerator<T> InOrderTraversal()
        {
            if (_head == null)
                yield break;

            BinaryTreeNode<T> current = _head;

            Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
            stack.Push(current);
            bool goToLeft = true;

            while (stack.Count > 0)
            {
              if (goToLeft)
                  while (current.Left != null)
                    {
                        stack.Push(current);
                        current = current.Left;
                    }
     
                yield return current.Value;

              if (current.Right != null)
                {
                    current = current.Right;
                    goToLeft = true;
                }
                else
                {
                    current = stack.Pop();
                    goToLeft = false;
                }
            }
        }
       
        #endregion

        #region IEnumerable Interface
        
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }
       
        public int CompareTo(T t)
        {
            return CompareTo(t);
        }
        #endregion

        #region Serialization
        public void Serialize(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            if (!File.Exists(filePath))
                throw new Exception("File isn't exist");

            FileStream stream = File.Create(filePath);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public BinaryTree<T> Deserialize(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            if (!File.Exists(filePath))
                throw new Exception("File isn't exist");

            FileStream stream = File.OpenRead(filePath);
                        BinaryFormatter formatter = new BinaryFormatter();
            BinaryTree<T> st = formatter.Deserialize(stream) as BinaryTree<T>;
            stream.Close();

            return st;
        }
        #endregion

       
        
    }
}
