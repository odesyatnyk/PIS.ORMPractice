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
using PIS.ORMPractice.CodeFirst.DatabaseContext;
using PIS.ORMPractice.CodeFirst.DTO;
using PIS.ORMPractice.CodeFirst.Entities;

namespace PIS.ORMPractice.CodeFirst
{
    public partial class Form1 : Form
    {
        Context context;
        public Form1()
        {
            InitializeComponent();
            context = new Context();
            RefreshSchoolSelectionCombobox();
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            dataGridView1.DataSource = context.Departments.Select(x => new
            {
                DepartmentName = x.DepartmentName,
                DepartmentDesc = x.DepartmentDescription
            }).ToList();

            dataGridView2.DataSource = context.Positions.Select(x => new
            {
                DepartmentName = x.Department.DepartmentName,
                PositionName = x.PositionName,
                PositionDesc = x.PositionDescription
            }).ToList();
        }

        private void RefreshSchoolSelectionCombobox()
        {
            comboBoxDep.DataSource = context.Departments.Select(x => new DepartmentItemComboboxDTO()
            {
                IdDepartment = x.IdDepartment,
                DepartmentName = x.DepartmentName
            }).ToList();

            comboBoxDep.DisplayMember = "DepartmentName";
            comboBoxDep.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxDep.AutoCompleteMode = AutoCompleteMode.Suggest;
            comboBoxDep.SelectedItem = null;
        }

        private void buttonAddDep_Click(object sender, EventArgs e)
        {
            var department = new Department()
            {
                DepartmentName = textBoxDepName.Text,
                DepartmentDescription = textBoxDepDesc.Text
            };

            context.Departments.Add(department);
            context.SaveChanges();
            RefreshGrids();
            RefreshSchoolSelectionCombobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var position = new Position()
            {
                IdDepartment = ((DepartmentItemComboboxDTO)comboBoxDep.SelectedItem).IdDepartment,
                PositionName = textBoxPosName.Text,
                PositionDescription = textBoxPosDesc.Text
            };

            context.Positions.Add(position);
            context.SaveChanges();
            RefreshGrids();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var term = textBoxSearch.Text.Trim().ToLower();
            string sqlString = $"SELECT VALUE pos FROM Positions as pos WHERE pos.PositionName like '%{term}%' or pos.PositionDescription like '%{term}%'";

            var objctx = (context as IObjectContextAdapter).ObjectContext;

            ObjectQuery<Position> positions = objctx.CreateQuery<Position>(sqlString);

            if (!positions.Any())
                MessageBox.Show("No results");
            else
                dataGridView2.DataSource = positions.Select(x => new
                {
                    DepartmentName = x.Department.DepartmentName,
                    PositionName = x.PositionName,
                    PositionDesc = x.PositionDescription
                }).ToList();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshSchoolSelectionCombobox();
            RefreshGrids();
        }
    }
}
