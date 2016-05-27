using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Panel;

namespace LabelMe2IPhotoDraw
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                System.Console.WriteLine("Usage: LabelMe2IPhotoDraw <Input LabelMe Annotation Folder> <Output iPhotoDraw Annotation Folder>");
                return;
            }
            string input_dir = args[0], output_dir = args[1];
            if (!System.IO.Directory.Exists(input_dir))
            {
                System.Console.WriteLine("Can not find " + input_dir);
                return;
            }

            if (!System.IO.Directory.Exists(output_dir))
                System.IO.Directory.CreateDirectory(output_dir);

            LabelMeAnnotation label_me = new LabelMeAnnotation();
            string[] files = System.IO.Directory.GetFiles(input_dir, "*.xml");
            for (int i = 0; i < files.Length; i++)
            {
                string input_file = files[i];
                label_me.LoadRectObjects(input_file);
                string output_file = input_file.Replace(input_dir, output_dir);
                output_file = output_file.Replace(".xml", "_data.xml");
                label_me.SaveRectObjectIniPhotoDrawFormat(output_file);
            }

        }
    }
}
