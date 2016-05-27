using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using HtmlAgilityPack;

namespace Panel
{
    internal class iPhotoDrawAnnotation : ObjectAnnotation
    {
        public override void LoadRectObjects(string xml_file)
        {
            figureFilename = System.IO.Path.GetFileName(xml_file).Replace("_data.xml", "");

            rectObjects = new List<RectObject>();
            HtmlDocument doc = new HtmlDocument(); doc.Load(xml_file);

            HtmlNodeCollection shape_nodes = doc.DocumentNode.SelectNodes("//shape");
            foreach (HtmlAgilityPack.HtmlNode shape_node in shape_nodes)
            {
                HtmlNode block_text_node = shape_node.SelectSingleNode(".//blocktext");
                HtmlNode text_node = block_text_node.SelectSingleNode("./text");
                string label = text_node.InnerText.Trim().ToLower();

                HtmlNode data_node = shape_node.SelectSingleNode(".//data");
                HtmlNode extent_node = data_node.SelectSingleNode("./extent");
                string x = extent_node.GetAttributeValue("X", "");
                string y = extent_node.GetAttributeValue("Y", "");
                string width = extent_node.GetAttributeValue("Width", "");
                string height = extent_node.GetAttributeValue("Height", "");
                Rectangle rect = new Rectangle((int)(float.Parse(x) + 0.5), (int)(float.Parse(y) + 0.5), (int)(float.Parse(width) + 0.5), (int)(float.Parse(height) + 0.5));

                RectObject rect_object = new RectObject(label, rect);
                rectObjects.Add(rect_object);
            }
        }

        /// <summary>
        /// Create an XML document template, which does not include any object annotation yet.
        /// </summary>
        internal void CreateDocumentTemplate()
        {
            doc = new HtmlDocument();
            doc.OptionOutputAsXml = true;
           
            HtmlNode document_node = doc.CreateElement("Document");
            document_node.Attributes.Add("FileVersion", "1.0");

            HtmlNode image_options_node = doc.CreateElement("ImageOptions");
            image_options_node.Attributes.Add("Rotation", "0");
            image_options_node.Attributes.Add("IsNegative", "False");

            HtmlNode canvas_node = doc.CreateElement("Canvas");

            HtmlNode box_node = doc.CreateElement("Box");
            box_node.Attributes.Add("Height", "0");
            box_node.Attributes.Add("Width", "0");
            box_node.Attributes.Add("Top", "0");
            box_node.Attributes.Add("Left", "0");

            HtmlNode box_color_node = doc.CreateElement("BackColor");
            box_color_node.Attributes.Add("B", "255");
            box_color_node.Attributes.Add("G", "255");
            box_color_node.Attributes.Add("R", "255");
            box_color_node.Attributes.Add("Alpha", "255");

            HtmlNode flip_node = doc.CreateElement("Flip");
            flip_node.Attributes.Add("VerticalFlip", "False");
            flip_node.Attributes.Add("HorizontalFlip", "False");

            HtmlNode layers_node = doc.CreateElement("Layers");

            HtmlNode layer_node = doc.CreateElement("Layer");
            layer_node.Attributes.Add("Visible", "True");
            layer_node.Attributes.Add("Name", "Layer1");

            HtmlNode shapes_node = doc.CreateElement("Shapes");

            //HtmlTextNode line_break_node = doc.CreateTextNode("\n");
            //HtmlTextNode tab_node = doc.CreateTextNode("\t");

            doc.DocumentNode.AppendChild(document_node);
            document_node.AppendChild(image_options_node);
            document_node.AppendChild(layers_node);


            layer_node.AppendChild(shapes_node);
            layers_node.AppendChild(layer_node);
            canvas_node.AppendChild(box_node);
            canvas_node.AppendChild(box_color_node);
            image_options_node.AppendChild(canvas_node);
            image_options_node.AppendChild(flip_node);
        }

