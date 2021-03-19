using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML5VideoTag
{
    public partial class Form1 : Form
    {

        ToolStripButton VideoProperties;

        public Form1()
        {
            InitializeComponent();

            this.htmlEditControl1.DocumentHTML = "<video width=\"320\" height=\"240\" controls><source src=\"https://www.w3schools.com/html/movie.mp4\" type=\"video/mp4\"><source src=\"https://www.w3schools.com/html/movie.ogg\" type=\"video/ogg\"></video>";

            VideoProperties = new ToolStripButton("Video Properties")
            {
                Padding = new Padding(3),
                ToolTipText = "Video Properties",
                Tag = "Video Properties"
            };
            this.htmlEditControl1.ToolStripItems.Add(VideoProperties);
            VideoProperties.Click += VideoProperties_Click;

            ToolStripButton InsertVideo = new ToolStripButton("Insert Video")
            {
                Padding = new Padding(3),
                ToolTipText = "Insert Video",
                Tag = "Insert Video"
            };
            this.htmlEditControl1.ToolStripItems.Add(InsertVideo);
            InsertVideo.Click += InsertVideo_Click;

            this.htmlEditControl1.UserInteraction += HtmlEditControl1_UserInteraction;

        }

        private void InsertVideo_Click(object sender, EventArgs e)
        {

            HtmlElement oVid = this.htmlEditControl1.InsertHTMLELement("video");
            HtmlElement oVidSrc;

            oVidSrc = this.htmlEditControl1.Document.CreateElement("source");
            oVidSrc.SetAttribute("src", "https://www.w3schools.com/html/movie.mp4");
            oVidSrc.SetAttribute("type", "video/mp4");

            oVid.AppendChild(oVidSrc);

            oVidSrc = this.htmlEditControl1.Document.CreateElement("source");
            oVidSrc.SetAttribute("src", "https://www.w3schools.com/html/movie.ogg");
            oVidSrc.SetAttribute("type", "video/ogg");

            oVid.AppendChild(oVidSrc);

        }

        private void VideoProperties_Click(object sender, EventArgs e)
        {
            if (htmlEditControl1.CurrentWindowsFormsElement.TagName.ToLower() == "video")
            {
                string Sources = "";

                foreach (HtmlElement oChild in htmlEditControl1.CurrentWindowsFormsElement.Children)
                {
                    Sources += oChild.GetAttribute("src") + "\r\n";
                }

                if (MessageBox.Show(this, Sources, "Play this video?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    htmlEditControl1.CurrentWindowsFormsElement.InvokeMember("play");
                }
            }
        }

        private void HtmlEditControl1_UserInteraction(object sender, Zoople.UserInteractionEventsArgs e)
        {
            if (e.InteractionType == Zoople.EditorUIEvents.onmousedown)
            {
                if (htmlEditControl1.CurrentWindowsFormsElement.TagName.ToLower() == "video")
                {
                    VideoProperties.Enabled = true;
                }
                else
                {
                    VideoProperties.Enabled = false;
                }
            }
        }
    }
}
