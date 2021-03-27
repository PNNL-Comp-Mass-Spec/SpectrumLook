using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    public partial class DataViewAdvance : Form
    {
        public DataView DataViewMain = null;
        public DataViewAdvance()
        {
            InitializeComponent();
        }
        public void SetAdvance(List<string> inputList)
        {
            for (var i=0;i<inputList.Count;i++)
            {
                SeaFil_Pep1.Items.Insert(i, inputList[i]);
                SeaFil_Pep2.Items.Insert(i, inputList[i]);
                SeaFil_Pep3.Items.Insert(i, inputList[i]);
                SeaFil_Pep4.Items.Insert(i, inputList[i]);
                SrtClm1.Items.Insert(i, inputList[i]);
                SrtClm2.Items.Insert(i, inputList[i]);
                SrtClm3.Items.Insert(i, inputList[i]);
                checkedListBox1.Items.Insert(i, inputList[i]);
            }
            SeaFil_Pep1.DropDownStyle = ComboBoxStyle.DropDownList;
            SeaFil_Pep2.DropDownStyle = ComboBoxStyle.DropDownList;
            SeaFil_Pep3.DropDownStyle = ComboBoxStyle.DropDownList;
            SeaFil_Pep4.DropDownStyle = ComboBoxStyle.DropDownList;
            SrtClm1.DropDownStyle = ComboBoxStyle.DropDownList;
            SrtClm2.DropDownStyle = ComboBoxStyle.DropDownList;
            SrtClm3.DropDownStyle = ComboBoxStyle.DropDownList;

            this.tabPage2.Hide();
            this.tabPage3.Hide();

            this.tabControl1.TabPages.Remove(tabPage2);
            this.tabControl1.TabPages.Remove(tabPage3);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SeaFil_Pep1.SelectedIndex = -1;
            FilterComboBox1.SelectedIndex = -1;
            this.Filter_Value1.Text = null;
            this.AndOrComboBox1.SelectedIndex = -1;
            button4_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SeaFil_Pep2.SelectedIndex = -1;
            FilterComboBox2.SelectedIndex = -1;
            this.Filter_Value2.Text = null;
            this.AndOrComboBox2.SelectedIndex = -1;
            button3_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SeaFil_Pep3.SelectedIndex = -1;
            FilterComboBox3.SelectedIndex = -1;
            this.Filter_Value3.Text = null;
            this.AndOrComboBox3.SelectedIndex = -1;
            button5_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SeaFil_Pep4.SelectedIndex = -1;
            FilterComboBox4.SelectedIndex = -1;
            this.Filter_Value4.Text = null;
        }

        private void Filter_Value3_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
