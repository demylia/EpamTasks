using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextEditorBL
{
    
    public class TextEditor : IDisposable
    {
        string filePath;
        FileStream fileStream;
        string text;
        bool disposed = false;
        public string Text { get{ return text;} }
        

        public TextEditor(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(Resource.FileNotExists);
            this.filePath = filePath;
            
            text = File.ReadAllText(filePath);
            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        }
       
        public void AddingText(int offset,int addedLenght,string newText)
        {
           
            if (addedLenght == 0  || offset < 0)
                throw new ArgumentException(Resource.IncorrectArg);
           
            byte[] buffer = new byte[fileStream.Length - offset];
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream.Read(buffer, 0, buffer.Length);
            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream.Write(System.Text.Encoding.UTF8.GetBytes(newText), 0, newText.Length);
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Flush();
        }
        public void RemovingText( int offset, int removedLenght)
        {
           if ( removedLenght == 0 || offset < 0)
                throw new ArgumentException(Resource.IncorrectArg);
           
            if ((offset + removedLenght) == fileStream.Length)
                   fileStream.SetLength(offset);
            else
               {
                   byte[] rightText = new byte[fileStream.Length - offset - removedLenght];
                   fileStream.Seek(offset + removedLenght, SeekOrigin.Begin);
                   fileStream.Read(rightText, 0, rightText.Length);
                   fileStream.SetLength(fileStream.Length - removedLenght);
                   fileStream.Seek(offset, SeekOrigin.Begin);
                   fileStream.Write(rightText, 0, rightText.Length);
               }
            fileStream.Flush();
        }

       
       
        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       protected virtual void Dispose(bool disposing)
        {
            if(disposed)
                return;

            if (disposing)
                {
                    filePath = null;
                    text = null;
                }

           fileStream.Close();
           disposed = true;
        }

        ~TextEditor()
        {
           Dispose(false);
        }
    }
}
