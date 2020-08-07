using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters;
using GTS.Clock.AppSetup.DataSet.DBDataSetTableAdapters;
using GTS.Clock.AppSetup.DataSet.ClockDataSetTableAdapters;
using GTS.Clock.Infrastructure.Utility;

namespace GTS.Clock.AppSetup
{
    public partial class ResourceTree : BaseForm
    {
        const string ParentFiledName = "resource_ParentID";
        const string IdFiledName = "resource_ID";
        const string TextFiledName = "resource_Description";
        TA_SecurityResourceTableAdapter resource = new TA_SecurityResourceTableAdapter();
        GTSDB.TA_SecurityResourceDataTable resourceTable = new GTSDB.TA_SecurityResourceDataTable();
        
        public ResourceTree()
        {
            InitializeComponent();
        }

        private void ResourceTree_Load(object sender, EventArgs e)
        {
            resource.Connection = GTSAppSettings.SQLConnection;
            resource.Fill(resourceTable);
          
        }
       
        private void BindTree()
        {
            IList<int> childs = GetChildId(0);
            for (int i = 0; i < childs.Count; i++)
            {
                TreeNode node = new TreeNode();
                node = GetChild(childs[i]);
                node.Text = GetNodeText(childs[i]);
                treeView1.Nodes.Add(node);
            }

        }

        private IList<int> GetChildId(int parentId)
        {

            IList<int> ids = new List<int>();
            for (int i = 0; i < resourceTable.Rows.Count; i++)
            {
                if (parentId > 0)
                {
                    if (resourceTable.Rows[i][ParentFiledName].ToString().Equals(parentId.ToString()))
                    {
                        ids.Add(Convert.ToInt32(resourceTable.Rows[i][IdFiledName]));
                    }
                }
                if (resourceTable.Rows[i][ParentFiledName].ToString().Equals(""))
                {
                    ids.Add(Convert.ToInt32(resourceTable.Rows[i][IdFiledName]));
                }
            }
            return ids;

        }

        private TreeNode GetChild(int parentId)
        {
            TreeNode node = new TreeNode();
            TreeNode tmpNode = new TreeNode();
            for (int i = 0; i < resourceTable.Rows.Count; i++)
            {
                if (resourceTable.Rows[i][ParentFiledName].ToString().Equals(parentId.ToString()))
                {
                    int nodeId = Convert.ToInt32(resourceTable.Rows[i][IdFiledName]);
                    tmpNode = GetChild(nodeId);
                    tmpNode.Text = GetNodeText(nodeId);
                    node.Nodes.Add(tmpNode);
                }
            }
            return node;
        }

        public string GetNodeText(int id)
        {
            for (int i = 0; i < resourceTable.Rows.Count; i++)
            {
                if (resourceTable.Rows[i][IdFiledName].ToString().Equals(id.ToString()))
                {
                    return resourceTable.Rows[i][TextFiledName].ToString();
                }
            }
            return "";
        }

        private void Show_Click(object sender, EventArgs e)
        {
            BindTree();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            for (int i = 0; i < resourceTable.Rows.Count; i++)
            {
                GTSDB.TA_SecurityResourceRow resourceRow = (GTSDB.TA_SecurityResourceRow)resourceTable.Rows[i];

                if ((resourceTable.Rows[i][ParentFiledName] == DBNull.Value ? 0 : resourceRow.resource_ParentID) > 0)
                {
                    string path = "";
                    this.GetParentPath(resourceRow.resource_ID, ref path);
                    resource.UpdateParentPath(path + ",", resourceRow.resource_ID);
                }
            }
            Cursor = Cursors.Default;
            MessageBox.Show("با موفقیت به پایان رسید");
        }

        private void GetParentPath(decimal resourceId,ref string path)
        {
            for (int i = 0; i < resourceTable.Rows.Count; i++)
            {
                GTSDB.TA_SecurityResourceRow resourceRow = (GTSDB.TA_SecurityResourceRow)resourceTable.Rows[i];
                if (resourceRow.resource_ID == resourceId)
                {
                    if ((resourceTable.Rows[i][ParentFiledName] == DBNull.Value ? 0 : resourceRow.resource_ParentID) > 0)
                    {
                        path += "," + resourceRow.resource_ParentID.ToString();
                        this.GetParentPath(resourceRow.resource_ParentID, ref path);
                    }
                }
            }
        }
    }

}
