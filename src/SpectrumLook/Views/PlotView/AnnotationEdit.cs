using System;
using System.Windows.Forms;

namespace SpectrumLook.Views.PlotView
{
    public partial class AnnotationEdit : Form
    {
        public Annotation mAnnotation
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

            textBox1.Text = mOriginalAnnotation.mText;
            if (mModifiedAnnotation.mShowHideAuto > 0)
            {
                radioButtonVisibleAlways.Checked = true;
            }
            else if (mModifiedAnnotation.mShowHideAuto < 0)
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
                mAnnotation = mModifiedAnnotation;
            }
            else
            {
                mAnnotation = mOriginalAnnotation;
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
            mModifiedAnnotation.mText = textBox1.Text;
        }

        private void checkBoxVisibleYes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleAlways.Checked)
            {
                mModifiedAnnotation.mShowHideAuto = 1;
            }
            // if (checkBoxVisibleYes.Checked == true)
            //{
            //    mModifiedAnnotation.mShowHideAuto = 1;
            //    checkBoxVisibleNo.Checked = false;
            //}
            // else if (checkBoxVisibleNo.Checked == false) // both checkboxes are false
            //{
            //    mModifiedAnnotation.mShowHideAuto = 0;
            //}
        }

        private void checkBoxVisibleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleNever.Checked)
            {
                mModifiedAnnotation.mShowHideAuto = -1;
            }
            // if (checkBoxVisibleNo.Checked == true)
            //{
            //    mModifiedAnnotation.mShowHideAuto = -1;
            //    checkBoxVisibleYes.Checked = false;
            //}
            // else if (checkBoxVisibleYes.Checked == false) // both checkboxes are false
            //{
            //    mModifiedAnnotation.mShowHideAuto = 0;
            //}
        }

        private void radioButtonVisibleAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVisibleAuto.Checked)
            {
                mModifiedAnnotation.mShowHideAuto = 0;
            }
        }

        private void buttonResetAnnotation_Click(object sender, EventArgs e)
        {
            if (mOriginalAnnotation.mPoint.Tag != null)
            {
                textBox1.Text = mOriginalAnnotation.mPoint.Tag.ToString();
            }
            else
            {
                textBox1.Text = string.Empty;
            }
        }
    }
}
