using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using HtmlAgilityPack;

namespace Panel
{
    internal class RectObject
    {
        public string Name;
        public Rectangle Rect;

        public RectObject(string name, Rectangle rect) { Name = name; Rect = rect; }
    }

    internal abstract class ObjectAnnotation
    {
        protected string figureFilename;
        protected HtmlDocument doc;             //The XML document load from XML file
        protected List<RectObject> rectObjects; //The Rectangle Objects Annotated on the image
        public List<RectObject> RectObjects { get { return rectObjects; } }

        /// <summary>
        /// Load Rectangle Object Annotation from corresponding XML file
        /// </summary>
        /// <param name="xml_file"></param>
        public abstract void LoadRectObjects(string xml_file);

        public void SaveRectObjectIniPhotoDrawFormat(string xml_file)
        {
            iPhotoDrawAnnotation iphotodraw = new iPhotoDrawAnnotation();
            iphotodraw.CreateDocumentTemplate();
            for (int i = 0; i < rectObjects.Count; i++)
            {
                iphotodraw.AddRectObjectAnnotationToDocument(rectObjects[i]);
            }

            //HTMLAgilityPack is designed for HTML, which is case-insensitive, so we have to convert certain tags to its original case.
            string xml_string = iphotodraw.doc.DocumentNode.InnerHtml;
            xml_string = xml_string.Replace(" align=", " Align=");
            xml_string = xml_string.Replace(" righttoleft=", " RightToLeft=");
            xml_string = xml_string.Replace(" left=", " Left=");
            xml_string = xml_string.Replace(" top=", " Top=");
            xml_string = xml_string.Replace(" right=", " Right=");
            xml_string = xml_string.Replace(" bottom=", " Bottom=");
            xml_string = xml_string.Replace(" x=", " X=");
            xml_string = xml_string.Replace(" y=", " Y=");
            xml_string = xml_string.Replace(" width=", " Width=");
            xml_string = xml_string.Replace(" height=", " Height=");
            xml_string = xml_string.Replace(" isroundcorner=", " IsRoundCorner=");
            xml_string = xml_string.Replace(" roundcornerradius=", " RoundCornerRadius=");
            xml_string = xml_string.Replace(" rotation=", " Rotation=");
            xml_string = xml_string.Replace(" b=", " B=");
            xml_string = xml_string.Replace(" g=", " G=");
            xml_string = xml_string.Replace(" r=", " R=");
            xml_string = xml_string.Replace(" alpha=", " Alpha=");
            xml_string = xml_string.Replace(" visible=", " Visible=");
            xml_string = xml_string.Replace(" name=", " Name=");
            xml_string = xml_string.Replace(" isnegative=", " IsNegative=");
            xml_string = xml_string.Replace(" verticalflip=", " VerticalFlip=");
            xml_string = xml_string.Replace(" horizontalflip=", " HorizontalFlip=");
            xml_string = xml_string.Replace(" type=", " Type=");
            xml_string = xml_string.Replace(" style=", " Style=");
            xml_string = xml_string.Replace(" size=", " Size=");
            xml_string = xml_string.Replace(" outlinetype=", " OutlineType=");
            xml_string = xml_string.Replace(" join=", " Join=");
            xml_string = xml_string.Replace(" dash=", " Dash=");
            xml_string = xml_string.Replace(" heightfactor=", " HeightFactor=");
            xml_string = xml_string.Replace(" widthfactor=", " WidthFactor=");
            xml_string = xml_string.Replace(" angle=", " Angle=");
            xml_string = xml_string.Replace(" verticaloffset=", " VerticalOffset=");
            xml_string = xml_string.Replace(" horizontaloffset=", " HorizontalOffset=");
            xml_string = xml_string.Replace(" filltype=", " FillType=");
            xml_string = xml_string.Replace(" imagefilltype=", " ImageFillType=");
            xml_string = xml_string.Replace(" filename=", " FileName=");
            xml_string = xml_string.Replace(" zoomfactor=", " ZoomFactor=");
            xml_string = xml_string.Replace(" wrapmode=", " WrapMode=");
            xml_string = xml_string.Replace(" usetexteffect=", " UseTextEffect=");

            xml_string = xml_string.Replace("<imageoptions ", "<ImageOptions ");
            xml_string = xml_string.Replace("</imageoptions>", "</ImageOptions>");

            xml_string = xml_string.Replace("<canvas>", "<Canvas>");
            xml_string = xml_string.Replace("</canvas>", "</Canvas>");

            xml_string = xml_string.Replace("<box ", "<Box ");
            xml_string = xml_string.Replace("</box>", "</Box>");

            xml_string = xml_string.Replace("<backcolor ", "<BackColor ");
            xml_string = xml_string.Replace("</backcolor>", "</BackColor>");

            xml_string = xml_string.Replace("<flip ", "<Flip ");
            xml_string = xml_string.Replace("</flip>", "</Flip>");

            xml_string = xml_string.Replace("<shapes>", "<Shapes>");
            xml_string = xml_string.Replace("</shapes>", "</Shapes>");
            xml_string = xml_string.Replace("<shape ", "<Shape ");
            xml_string = xml_string.Replace("</shape>", "</Shape>");

            xml_string = xml_string.Replace("<settings>", "<Settings>");
            xml_string = xml_string.Replace("</settings>", "</Settings>");

            xml_string = xml_string.Replace("<font ", "<Font ");
            xml_string = xml_string.Replace("</font>", "</Font>");

            xml_string = xml_string.Replace("<line ", "<Line ");
            xml_string = xml_string.Replace("</line>", "</Line>");

            xml_string = xml_string.Replace("<color ", "<Color ");
            xml_string = xml_string.Replace("</color>", "</Color>");

            xml_string = xml_string.Replace("<startarrowhead ", "<StartArrowHead ");
            xml_string = xml_string.Replace("</startarrowhead>", "</StartArrowHead>");

            xml_string = xml_string.Replace("<endarrowhead ", "<EndArrowHead ");
            xml_string = xml_string.Replace("</endarrowhead>", "</EndArrowHead>");

            xml_string = xml_string.Replace("<layers>", "<Layers>");
            xml_string = xml_string.Replace("</layers>", "</Layers>");
            xml_string = xml_string.Replace("<layer ", "<Layer ");
            xml_string = xml_string.Replace("</layer>", "</Layer>");

            xml_string = xml_string.Replace("<fill ", "<Fill ");
            xml_string = xml_string.Replace("</fill>", "</Fill>");

            xml_string = xml_string.Replace("<fill ", "<Fill ");
            xml_string = xml_string.Replace("</fill>", "</Fill>");

            xml_string = xml_string.Replace("<gradientsettings ", "<GradientSettings ");
            xml_string = xml_string.Replace("</gradientsettings>", "</GradientSettings>");

            xml_string = xml_string.Replace("<startingcolor ", "<StartingColor ");
            xml_string = xml_string.Replace("</startingcolor>", "</StartingColor>");

            xml_string = xml_string.Replace("<endingcolor ", "<EndingColor ");
            xml_string = xml_string.Replace("</endingcolor>", "</EndingColor>");

            xml_string = xml_string.Replace("<blend>", "<Blend>");
            xml_string = xml_string.Replace("</blend>", "</Blend>");

            xml_string = xml_string.Replace("<embeddedimage ", "<EmbeddedImage ");
            xml_string = xml_string.Replace("</embeddedimage>", "</EmbeddedImage>");

            xml_string = xml_string.Replace("<stretchsettings ", "<StretchSettings ");
            xml_string = xml_string.Replace("</stretchsettings>", "</StretchSettings>");

            xml_string = xml_string.Replace("<tilesettings ", "<TileSettings ");
            xml_string = xml_string.Replace("</tilesettings>", "</TileSettings>");

            xml_string = xml_string.Replace("<offset ", "<Offset ");
            xml_string = xml_string.Replace("</offset>", "</Offset>");

            xml_string = xml_string.Replace("<imagedata>", "<ImageData>");
            xml_string = xml_string.Replace("</imagedata>", "</ImageData>");

            xml_string = xml_string.Replace("<endingcolor ", "<EndingColor ");
            xml_string = xml_string.Replace("</endingcolor>", "</EndingColor>");

            xml_string = xml_string.Replace("<texteffect ", "<TextEffect ");
            xml_string = xml_string.Replace("</texteffect>", "</TextEffect>");

            xml_string = xml_string.Replace("<blocktext ", "<BlockText ");
            xml_string = xml_string.Replace("</blocktext>", "</BlockText>");
            xml_string = xml_string.Replace("<text>", "<Text>");
            xml_string = xml_string.Replace("</text>", "</Text>");
            xml_string = xml_string.Replace("<margin ", "<Margin ");
            xml_string = xml_string.Replace("</margin>", "</Margin>");
            xml_string = xml_string.Replace("<data ", "<Data ");
            xml_string = xml_string.Replace("</data>", "</Data>");
            xml_string = xml_string.Replace("<extent ", "<Extent ");
            xml_string = xml_string.Replace("</extent>", "</Extent>");

            System.IO.File.WriteAllText(xml_file, xml_string);
        }

