using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Panel;

namespace PanelSeg2IPhotoDraw
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                System.Console.WriteLine("Usage: PanelSeg2IPhotoDraw <Input PanelSeg Annotation Folder> <Output iPhotoDraw Annotation Folder>");
                System.Console.WriteLine("       Convert PanelSeg formated (Automated Panel Segmentation Result) XML file to iPhotoDraw formated XML file");
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

            string[] files = System.IO.Directory.GetFiles(input_dir, "*.xml");
            for (int i = 0; i < files.Length; i++)
            {
                string input_file = files[i];
                string output_file = input_file.Replace(input_dir, output_dir);
                output_file = output_file.Replace(".xml", "_data.xml");

                PanelSegAnnotation panel_seg = new PanelSegAnnotation();
                panel_seg.LoadRectObjects(input_file);
                panel_seg.SaveRectObjectIniPhotoDrawFormat(output_file);
            }

        }
    }
}
