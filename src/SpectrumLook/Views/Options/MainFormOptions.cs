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
                Invoke();
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
                    Invoke();
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
                    Invoke();
                }
            }
        }

        public MainFormOptions()
        {
            m_isPlotInMainForm = true;
            m_toleranceValue = 0.7;
            m_lowerToleranceValue = 0.0;
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
