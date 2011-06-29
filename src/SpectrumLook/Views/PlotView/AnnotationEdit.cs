using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace SpectrumLook.Views.PlotView
{
    public partial class AnnotationEdit : Form
    {
        public Annotation m_annotation
        {
            get;
            set;
        }

        private Annotation m_originalAnnotation;
        private Annotation m_modifiedAnnotation;

        public AnnotationEdit(Annotation annotation)
        {
            InitializeComponent();

            m_originalAnnotation = annotation;
            m_modifiedAnnotation = annotation;

            textBox1.Text = m_originalAnnotation.m_text;
            if (m_modifiedAnnotation.m_showHideAuto > 0)
            {
                radioButtonVisibleAlways.Checked = true;
            }
            else if (m_modifiedAnnotation.m_showHideAuto < 0)
            {
                radioButtonVisibleNever.Checked = true;
            }
            else
            {
                radioButtonVisibleAuto.Checked = true;
            }

            this.FormClosing += new FormClosingEventHandler(AnnotationEdit_FormClosing);
        }

        void AnnotationEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                m_annotation = m_modifiedAnnotation;
            }
            else
            {
                m_annotation = m_originalAnnotation;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            m_modifiedAnnotation.m_text = textBox1.Text;
        }

        private void checkBoxVisibleYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleAlways.Checked)
            {
                m_modifiedAnnotation.m_showHideAuto = 1;
            }
            //if (checkBoxVisibleYes.Checked == true)
            //{
            //    m_modifiedAnnotation.m_showHideAuto = 1;
            //    checkBoxVisibleNo.Checked = false;
            //}
            //else if (checkBoxVisibleNo.Checked == false) //both checkboxes are false
            //{
            //    m_modifiedAnnotation.m_showHideAuto = 0;
            //}
        }

        private void checkBoxVisibleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleNever.Checked)
            {
                m_modifiedAnnotation.m_showHideAuto = -1;
            }
            //if (checkBoxVisibleNo.Checked == true)
            //{
            //    m_modifiedAnnotation.m_showHideAuto = -1;
            //    checkBoxVisibleYes.Checked = false;
            //}
            //else if (checkBoxVisibleYes.Checked == false) //both checkboxes are false
            //{
            //    m_modifiedAnnotation.m_showHideAuto = 0;
            //}
        }

        private void radioButtonVisibleAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleAuto.Checked)
            {
                m_modifiedAnnotation.m_showHideAuto = 0;
            }
        }

        private void buttonResetAnnotation_Click(object sender, EventArgs e)
        {
            if (m_originalAnnotation.m_point.Tag != null)
            {
                textBox1.Text = m_originalAnnotation.m_point.Tag.ToString();
            }
            else
            {
                textBox1.Text = string.Empty;
            }
        }
    }
}