        public static void EvaluateRectObjects(string gt_dir, string auto_dir)
        {
            int gt_count_total = 0, auto_count_total = 0, matched_count_total = 0;

            string[] gt_xml_files = Directory.GetFiles(gt_dir, "*.xml");
            using (StreamWriter sw = new StreamWriter("Evaluation.txt"))
            {
                sw.WriteLine("FigureName\tGT Panels\tAuto Panels\tMatched Panels");
                for (int i = 0; i < gt_xml_files.Length; i++)
                {
                    string gt_xml_file = gt_xml_files[i];
                    string auto_xml_file = Path.Combine(auto_dir, Path.GetFileName(gt_xml_file).Replace("_data.xml", ".xml"));
                    if (!File.Exists(auto_xml_file)) continue;

                    iPhotoDrawAnnotation gt_annotation = new iPhotoDrawAnnotation(); gt_annotation.LoadRectObjects(gt_xml_file);
                    PanelSegmentationAnnotation auto_annotation = new PanelSegmentationAnnotation(); auto_annotation.LoadRectObjects(auto_xml_file);

                    int gt_count, auto_count, matched_count;
                    ObjectAnnotation.EvaluateRectObjects(gt_annotation, auto_annotation, out gt_count, out auto_count, out matched_count);
                    sw.WriteLine("{0}\t{1}\t{2}\t{3}", gt_annotation.figureFilename, gt_count, auto_count, matched_count);

                    gt_count_total += gt_count;
                    auto_count_total += auto_count;
                    matched_count_total += matched_count;
                }

                double recall = (double)matched_count_total / (double)gt_count_total;
                double precision = (double)matched_count_total / (double)auto_count_total;
                double fscore = 2 * precision * recall / (precision + recall);

                sw.WriteLine("Total\t{0}\t{1}\t{2}", gt_count_total, auto_count_total, matched_count_total);
                sw.WriteLine("Precision is {0}", precision);
                sw.WriteLine("Recall is {0}", recall);
                sw.WriteLine("FScore is {0}", fscore);
            }
        }

