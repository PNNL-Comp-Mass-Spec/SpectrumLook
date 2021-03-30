using System;
using System.Collections.Generic;

namespace SpectrumLook.Views.Options
{
    [Serializable]
    public class FragmentLadderOptions : Subject
    {
        private Dictionary<char, double> mModificationList;

        private List<string> mCheckedHeaders;

        private bool mAmmoniaLoss;

        private bool mWaterLoss;

        private int mPrecision;

        public Dictionary<char, double> ModificationList
        {
            get => mModificationList;
            set
            {
                // This is a property that does need to be synced.
                Invoke();
                mModificationList = value;
            }
        }

        public List<string> CheckedHeaders
        {
            get => mCheckedHeaders;
            set
            {
                // This is a property that does not need to be synced.
                Invoke();
                mCheckedHeaders = value;
            }
        }

        public bool AmmoniaLoss
        {
            get => mAmmoniaLoss;
            set
            {
                Invoke();
                mAmmoniaLoss = value;
            }
        }

        public bool WaterLoss
        {
            get => mWaterLoss;
            set
            {
                Invoke();
                mWaterLoss = value;
            }
        }

        public int Precision
        {
            get => mPrecision;
            set
            {
                Invoke();
                mPrecision = value;
            }
        }

        public FragmentLadderOptions()
        {
            mModificationList = new Dictionary<char, double>();
            mCheckedHeaders = new List<string>();
            mCheckedHeaders.AddRange(new string[] {
                "b",
                "b++",
                "y",
                "y++"
            });
        }

        public FragmentLadderOptions(FragmentLadderOptions rhsOptions)
        {
            mModificationList = rhsOptions.mModificationList;
            mCheckedHeaders = rhsOptions.mCheckedHeaders;
            mAmmoniaLoss = rhsOptions.AmmoniaLoss;
            mWaterLoss = rhsOptions.WaterLoss;
            mPrecision = rhsOptions.mPrecision;
        }
    }
}
