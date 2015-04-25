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
        private string filePath;
        private FileStream fileStream;
        private string text;
        public string Text { get { return text; } }
        private bool disposed = false;
        

        /// <summary>
        /// Constructor. Accept path ti file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        public TextEditor(string filePath)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException(Resource.FileNotExists);
            this.filePath = filePath;
            text = File.ReadAllText(filePath);
            fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        }

        /// <summary>
        /// Change text in memory by info about changed
        /// </summary>
        /// <param name="addedLenght">Lenght added text</param>
        /// <param name="offset">Offset</param>
        /// <param name="removedLenght">enght remove text</param>
        /// <param name="text">Text if added, empty string if remove</param>
        public void AddingText(int addedLenght, int offset, string text)
        {
            if (text == null)
                throw new ArgumentNullException(Resource.NullString);
            if (addedLenght != 0  || offset < 0)
                throw new ArgumentException(Resource.IncorrectArg);

            if(offset == 0 && addedLenght > 0)
            {
                byte[] allText = new byte[fileStream.Length];
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.Read(allText, 0, (int)fileStream.Length);
                fileStream.Seek(0, SeekOrigin.Begin);
                fileStream.Write(System.Text.Encoding.UTF8.GetBytes(text), 0, text.Length);
                fileStream.Write(allText, 0, allText.Length);
            }
           
            else if(addedLenght > 0)
            {
                byte[] rightText = new byte[fileStream.Length - offset];
                fileStream.Seek(offset, SeekOrigin.Begin);
                fileStream.Read(rightText, 0, rightText.Length);
                fileStream.Seek(offset, SeekOrigin.Begin);
                fileStream.Write(System.Text.Encoding.UTF8.GetBytes(text), 0, text.Length);
                fileStream.Write(rightText, 0, rightText.Length);
            }
           
            fileStream.Flush();
        }
        public void RemovingText(int removedLenght, int offset)
        {
           if ( removedLenght != 0 || offset < 0)
                throw new ArgumentException(Resource.IncorrectArg);

           
               if ((offset + removedLenght) == fileStream.Length)
                   fileStream.SetLength(offset);

               else if (offset == 0 && removedLenght > 0)
               {
                   byte[] lastText = new byte[fileStream.Length - removedLenght];
                   fileStream.Seek(removedLenght, SeekOrigin.Begin);
                   fileStream.Read(lastText, 0, lastText.Length);
                   fileStream.SetLength(fileStream.Length - removedLenght);
                   fileStream.Seek(0, SeekOrigin.Begin);
                   fileStream.Write(lastText, 0, lastText.Length);
               }
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

       
        /// <summary>
        /// Close and realise memory stream
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// Realise interface IDisposable
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Realise interface IDisposable
        /// </summary>
        /// <param name="disposing">True if need realise memory stream</param>
        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if (disposing)
                {
                    filePath = null;
                    disposed = true;
                }

                fileStream.Close();
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~TextEditor()
        {
            if (!this.disposed)
                Dispose(false);
        }
    }
}