        private static void EvaluateRectObjects(ObjectAnnotation gt_annotation, ObjectAnnotation auto_annotation, out int gt_count, out int auto_count, out int matched_count)
        {
            RectObject[] gt_rect_objs = gt_annotation.rectObjects.ToArray();
            RectObject[] auto_rect_objs = auto_annotation.rectObjects.ToArray();

            gt_count = 0;  bool[] auto_matched = new bool[auto_rect_objs.Length];

            for (int i = 0; i < gt_rect_objs.Length; i++)
            {
                RectObject gt_rect_obj = gt_rect_objs[i];
                if (!gt_rect_obj.Name.ToLower().Trim().StartsWith("panel")) continue; //We are not evaluating labels yet.

                gt_count++;

                //Search auto annotation to find matches
                for (int j = 0; j < auto_rect_objs.Length; j++)
                {
                    RectObject auto_rect_obj = auto_rect_objs[j];

                    {//Criteria 1: Rectangle overlapping is larger than 75%
                        Rectangle overlapping_rect = Rectangle.Intersect(gt_rect_obj.Rect, auto_rect_obj.Rect);
                        double overlapping_area = overlapping_rect.Width * overlapping_rect.Height;
                        double gt_area = gt_rect_obj.Rect.Width * gt_rect_obj.Rect.Height;
                        if (overlapping_area / gt_area < 0.75) continue;
                    }

                    {//Criteria 2: Overlapping to adjacent panle of the matching reference panel is less than 5%
                        int k; for (k = 0; k < gt_rect_objs.Length; k++)
                        {
                            if (k == i) continue;

                            RectObject gt_rect_obj1 = gt_rect_objs[k];
                            if (!gt_rect_obj1.Name.ToLower().Trim().StartsWith("panel")) continue; //We are not evaluating labels yet.

                            Rectangle overlapping_rect = Rectangle.Intersect(gt_rect_obj1.Rect, auto_rect_obj.Rect);
                            double overlapping_area = overlapping_rect.Width * overlapping_rect.Height;
                            double gt_area = gt_rect_obj1.Rect.Width * gt_rect_obj1.Rect.Height;
                            if (overlapping_area / gt_area > 0.05) break;
                        }
                        if (k != gt_rect_objs.Length) continue; //This means auto_rect_obj overlaps with an adjacent gt_rect_obj and larger than 5%.
                    }

                    auto_matched[j] = true; break;
                }
            }

            //Compute Precision and Recall
            auto_count = auto_rect_objs.Length;
            matched_count = 0; for (int i = 0; i < auto_matched.Length; i++) if (auto_matched[i]) matched_count++;
        }

