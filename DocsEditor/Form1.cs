using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace DocsEditor
{
    public partial class Form1 : Form
    {
        private string path;
        private XDocument doc;
        public Form1()
        {
            InitializeComponent();
            path = @"..\..\Docs\TestDoc\Content.xml";
            doc = XDocument.Load(path);
        }

        private void LoadContent()
        {
            var root = doc.Element("root");
            TreeNode rootNode = new TreeNode(root.Attribute("name").Value, 0, 1);

            var chapters = root.Elements("chapter");
            foreach(var chapt in chapters)
            {
                TreeNode chaptNode = new TreeNode(chapt.Attribute("name").Value, 4, 5);
                var paragraphs = chapt.Elements("paragraph");
                foreach(var par in paragraphs)
                {
                    TreeNode parNode = new TreeNode(par.Attribute("name").Value, 2, 3);
                    chaptNode.Nodes.Add(parNode);

                }

                rootNode.Nodes.Add(chaptNode);
            }

            treeView1.Nodes.Add(rootNode);
            treeView1.ExpandAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadContent();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string node = treeView1.SelectedNode.Text;
            string path = @"..\..\Docs\TestDoc\" + node + ".rtf";
            if (!File.Exists(path))
                MessageBox.Show("Данный раздел временно не доступен");
            else
            {
                richTextBox1.LoadFile(path);
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if(fd.ShowDialog() == DialogResult.OK)
            {
                //
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ColorDialog cd = new ColorDialog();
            if(cd.ShowDialog() == DialogResult.OK)
            {
                //
            }
        }
    }
}