        /// <summary>
        /// Add a rectangle object annotation to the document. 
        /// </summary>
        /// <param name="rect_object"></param>
        internal void AddRectObjectAnnotationToDocument(RectObject rect_object)
        {
            HtmlNode shape_node = doc.CreateElement("Shape");
            shape_node.Attributes.Add("Type", "Rectangle");

            HtmlNode settings_node = doc.CreateElement("Settings");

            HtmlNode font_node = doc.CreateElement("Font");
            font_node.Attributes.Add("Name", "Arial");
            font_node.Attributes.Add("Style", "Regular");
            font_node.Attributes.Add("Size", "12");

            HtmlNode font_color_node = doc.CreateElement("Color");
            font_color_node.Attributes.Add("B", "0");
            font_color_node.Attributes.Add("G", "0");
            font_color_node.Attributes.Add("R", "0");
            font_color_node.Attributes.Add("Alpha", "255");

            HtmlNode line_node = doc.CreateElement("Line");
            line_node.Attributes.Add("Width", "1");
            line_node.Attributes.Add("OutlineType", "Color");
            line_node.Attributes.Add("Join", "Round");
            line_node.Attributes.Add("Dash", "Solid");

            HtmlNode line_color_node = doc.CreateElement("Color");
            if (rect_object.Name.StartsWith("label"))
            {
                line_color_node.Attributes.Add("B", "234");
                line_color_node.Attributes.Add("G", "22");
                line_color_node.Attributes.Add("R", "30");
                line_color_node.Attributes.Add("Alpha", "255");
            }
            else
            {
                line_color_node.Attributes.Add("B", "30");
                line_color_node.Attributes.Add("G", "22");
                line_color_node.Attributes.Add("R", "234");
                line_color_node.Attributes.Add("Alpha", "255");
            }

            HtmlNode line_start_arrow_node = doc.CreateElement("StartArrowHead");
            line_start_arrow_node.Attributes.Add("Type", "None");
            line_start_arrow_node.Attributes.Add("WidthFactor", "2");
            line_start_arrow_node.Attributes.Add("HeightFactor", "1");

            HtmlNode line_end_arrow_node = doc.CreateElement("EndArrowHead");
            line_end_arrow_node.Attributes.Add("Type", "None");
            line_end_arrow_node.Attributes.Add("WidthFactor", "2");
            line_end_arrow_node.Attributes.Add("HeightFactor", "1");

            HtmlNode fill_node = doc.CreateElement("Fill");
            fill_node.Attributes.Add("FillType", "None");

            HtmlNode fill_color_node = doc.CreateElement("Color");
            fill_color_node.Attributes.Add("B", "255");
            fill_color_node.Attributes.Add("G", "255");
            fill_color_node.Attributes.Add("R", "255");
            fill_color_node.Attributes.Add("Alpha", "255");

            HtmlNode fill_gradient_settings_node = doc.CreateElement("GradientSettings");
            fill_gradient_settings_node.Attributes.Add("Type", "Linear");
            fill_gradient_settings_node.Attributes.Add("VerticalOffset", "0");
            fill_gradient_settings_node.Attributes.Add("HorizontalOffset", "0");
            fill_gradient_settings_node.Attributes.Add("Angle", "0");

            HtmlNode fill_gradient_starting_color_node = doc.CreateElement("StartingColor");
            fill_gradient_starting_color_node.Attributes.Add("B", "0");
            fill_gradient_starting_color_node.Attributes.Add("G", "0");
            fill_gradient_starting_color_node.Attributes.Add("R", "0");
            fill_gradient_starting_color_node.Attributes.Add("Alpha", "255");

            HtmlNode fill_gradient_ending_color_node = doc.CreateElement("EndingColor");
            fill_gradient_ending_color_node.Attributes.Add("B", "255");
            fill_gradient_ending_color_node.Attributes.Add("G", "255");
            fill_gradient_ending_color_node.Attributes.Add("R", "255");
            fill_gradient_ending_color_node.Attributes.Add("Alpha", "255");

            HtmlNode fill_gradient_blend_node = doc.CreateElement("Blend");

            HtmlNode fill_embedded_image_node = doc.CreateElement("EmbeddedImage");
            fill_embedded_image_node.Attributes.Add("Alpha", "255");
            fill_embedded_image_node.Attributes.Add("FileName", "");
            fill_embedded_image_node.Attributes.Add("ImageFillType", "Stretch");
            fill_embedded_image_node.Attributes.Add("Align", "Center");

            HtmlNode fill_embedded_image_stretch_setting_node = doc.CreateElement("StretchSettings");
            fill_embedded_image_stretch_setting_node.Attributes.Add("Type", "KeepOriginalSize");
            fill_embedded_image_stretch_setting_node.Attributes.Add("Align", "Center");
            fill_embedded_image_stretch_setting_node.Attributes.Add("ZoomFactor", "100");

            HtmlNode fill_embedded_image_stretch_setting_offset_node = doc.CreateElement("Offset");
            fill_embedded_image_stretch_setting_offset_node.Attributes.Add("Y", "0");
            fill_embedded_image_stretch_setting_offset_node.Attributes.Add("X", "0");

            HtmlNode fill_embedded_image_tile_setting_node = doc.CreateElement("TileSettings");
            fill_embedded_image_tile_setting_node.Attributes.Add("WrapMode", "Tile");

            HtmlNode fill_embedded_image_tile_setting_offset_node = doc.CreateElement("Offset");
            fill_embedded_image_tile_setting_offset_node.Attributes.Add("Y", "0");
            fill_embedded_image_tile_setting_offset_node.Attributes.Add("X", "0");

            HtmlNode fill_embedded_image_image_data_node = doc.CreateElement("ImageData");

            HtmlNode text_effect_node = doc.CreateElement("TextEffect");
            text_effect_node.Attributes.Add("UseTextEffect", "False");

            HtmlNode block_text_node = doc.CreateElement("BlockText");
            block_text_node.Attributes.Add("Align", "Center");
            block_text_node.Attributes.Add("RightToLeft", "No");

            HtmlNode text_node = doc.CreateElement("Text");
            HtmlTextNode text_value_node = doc.CreateTextNode(rect_object.Name);

            HtmlNode margin_node = doc.CreateElement("Margin");
            margin_node.Attributes.Add("Top", "0");
            margin_node.Attributes.Add("Left", "0");
            margin_node.Attributes.Add("Bottom", "0");
            margin_node.Attributes.Add("Right", "0");

            HtmlNode data_node = doc.CreateElement("Data");
            data_node.Attributes.Add("Rotation", "0");
            data_node.Attributes.Add("RoundCornerRadius", "0");
            data_node.Attributes.Add("IsRoundCorner", "False");

            HtmlNode extent_node = doc.CreateElement("Extent");
            extent_node.Attributes.Add("Height", rect_object.Rect.Height.ToString());
            extent_node.Attributes.Add("Width", rect_object.Rect.Width.ToString());
            extent_node.Attributes.Add("Y", rect_object.Rect.Y.ToString());
            extent_node.Attributes.Add("X", rect_object.Rect.X.ToString());

            HtmlNode shapes_node = doc.DocumentNode.SelectSingleNode("//shapes");
            shapes_node.AppendChild(shape_node);
            shape_node.AppendChild(settings_node);
            settings_node.AppendChild(font_node);
            font_node.AppendChild(font_color_node);
            settings_node.AppendChild(line_node);
            line_node.AppendChild(line_color_node);
            line_node.AppendChild(line_start_arrow_node);
            line_node.AppendChild(line_end_arrow_node);
            settings_node.AppendChild(fill_node);
            fill_node.AppendChild(fill_color_node);
            fill_node.AppendChild(fill_gradient_settings_node);
            fill_gradient_settings_node.AppendChild(fill_gradient_starting_color_node);
            fill_gradient_settings_node.AppendChild(fill_gradient_ending_color_node);
            fill_gradient_settings_node.AppendChild(fill_gradient_blend_node);
            fill_node.AppendChild(fill_embedded_image_node);
            fill_embedded_image_node.AppendChild(fill_embedded_image_stretch_setting_node);
            fill_embedded_image_stretch_setting_node.AppendChild(fill_embedded_image_stretch_setting_offset_node);
            fill_embedded_image_node.AppendChild(fill_embedded_image_tile_setting_node);
            fill_embedded_image_tile_setting_node.AppendChild(fill_embedded_image_tile_setting_offset_node);
            fill_embedded_image_node.AppendChild(fill_embedded_image_image_data_node);
            settings_node.AppendChild(text_effect_node);
            shape_node.AppendChild(block_text_node);
            block_text_node.AppendChild(text_node);
            text_node.AppendChild(text_value_node);
            block_text_node.AppendChild(margin_node);
            shape_node.AppendChild(data_node);
            data_node.AppendChild(extent_node);
        }
    }
}