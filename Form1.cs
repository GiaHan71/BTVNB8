using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi7cpp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load dữ liệu
            this.studentsTableAdapter.Fill(this.schoolDBDataSet.Students);

            studentsBindingSource.DataSource = schoolDBDataSet.Students;

            // Thêm ngành học
            cmbNganh.Items.Add("CNTT");
            cmbNganh.Items.Add("Kinh tế");
            cmbNganh.Items.Add("Điện tử");
            cmbNganh.Items.Add("Cơ khí");
        }

        // ================== ADD ==================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int tuoi;
                if (!int.TryParse(txtTuoi.Text, out tuoi))
                {
                    MessageBox.Show("Tuổi phải là số!");
                    return;
                }

                schoolDBDataSet.Students.AddStudentsRow(
                    txtHoTen.Text,
                    tuoi,
                    cmbNganh.Text
                );

                // Lưu xuống DB
                studentsTableAdapter.Update(schoolDBDataSet.Students);

                // Reload + trỏ tới dòng mới
                studentsTableAdapter.Fill(schoolDBDataSet.Students);
                studentsBindingSource.MoveLast();

                MessageBox.Show("Thêm thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message);
            }

        }

        // ================== UPDATE ==================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                studentsBindingSource.EndEdit();
                studentsTableAdapter.Update(schoolDBDataSet.Students);

                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }

        }

        // ================== DELETE ==================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa?",
                    "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    studentsBindingSource.RemoveCurrent();
                    studentsTableAdapter.Update(schoolDBDataSet.Students);

                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message);
            }
        }
     }

    }
