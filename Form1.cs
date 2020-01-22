using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotnetCourse_HW_2
{
    public partial class Form1 : Form
    {
        FileSystemVisitor visitor = null;

        public Form1()
        {
            InitializeComponent();

            FillDriveNodes();
            listView.Show();
        }

        private void FillTreeNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach(var dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception exception) { }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void FillDriveNodes()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode { Text = drive.Name };
                    FillTreeNode(driveNode, drive.Name);
                    treeView.Nodes.Add(driveNode);
                }
            }
            catch (Exception ex) { }
        }

        private void TreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(treeView.SelectedNode.Nodes.Count.ToString());
            treeView.MouseDoubleClick -= TreeView_MouseDoubleClick;
            listView.DrawItem += listView_DrawItem;

        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {

        }

        private void treeView_Click(object sender, EventArgs e)
        {
            treeView.MouseDoubleClick += TreeView_MouseDoubleClick;
            listView.DrawItem += listView_DrawItem;
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            listView.Clear();
            labelPath.Text = treeView.SelectedNode.FullPath;
            visitor = new FileSystemVisitor(treeView.SelectedNode.FullPath);

            try
            {
                var files = Directory.GetFiles(treeView.SelectedNode.FullPath);

                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        listView.Items.Add(file.ToString());
                    }
                }
            }
            catch(Exception)
            {

            }
        }
    }
}
