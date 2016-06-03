using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using HtmlAgilityPack;

namespace Panel
{
    internal struct PanelSegInfo
    {
        public Rectangle panelRect;
        public string panelLabel;
        public Rectangle labelRect;
    }

    internal class PanelSegAnnotation : ObjectAnnotation
    {
        private List<PanelSegInfo> panels = new List<PanelSegInfo>();

        public void Load(string xml_file)
        {
            HtmlDocument doc = new HtmlDocument(); doc.Load(xml_file);

            HtmlNodeCollection panelseginfo_nodes = doc.DocumentNode.SelectNodes("//gov.nih.nlm.iti.figure.panelseginfo");
            foreach (HtmlAgilityPack.HtmlNode panelseginfo_node in panelseginfo_nodes)
            {
                PanelSegInfo panel = new PanelSegInfo();

                HtmlNode panelrect_node = panelseginfo_node.SelectSingleNode("./panelrect");
                HtmlNode labelrect_node = panelseginfo_node.SelectSingleNode("./labelrect");
                HtmlNode panellabel_node = panelseginfo_node.SelectSingleNode("./panellabel");

                {   //
                    HtmlNode x_node = panelrect_node.SelectSingleNode("./x");
                    HtmlNode y_node = panelrect_node.SelectSingleNode("./y");
                    HtmlNode width_node = panelrect_node.SelectSingleNode("./width");
                    HtmlNode height_node = panelrect_node.SelectSingleNode("./height");
                    string x = x_node.InnerText.Trim();
                    string y = y_node.InnerText.Trim();
                    string width = width_node.InnerText.Trim();
                    string height = height_node.InnerText.Trim();
                    panel.panelRect = new Rectangle(int.Parse(x), int.Parse(y), int.Parse(width), int.Parse(height));
                }

                if (labelrect_node != null)
                {   //
                    HtmlNode x_node = labelrect_node.SelectSingleNode("./x");
                    HtmlNode y_node = labelrect_node.SelectSingleNode("./y");
                    HtmlNode width_node = labelrect_node.SelectSingleNode("./width");
                    HtmlNode height_node = labelrect_node.SelectSingleNode("./height");
                    string x = x_node.InnerText.Trim();
                    string y = y_node.InnerText.Trim();
                    string width = width_node.InnerText.Trim();
                    string height = height_node.InnerText.Trim();
                    panel.labelRect = new Rectangle(int.Parse(x), int.Parse(y), int.Parse(width), int.Parse(height));
                }

                if (panellabel_node != null)
                    panel.panelLabel = panellabel_node.InnerText.Trim();

                panels.Add(panel);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml_file"></param>
        public override void LoadRectObjects(string xml_file)
        {
            figureFilename = System.IO.Path.GetFileNameWithoutExtension(xml_file);

            Load(xml_file);

            rectObjects = new List<RectObject>();
            for (int i = 0; i < panels.Count; i++)
            {
                PanelSegInfo panel = panels[i];

                if (panel.panelRect.IsEmpty) continue;

                RectObject rect_panel = new RectObject("panel " + panel.panelLabel, panel.panelRect);
                rectObjects.Add(rect_panel);

                if (panel.labelRect.IsEmpty) continue;

                RectObject rect_label = new RectObject("label " + panel.panelLabel, panel.labelRect);
                rectObjects.Add(rect_label);
            }

        }
    }
}