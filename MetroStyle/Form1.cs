using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.IO;
using Excel;

namespace MetroStyle
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            this.StyleManager = metroStyleManager1;
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

        }

        private void metroTile2_Click(object sender, EventArgs e)
        {

        }

        private void mbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (mbTheme.SelectedIndex)
            {
                case 0:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
                    break;
                case 1:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                    break;
            }
        }

        private void mbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroStyleManager1.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(mbColor.SelectedIndex);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mbColor.SelectedIndex = 0;
            mbTheme.SelectedIndex = 0;
        }

        DataSet result;

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {Filter= "Excel Workbook|*.xls", ValidateNames = true })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(fs);
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    cboSheet.Items.Clear();
                    foreach(DataTable dt in result.Tables)
                    {
                        cboSheet.Items.Add(dt.TableName);
                    }

                    reader.Close();

                }
            }
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = result.Tables[cboSheet.SelectedIndex];
        }
    }
}
