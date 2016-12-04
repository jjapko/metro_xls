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

        /// <summary>
        /// Method that writes messages into log file
        /// </summary>
        /// <param name="logMessage">message</param>
        /// <param name="w">file</param>
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        /// <summary>
        /// Method dumps written messages from log to console
        /// </summary>
        /// <param name="r">file</param>
        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
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
                    Properties.Settings.Default.theme = 0;
                    break;
                case 1:
                    metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
                    Properties.Settings.Default.theme = 1;
                    break;
            }

            Properties.Settings.Default.Save();
        }

        private void mbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroStyleManager1.Style = (MetroFramework.MetroColorStyle)Convert.ToInt32(mbColor.SelectedIndex);

            Properties.Settings.Default.style = Convert.ToInt32(mbColor.SelectedIndex);
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mbTheme.SelectedIndex = Properties.Settings.Default.theme;
            mbColor.SelectedIndex = Properties.Settings.Default.style;
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

        private void metroButton1_Click(object sender, EventArgs e)
        {
            using (StreamWriter w = File.AppendText("log.txt"))
            {
                Log("Test1", w);
                Log("Test2", w);
            }

            using (StreamReader r = File.OpenText("log.txt"))
            {
                DumpLog(r);
            }
        }
    }
}
