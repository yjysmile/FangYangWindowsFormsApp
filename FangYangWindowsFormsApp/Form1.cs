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

namespace FangYangWindowsFormsApp
{
    public partial class EmployeeRecordsForm : Form
    {
        private TreeNode tvRootNode;

        public EmployeeRecordsForm()
        {
            InitializeComponent();
            PopulateTreeView();
            initalizeListView();
        }

        private void EmployeeRecordsForm_Load(object sender, EventArgs e)
        {
           
        }
        private void PopulateTreeView()
        {
            statusBarPanel1.Tag = " ";
            this.Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            tvRootNode = new TreeNode("Emplyoee Records");
            this.Cursor = Cursors.Default;
            treeView1.Nodes.Add(tvRootNode);

            TreeNodeCollection nodeCollection=tvRootNode.Nodes;
            XmlTextReader reader = new XmlTextReader("C:\\Users\\yyyyy\\source\\repos\\FangYangWindowsFormsApp\\FangYangWindowsFormsApp\\EmpRec.xml");
            reader.MoveToElement();
            try
            {
                while (reader.Read())
                {
                    if(reader.HasAttributes && reader.NodeType == XmlNodeType.Element)
                    {
                        reader.MoveToElement();
                        reader.MoveToElement();

                        reader.MoveToAttribute("Id");
                        String strVal = reader.Value;

                        reader.Read();
                        reader.Read();
                        if(reader.Name == "Dept")
                        {
                            reader.Read();
                        }
                        TreeNode EcodeNode = new TreeNode(strVal);
                        nodeCollection.Add(EcodeNode);
                    }
                }
                statusBarPanel1.Text = "Click on an emplyoee code to see their record.";
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }//end
        }

        protected void initalizeListView()
        {
         listView1.Clear();
            listView1.Columns.Add("Emplyoee Name",255,HorizontalAlignment.Left);
            listView1.Columns.Add("Date of Join",70,HorizontalAlignment.Right);
            listView1.Columns.Add("Gread",105,HorizontalAlignment.Left);
            listView1.Columns.Add("Salary",105,HorizontalAlignment.Left);

        }

        protected void PopulateListView(TreeNode crrNode)
        {
            initalizeListView();
            XmlTextReader listRead = new XmlTextReader("C:\\Users\\yyyyy\\source\\repos\\FangYangWindowsFormsApp\\FangYangWindowsFormsApp\\EmpRec.xml");
            listRead.MoveToElement();

            while (listRead.Read())
            {
                String strNodeName;
                String strNodePath;
                String name;
                String gread;
                String doj;
                String sal;
                String[] strItemsArr = new String[4];
                listRead.MoveToFirstAttribute();
                strNodeName = listRead.Value;
                strNodePath = crrNode.FullPath.Remove(0,17);
                if (strNodePath == strNodeName)
                {
                    ListViewItem lvi;

                    listRead.MoveToNextAttribute();
                    name = listRead.Value;
                    lvi = listView1.Items.Add(name);

                    listRead.Read();
                    listRead.Read();

                    listRead.MoveToFirstAttribute();
                    doj = listRead.Value;
                    lvi.SubItems.Add(doj);

                    listRead.MoveToNextAttribute();
                    gread = listRead.Value;
                    lvi.SubItems.Add(gread);

                    listRead.MoveToNextAttribute();
                    sal = listRead.Value;
                    lvi.SubItems.Add(sal);

                    listRead.MoveToNextAttribute();
                    listRead.MoveToElement();
                    listRead.ReadString();
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode currNode=e.Node;
            if (tvRootNode == currNode)
            {
                initalizeListView();
                statusBarPanel1.Text = "Double Click the emplyoee records";
                return;
            }
            else
            {
                statusBarPanel1.Text = "Click an emplyoee code to view individual record";

            }
            PopulateListView(currNode);
        }
    }
}
