using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChenGang1216RESWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            InitalizeListControl();
        }

        private void statusBar1_PanelClick(object sender, StatusBarPanelClickEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void PopulateTreeView()
        {
            statusBarPanel1.Tag = "Refresing Employee Code.Please wait...";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Employee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);
            TreeNodeCollection nodeCollection = tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("D:\\Users\\a'su's\\source\\repos\\MyRepos\\ChenGang1216RESWindowsFormsApp\\ChenGang1216RESWindowsFormsApp\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if (reader.HasAttributes && reader.NodeType == XmlNodeType.Element) 
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        string strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if (reader.Name == "Dept")
                        {
                            reader.Read();
                            
                        }
                        TreeNode EcodeNode =new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);
                    }
                }
                statusBarPanel1.Text = "Click on an emplyoee code to see their record.";
            }
            catch(XmlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        protected void InitalizeListControl()
        {
            listView1.Clear();
            listView1.Columns.Add("Employee Nmae", 255, HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join",70,HorizontalAlignment.Right);
            listView1.Columns.Add("Gread",105, HorizontalAlignment.Left);
            listView1.Columns.Add("Salary", 105, HorizontalAlignment.Left);
        }
protected void PopulateListViem(TreeNode crrNode)
        {
            InitalizeListControl();
            XmlTextReader listRead = new XmlTextReader("D:\\Users\\a'su's\\source\\repos\\MyRepos\\ChenGang1216RESWindowsFormsApp\\ChenGang1216RESWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();
            while (listRead.Read()) 
            {
                string strNodeName;
                string strNodePath;
                string name;
                string gread;
                string doj;
                string sal;
                string[] strItemsArr=new string[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0, 17);
                if (strNodeName == strNodePath)
                {
                    ListViewItem lvi;
                    listRead.MoveToNextAttribute();
                    name = listRead.Value;
                    lvi = listView1.Items.Add(listRead.Value);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj= listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute();
                    gread= listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute();
                    sal= listRead.Value;
                    lvi.SubItems.Add((sal));
                    listRead.MoveToNextAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();
                }
            }
        }
        private void treeViewl_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode=e.Node;
            if (tvRootNode == currNode)
            {
                InitalizeListControl();
                statusBarPanel1.Text = "Double Click the Employee Records";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an Employee code to view individual record";

            }
            PopulateListViem(currNode);
        }

       
    }
}
