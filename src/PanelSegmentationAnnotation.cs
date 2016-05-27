using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using HtmlAgilityPack;

namespace Panel
{
    internal class PanelSegmentationAnnotation : ObjectAnnotation
    {
        /// <summary>
        /// Load Panel Rects Only, no Panel Labels yet.
        /// </summary>
        /// <param name="xml_file"></param>
        public override void LoadRectObjects(string xml_file)
        {
            figureFilename = System.IO.Path.GetFileNameWithoutExtension(xml_file);

            rectObjects = new List<RectObject>();
            HtmlDocument doc = new HtmlDocument(); doc.Load(xml_file);

            HtmlNodeCollection rectangle_nodes = doc.DocumentNode.SelectNodes("//rectangle");
            foreach (HtmlAgilityPack.HtmlNode rectangle_node in rectangle_nodes)
            {
                HtmlNode x_node = rectangle_node.SelectSingleNode("./x");
                HtmlNode y_node = rectangle_node.SelectSingleNode("./y");
                HtmlNode width_node = rectangle_node.SelectSingleNode("./width");
                HtmlNode height_node = rectangle_node.SelectSingleNode("./height");
                string x = x_node.InnerText.Trim();
                string y = y_node.InnerText.Trim();
                string width = width_node.InnerText.Trim();
                string height = height_node.InnerText.Trim();
                Rectangle rect = new Rectangle(int.Parse(x), int.Parse(y), int.Parse(width), int.Parse(height));

                RectObject rect_object = new RectObject("", rect);
                rectObjects.Add(rect_object);
            }
        }
    }
}