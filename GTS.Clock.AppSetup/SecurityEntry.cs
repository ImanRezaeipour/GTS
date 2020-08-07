using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTS.Clock.AppSetup.DataSet;
using GTS.Clock.AppSetup.DataSet.GTSDBTableAdapters;

namespace GTS.Clock.AppSetup
{
    public partial class SecurityEntry : BaseForm
    {
        public SecurityEntry()
        {
            InitializeComponent();
            this.tA_SecurityAuthorizeTableAdapter = new TA_SecurityAuthorizeTableAdapter();
            this.tA_SecurityResourceTableAdapter = new TA_SecurityResourceTableAdapter();
            this.tA_SecurityRoleTableAdapter = new TA_SecurityRoleTableAdapter();
            this.usersTableAdapter = new UsersTableAdapter();
        }

        private void SecurityEntry_Load(object sender, EventArgs e)
        {
            Refresh();

        }

        private void getPrsIdBtn_Click(object sender, EventArgs e)
        {
            if (barcodeTB.TextLength > 0)
            {
                TA_PersonTableAdapter checker = new TA_PersonTableAdapter();
                checker.Connection = GTSAppSettings.SQLConnection;
                GTSDB.TA_PersonDataTable table = checker.GetDataByBarcode(barcodeTB.Text);
                if (table.Rows.Count > 0)
                {
                    prsIDTB.Text = table.Rows[0][0].ToString();
                    //GB2Step.Enabled = true;
                }
            }
        }

        private void UnVisibleAll()
        {
            userGroupBox.Visible = false;
            rolesGroupBox.Visible = false;
            resourceGroupBox.Visible = false;
            userroleGroupBox.Visible = false;
            authorizeGroupBox.Visible = false;
        }

        private void userSaveBtn_Click(object sender, EventArgs e)
        {
            usersTableAdapter.Update(gTSDB);
            //UsersdataGrid.Refresh();

            tA_SecurityRoleTableAdapter.Update(gTSDB);
            //roledataGridView.Refresh();

            tA_SecurityResourceTableAdapter.Update(gTSDB);
            //resourceDatagrid.Refresh();

            tA_SecurityAuthorizeTableAdapter.Update(gTSDB);
           // dataGridView2.Refresh();

            Refresh();
        }

        private void usersBtn_Click(object sender, EventArgs e)
        {
            UnVisibleAll();
            userGroupBox.Visible = true;
        }

        private void RolesBtn_Click(object sender, EventArgs e)
        {
            UnVisibleAll();
            rolesGroupBox.Visible = true;
        }

        private void ResourcesBtn_Click(object sender, EventArgs e)
        {
            UnVisibleAll();
            resourceGroupBox.Visible = true;
        }

        private void userinRoleBtn_Click(object sender, EventArgs e)
        {
            UnVisibleAll();
            userroleGroupBox.Visible = true;
        }

        private void authorizeBtn_Click(object sender, EventArgs e)
        {
            UnVisibleAll();
            authorizeGroupBox.Visible = true;
        }

        private void resourceTreeViewBtn_Click(object sender, EventArgs e)
        {
            ResourceTree tree = new ResourceTree();
            tree.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoleTree tree = new RoleTree();
            tree.ShowDialog();
        }

        private void Refresh() 
        {
            this.tA_SecurityAuthorizeTableAdapter.Connection = GTSAppSettings.SQLConnection;
            this.tA_SecurityResourceTableAdapter.Connection = GTSAppSettings.SQLConnection;
            this.tA_SecurityRoleTableAdapter.Connection = GTSAppSettings.SQLConnection;
            this.usersTableAdapter.Connection = GTSAppSettings.SQLConnection;          
            
            this.tA_SecurityAuthorizeTableAdapter.Fill(this.gTSDB.TA_SecurityAuthorize);
            this.tA_SecurityResourceTableAdapter.Fill(this.gTSDB.TA_SecurityResource);
            this.tA_SecurityRoleTableAdapter.Fill(this.gTSDB.TA_SecurityRole);
            this.usersTableAdapter.Fill(this.gTSDB.Users);
        }

        private void resourceDatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (resourceDatagrid.Rows[e.RowIndex].Cells[6].Value.ToString() != String.Empty)
                {
                    GTSDB.TA_SecurityResourceDataTable resoureTable = tA_SecurityResourceTableAdapter.GetById((decimal)resourceDatagrid.Rows[e.RowIndex].Cells[6].Value);
                    if (resoureTable.Rows.Count == 1)
                    {
                        if (((GTSDB.TA_SecurityResourceRow)resoureTable.Rows[0]).resource_ParentPath == String.Empty)
                        {
                            resourceDatagrid.Rows[e.RowIndex].Cells[5].Value =
                                ((GTSDB.TA_SecurityResourceRow)resoureTable.Rows[0]).resource_ParentPath + "," +
                                resourceDatagrid.Rows[e.RowIndex].Cells[6].Value.ToString() + ",";
                        }
                        else 
                        {
                            resourceDatagrid.Rows[e.RowIndex].Cells[5].Value =
                                ((GTSDB.TA_SecurityResourceRow)resoureTable.Rows[0]).resource_ParentPath +
                                resourceDatagrid.Rows[e.RowIndex].Cells[6].Value.ToString() + ",";
                        }

                    }
                }
            }
        }

        
    }
}
