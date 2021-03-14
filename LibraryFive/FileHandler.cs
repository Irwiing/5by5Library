using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryFive
{
    class FileHandler
    {
        public String FileName { get; set; }
        public String FilePath { get; private set; }

        public FileHandler() => FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LibraryFive";

        public void CheckFile()
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            if (!File.Exists($@"{FilePath}\{FileName}"))
            {
                using (StreamWriter streamWriter = File.CreateText($@"{FilePath}\{FileName}"))
                {
                    streamWriter.Write("Arquivo Vazio!");
                }
            }
        }
        public void WriteFile(String[] content)
        {
            CheckFile();
            using (StreamWriter streamWriter = File.CreateText($@"{FilePath}\{FileName}"))
            {
                foreach (var line in content)
                {
                    streamWriter.Write(line);
                }
            }
        }
        public String[] ReadFile()
        {
            return File.ReadAllLines($@"{FilePath}\{FileName}");
        }

        public String GetLastLine()
        {
            return File.ReadAllLines($@"{FilePath}\{FileName}").Last();
        }
    }
}
