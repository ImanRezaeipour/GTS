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

namespace GTS.Clock.AppSetup
{
    public partial class RoleTree : BaseForm
    {
        const string ParentFiledName = "role_ParentID";
        const string IdFiledName = "role_ID";
        const string TextFiledName = "role_Name";
        TA_SecurityRoleTableAdapter resource = new TA_SecurityRoleTableAdapter();
        GTSDB.TA_SecurityRoleDataTable resourceTable = new GTSDB.TA_SecurityRoleDataTable();

        public RoleTree()
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
            IList <int> childs = GetChildId(0);
            for(int i=0;i<childs.Count ;i++)
            {
                TreeNode node = new TreeNode();
                node = GetChild(childs[i]);
                node.Text = GetNodeText(childs[i]);
                treeView1.Nodes.Add(node);
            }
            
        }
 
        private IList <int> GetChildId(int parentId) 
        {

            IList<int> ids = new List<int>();
            for (int i = 0; i < resourceTable.Rows.Count; i++)
            {
                if (resourceTable.Rows[i][ParentFiledName].ToString().Equals(parentId.ToString())) 
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
    }
}
