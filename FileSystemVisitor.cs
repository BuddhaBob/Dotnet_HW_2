using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCourse_HW_2
{
    public class FileSystemVisitor
    {
        string fullPath { get; set; }
        string[] files { get; set; }

        public FileSystemVisitor(string fullpath)
        {
            this.fullPath = fullPath;
        }

        public IEnumerable<string[]> Files(string fullpath)
        {
            if (fullpath.Length > 0)
            {
                files = Directory.GetFiles(fullpath);

                yield return files;

            }

            yield return null;
        }
    }
}
