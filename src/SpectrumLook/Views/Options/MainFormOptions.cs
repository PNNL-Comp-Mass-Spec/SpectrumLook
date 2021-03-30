using System;

namespace SpectrumLook.Views
{
    // TODO : Need to add a location for the ComparedListBuilder.
    [Serializable]
    public class MainFormOptions : Subject
    {
        private bool mIsPlotInMainForm;
        private double mToleranceValue;
        private double mLowerToleranceValue;

        public bool IsPlotInMainForm
        {
            get => mIsPlotInMainForm;
            set
            {
                mIsPlotInMainForm = value;
                Invoke();
            }
        }

        public double ToleranceValue
        {
            get => mToleranceValue;
            set
            {
                if (value >= 0.0)
                {
                    mToleranceValue = value;
                    Invoke();
                }
            }
        }

        public double LowerToleranceValue
        {
            get => mLowerToleranceValue;
            set
            {
                if (value >= 0.0)
                {
                    mLowerToleranceValue = value;
                    Invoke();
                }
            }
        }

        public MainFormOptions()
        {
            mIsPlotInMainForm = true;
            mToleranceValue = 0.7;
            mLowerToleranceValue = 0.0;
        }

        public MainFormOptions(MainFormOptions options)
        {
            SetOptions(options);
        }

        public void SetOptions(MainFormOptions options)
        {
            mIsPlotInMainForm = options.IsPlotInMainForm;
            mToleranceValue = options.ToleranceValue;
            mLowerToleranceValue = options.LowerToleranceValue;
        }
    }
}
