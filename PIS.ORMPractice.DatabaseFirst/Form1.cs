using PIS.ORMPractice.DatabaseFirst.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PIS.ORMPractice.DatabaseFirst
{
    public partial class Form1 : Form
    {
        KraftHeinzEntities context;
        public Form1()
        {
            InitializeComponent();
            context = new KraftHeinzEntities();
            RefreshSchoolSelectionCombobox();
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            dataGridView1.DataSource = context.departments_tb.Select(x => new
            {
                DepartmentName = x.departmentName,
                DepartmentDesc = x.departmentDescription
            }).ToList();

            dataGridView2.DataSource = context.positions_tb.Select(x => new
            {
                DepartmentName = x.departments_tb.departmentName,
                PositionName = x.positionName,
                PositionDesc = x.positionDescription
            }).ToList();
        }

        private void RefreshSchoolSelectionCombobox()
        {
            comboBoxDep.DataSource = context.departments_tb.Select(x => new DepartmentItemComboboxDTO()
            {
                IdDepartment = x.idDepartment,
                DepartmentName = x.departmentName
            }).ToList();

            comboBoxDep.DisplayMember = "DepartmentName";
            comboBoxDep.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxDep.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxDep.SelectedItem = null;
        }

        private void buttonAddDep_Click(object sender, EventArgs e)
        {
            var department = new departments_tb()
            {
                departmentName = textBoxDepName.Text,
                departmentDescription = textBoxDepDesc.Text
            };

            context.departments_tb.Add(department);
            context.SaveChanges();
            RefreshGrids();
            RefreshSchoolSelectionCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var position = new positions_tb()
            {
                idDepartment = ((DepartmentItemComboboxDTO)comboBoxDep.SelectedItem).IdDepartment,
                positionName = textBoxPosName.Text,
                positionDescription = textBoxPosDesc.Text
            };

            context.positions_tb.Add(position);
            context.SaveChanges();
            RefreshGrids();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var term = textBoxSearch.Text.Trim().ToLower();
            string sqlString = $"SELECT VALUE pos FROM Positions as pos WHERE pos.PositionName like '%{term}%' or pos.PositionDescription like '%{term}%'";

            var objctx = (context as IObjectContextAdapter).ObjectContext;

            ObjectQuery<positions_tb> positions = objctx.CreateQuery<positions_tb>(sqlString);

            if (!positions.Any())
                MessageBox.Show("No results");
            else
                dataGridView2.DataSource = positions.Select(x => new
                {
                    DepartmentName = x.departments_tb.departmentName,
                    PositionName = x.positionName,
                    PositionDesc = x.positionDescription
                }).ToList();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshSchoolSelectionCombobox();
            RefreshGrids();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            context.Database.ExecuteSqlCommand($"update positions_tb set positionDescription = '{textBoxUpdate.Text}'");
            context.SaveChanges();
            RefreshGrids();
        }
    }
}