        /// <summary>
        /// Analyze evaluation_file to find errors and then copy the error images from auto_result_folder to auto_error_folder
        /// </summary>
        /// <param name="evaluation_file"></param>
        /// <param name="auto_result_folder"></param>
        /// <param name="auto_error_folder"></param>
        public static void ListErrors(string evaluation_file, string auto_result_folder, string auto_error_folder)
        {
            List<string> errors = new List<string>();
            using (StreamReader sr = new StreamReader(evaluation_file))
            {
                string line = sr.ReadLine(); string[] words;
                while ((line = sr.ReadLine())!= null)
                {
                    if (line.StartsWith("Total")) break;
                    words = line.Split();
                    int gt_panels = int.Parse(words[1]);
                    int matched_panels = int.Parse(words[3]);

                    if (gt_panels != matched_panels) errors.Add(words[0]);
                }
            }

            for (int i = 0; i < errors.Count; i++)
            {
                string src_file = Path.Combine(auto_result_folder, errors[i] + ".jpg");
                string dst_file = Path.Combine(auto_error_folder, errors[i] + ".jpg");
                File.Copy(src_file, dst_file);
            }
        }

        public static void CorrectComparison(string evaluation_file1, string evaluation_file2)
        {
            List<string> corrects1 = new List<string>();
            using (StreamReader sr = new StreamReader(evaluation_file1))
            {
                string line = sr.ReadLine(); string[] words;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("Total")) break;
                    words = line.Split();
                    int gt_panels = int.Parse(words[1]);
                    int matched_panels = int.Parse(words[3]);

                    if (gt_panels == matched_panels) corrects1.Add(words[0]);
                }
            }
            List<string> corrects2 = new List<string>();
            using (StreamReader sr = new StreamReader(evaluation_file2))
            {
                string line = sr.ReadLine(); string[] words;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("Total")) break;
                    words = line.Split();
                    int gt_panels = int.Parse(words[1]);
                    int matched_panels = int.Parse(words[3]);

                    if (gt_panels == matched_panels) corrects2.Add(words[0]);
                }
            }

            //Either correct and both correct
            List<string> either_corrects = new List<string>(); either_corrects.AddRange(corrects1);
            List<string> both_corrects = new List<string>();
            for (int i = 0; i < corrects2.Count; i++)
            {
                string correct = corrects2[i];
                if (corrects1.IndexOf(correct) != -1) both_corrects.Add(correct);
                else either_corrects.Add(correct);
            }

            string[] either_arr = either_corrects.ToArray(); Array.Sort(either_arr);
            string[] both_arr = both_corrects.ToArray(); Array.Sort(both_arr);
            using (StreamWriter sw = new StreamWriter("EitherCorrects.txt"))
            {
                for (int i = 0; i < either_arr.Length; i++) sw.WriteLine(either_arr[i]);
            }
            using (StreamWriter sw = new StreamWriter("BothCorrects.txt"))
            {
                for (int i = 0; i < both_arr.Length; i++) sw.WriteLine(both_arr[i]);
            }

        }
    }
}