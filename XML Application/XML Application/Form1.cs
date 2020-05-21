using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace XML_Application
{
    public partial class Form1 : Form
    {

        string selectedState1;
        string selectedState2;
        string fileNameToOpen;

        public Form1()
        {         
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            saveFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";
            openFileDialog1.Filter = "XML files(*.xml)|*.xml|All files(*.*)|*.*";

            OracleConnection conn = DBUtils.GetDBConnection();
            label1.Text = "Get Connection: " + conn;
            try
            {
                conn.Open();
                label1.Text = "Połączenie jest OK";
            }
            catch (Exception ex)
            {
                label1.Text = "Błąd: " + ex.Message;
                return;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }                         
            string filename = saveFileDialog1.FileName;
            selectedState1 = comboBox1.SelectedItem.ToString();
            label1.Text = Program.DbToXml(selectedState1, filename);
        }

        private void button3_Click(object sender, EventArgs e)
        {      
            if (fileNameToOpen != null)
            {
                label1.Text = Program.XmlToDb(selectedState2, fileNameToOpen);
            }
            else
            {
                label1.Text = "Nie wybrano plik";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                fileNameToOpen = openFileDialog1.FileName;
                selectedState2 = comboBox2.SelectedItem.ToString();
                textBox1.Text = fileNameToOpen;
            }
            catch(Exception ex)
            {
                label1.Text = "Błąd: " + ex.Message;
            }
        }
    }
}
