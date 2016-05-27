using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using HtmlAgilityPack;

namespace Panel
{
    internal class LabelMeAnnotation : ObjectAnnotation
    {
        public override void LoadRectObjects(string xml_file)
        {
            rectObjects = new List<RectObject>();
            doc = new HtmlDocument(); doc.Load(xml_file);

            HtmlNodeCollection object_nodes = doc.DocumentNode.SelectNodes("//object");
            foreach (HtmlAgilityPack.HtmlNode object_node in object_nodes)
            {
                HtmlNode name_node = object_node.SelectSingleNode("./name");
                string name = name_node.InnerText.Trim();

                List<PointF> points = new List<PointF>();
                HtmlNode polygon_node = object_node.SelectSingleNode("./polygon");
                HtmlNodeCollection pt_nodes = polygon_node.SelectNodes("./pt");                
                foreach (HtmlNode pt_node in pt_nodes)
                {
                    HtmlNode x_node = pt_node.SelectSingleNode("./x");
                    HtmlNode y_node = pt_node.SelectSingleNode("./y");
                    string x = x_node.InnerText.Trim();
                    string y = y_node.InnerText.Trim();

                    PointF point = new PointF(float.Parse(x), float.Parse(y));
                    points.Add(point);
                }
                Rectangle rect = Emgu.CV.PointCollection.BoundingRectangle(points.ToArray());
                rect.Width -= 1; rect.Height -= 1; //Make the rect 1 pixel smaller.

                RectObject rect_object = new RectObject(name, rect);
                rectObjects.Add(rect_object);
            }
        }
    }
}