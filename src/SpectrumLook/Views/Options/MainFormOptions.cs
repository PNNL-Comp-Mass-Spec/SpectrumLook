using System;

namespace SpectrumLook.Views
{
    // TODO : Need to add a location for the ComparedListBuilder.
    [Serializable]
    public class MainFormOptions : Subject
    {
        private bool m_isPlotInMainForm;
        private double m_toleranceValue;
        private double m_lowerToleranceValue;

        public bool isPlotInMainForm
        {
            get => m_isPlotInMainForm;
            set
            {
                m_isPlotInMainForm = value;
                this.Invoke();
            }
        }

        public double toleranceValue
        {
            get => m_toleranceValue;
            set
            {
                if (value >= 0.0)
                {
                    m_toleranceValue = value;
                    this.Invoke();
                }
            }
        }

        public double lowerToleranceValue
        {
            get => m_lowerToleranceValue;
            set
            {
                if (value >= 0.0)
                {
                    m_lowerToleranceValue = value;
                    this.Invoke();
                }
            }
        }

        public MainFormOptions()
        {
            this.m_isPlotInMainForm = true;
            this.m_toleranceValue = 0.7;
            this.m_lowerToleranceValue = 0.0;
        }

        public MainFormOptions(MainFormOptions optionsToCopy)
        {
            m_isPlotInMainForm = optionsToCopy.isPlotInMainForm;
            m_toleranceValue = optionsToCopy.toleranceValue;
            m_lowerToleranceValue = optionsToCopy.lowerToleranceValue;
        }

        public void CopyOptions(MainFormOptions optionsToCopy)
        {
            isPlotInMainForm = optionsToCopy.isPlotInMainForm;
            toleranceValue = optionsToCopy.toleranceValue;
            lowerToleranceValue = optionsToCopy.lowerToleranceValue;
        }
    }
}
