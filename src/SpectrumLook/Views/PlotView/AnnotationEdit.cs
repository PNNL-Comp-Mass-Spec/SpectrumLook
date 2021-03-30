using System;
using System.Windows.Forms;

namespace SpectrumLook.Views.PlotView
{
    public partial class AnnotationEdit : Form
    {
        public Annotation Annotation
        {
            get;
            set;
        }

        private readonly Annotation mOriginalAnnotation;
        private Annotation mModifiedAnnotation;

        public AnnotationEdit(Annotation annotation)
        {
            InitializeComponent();

            mOriginalAnnotation = annotation;
            mModifiedAnnotation = annotation;

            textBox1.Text = mOriginalAnnotation.Text;
            if (mModifiedAnnotation.ShowHideAuto > 0)
            {
                radioButtonVisibleAlways.Checked = true;
            }
            else if (mModifiedAnnotation.ShowHideAuto < 0)
            {
                radioButtonVisibleNever.Checked = true;
            }
            else
            {
                radioButtonVisibleAuto.Checked = true;
            }

            FormClosing += AnnotationEdit_FormClosing;
        }

        private void AnnotationEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                Annotation = mModifiedAnnotation;
            }
            else
            {
                Annotation = mOriginalAnnotation;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mModifiedAnnotation.Text = textBox1.Text;
        }

        private void checkBoxVisibleYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleAlways.Checked)
            {
                mModifiedAnnotation.ShowHideAuto = 1;
            }
            // if (checkBoxVisibleYes.Checked == true)
            //{
            //    mModifiedAnnotation.ShowHideAuto = 1;
            //    checkBoxVisibleNo.Checked = false;
            //}
            // else if (checkBoxVisibleNo.Checked == false) // both checkboxes are false
            //{
            //    mModifiedAnnotation.ShowHideAuto = 0;
            //}
        }

        private void checkBoxVisibleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleNever.Checked)
            {
                mModifiedAnnotation.ShowHideAuto = -1;
            }
            // if (checkBoxVisibleNo.Checked == true)
            //{
            //    mModifiedAnnotation.ShowHideAuto = -1;
            //    checkBoxVisibleYes.Checked = false;
            //}
            // else if (checkBoxVisibleYes.Checked == false) // both checkboxes are false
            //{
            //    mModifiedAnnotation.ShowHideAuto = 0;
            //}
        }

        private void radioButtonVisibleAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleAuto.Checked)
            {
                mModifiedAnnotation.ShowHideAuto = 0;
            }
        }

        private void buttonResetAnnotation_Click(object sender, EventArgs e)
        {
            if (mOriginalAnnotation.Point.Tag != null)
            {
                textBox1.Text = mOriginalAnnotation.Point.Tag.ToString();
            }
            else
            {
                textBox1.Text = string.Empty;
            }
        }
    }
}
