using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Panel;

namespace PanelEvaluation
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                System.Console.WriteLine("Usage: PanelEvaluation <GT Annotation Folder> <Auto Annotation Folder>"); return;
            }
            string gt_dir = args[0], auto_dir = args[1];
            if (!System.IO.Directory.Exists(gt_dir))
            {
                System.Console.WriteLine("Can not find " + gt_dir); return;
            }
            if (!System.IO.Directory.Exists(auto_dir))
            {
                System.Console.WriteLine("Can not find " + auto_dir); return;
            }

            Panel.ObjectAnnotation.EvaluateRectObjects(gt_dir, auto_dir);
        }
    }
}
