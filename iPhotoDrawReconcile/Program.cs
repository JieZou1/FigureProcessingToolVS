using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace iPhotoDrawReconcile
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Usage: iPhotoDrawReconcile <Folder having image file and corresponding XML file>");
                System.Console.WriteLine("       Display annotation in iPhotoDraw for reviewing and correction");
                return;
            }
            string input_dir = args[0];
            if (!System.IO.Directory.Exists(input_dir))
            {
                System.Console.WriteLine("Can not find " + input_dir);
                return;
            }

            string iphotodraw_path = @"C:\Program Files (x86)\iPhotoDraw\iPhotoDraw.exe";

            string[] files = System.IO.Directory.GetFiles(input_dir);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                if (file.ToLower().EndsWith(".xml")) continue;
                if (file.ToLower().EndsWith(".db")) continue;

                Process process = Process.Start(iphotodraw_path, file);
                process.WaitForExit();
            }
        }
    }
}
