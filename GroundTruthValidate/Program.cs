using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panel;

using Emgu.CV;
using Emgu.CV.Structure;

namespace GroundTruthValidate
{
    class Program
    {
        static void CheckMissingAnnotationFile(string image_folder)
        {
            List<string> missing_gt_files = new List<string>();
            string[] image_files = System.IO.Directory.GetFiles(image_folder, "*.jpg");
            for (int i = 0; i < image_files.Length; i++)
            {
                string image_file = image_files[i];
                string xml_file = image_file.Replace(@"\Image\", @"\GT\").Replace(".jpg", "_data.xml");

                if (!System.IO.File.Exists(xml_file))
                {
                    missing_gt_files.Add(System.IO.Path.GetFileNameWithoutExtension(image_file));
                }
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("missing_gt.txt"))
            {
                for (int i = 0; i < missing_gt_files.Count; i++)
                {
                    sw.WriteLine(missing_gt_files[i]);
                }
            }
        }

        static void SuperimposeGT(string image_folder)
        {
            string[] image_files = System.IO.Directory.GetFiles(image_folder, "*.jpg");
            for (int i = 0; i < image_files.Length; i++)
            {
                string image_file = image_files[i];
                string xml_file = image_file.Replace(@"\Image\", @"\GT\").Replace(".jpg", "_data.xml");

                if (System.IO.File.Exists(xml_file))
                {
                    iPhotoDrawAnnotation annotation = new iPhotoDrawAnnotation();
                    annotation.LoadRectObjects(xml_file);

                    Mat image = new Emgu.CV.Mat(image_file, Emgu.CV.CvEnum.LoadImageType.Color);
                    for (int j = 0; j < annotation.RectObjects.Count; j++)
                    {
                        RectObject rect_object = annotation.RectObjects[j];
                        MCvScalar scalar = rect_object.Name.StartsWith("panel") ? new MCvScalar(0, 0, 255): new MCvScalar(255, 0, 0);
                        CvInvoke.Rectangle(image, rect_object.Rect, scalar);
                    }
                    CvInvoke.Imwrite(System.IO.Path.GetFileName(image_file), image);
                }
            }
        }

        static void LabelStatistics(string gt_folder)
        {
            string[] gt_files = System.IO.Directory.GetFiles(gt_folder, "*_data.xml");
            List<int> widths = new List<int>(), heights = new List<int>();
            for (int i = 0; i < gt_files.Length; i++)
            {
                string xml_file = gt_files[i];

                iPhotoDrawAnnotation annotation = new iPhotoDrawAnnotation();
                annotation.LoadRectObjects(xml_file);

                for (int j = 0; j < annotation.RectObjects.Count; j++)
                {
                    RectObject rect_object = annotation.RectObjects[j];
                    string name = rect_object.Name;
                    if (!name.StartsWith("label")) continue;
                    if (name.Length > "label a".Length) continue;

                    widths.Add(rect_object.Rect.Width);
                    heights.Add(rect_object.Rect.Height);
                }
            }

            double mean_width = 0, mean_height = 0, mean_aspect_ratio = 0;
            int max_width = int.MinValue, max_height = int.MinValue, min_width = int.MaxValue, min_height = int.MaxValue; double min_aspect_ration = double.MaxValue, max_aspect_ratio = double.MinValue;
            for (int i = 0; i < widths.Count; i++)
            {
                int width = widths[i], height = heights[i];
                double aspect_ratio = (double)width / (double)height;

                mean_width += width; mean_height += height;
                mean_aspect_ratio += aspect_ratio;

                if (width > max_width) max_width = width;
                if (width < min_width) min_width = width;
                if (height > max_height) max_height = height;
                if (height < min_height) min_height = height;
                if (aspect_ratio > max_aspect_ratio) max_aspect_ratio = aspect_ratio;
                if (aspect_ratio < min_aspect_ration) min_aspect_ration = aspect_ratio;
            }
            mean_width /= widths.Count;
            mean_height /= heights.Count;
            mean_aspect_ratio /= widths.Count;

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("LabelStatistics.txt"))
            {
                sw.WriteLine("Min Width {0}\tMax Width {1}\tMean Width {2}", min_width, max_width, mean_width);
                sw.WriteLine("Min Height {0}\tMax Height {1}\tMean Height {2}", min_height, max_height, mean_height);
                sw.WriteLine("Min Aspect Ratio {0}\tMax Aspect Ratio {1}\tMean Aspect Ratio {2}", min_aspect_ration, max_aspect_ratio, mean_aspect_ratio);

                for (int i = 0; i < widths.Count; i++)
                    sw.WriteLine("{0}\t{1}", widths[i], heights[i]);
            }
        }

        static void Main(string[] args)
        {
            string image_folder = @"\Users\jie\Openi\Panel\data\Train\Image";
            string gt_folder = @"\Users\jie\Openi\Panel\data\Train\GT";

            //CheckMissingAnnotationFile(image_folder);
            //SuperimposeGT(image_folder);
            LabelStatistics(gt_folder);
        }
    }
}
