using System;
using System.Collections.Generic;

namespace SpectrumLook.Views.Options
{
    [Serializable]
    public class FragmentLadderOptions : Subject
    {
        private Dictionary<char, double> m_modificationList;

        private List<string> m_checkedHeaders;

        private bool m_ammoniaLoss;

        private bool m_waterLoss;

        private int m_precision;

        public Dictionary<char, double> modificationList
        {
            get => m_modificationList;
            set
            {
                // This is a property that does need to be synced.
                Invoke();
                m_modificationList = value;
            }
        }

        public List<string> checkedHeaders
        {
            get => m_checkedHeaders;
            set
            {
                // This is a property that does not need to be synced.
                Invoke();
                m_checkedHeaders = value;
            }
        }

        public bool ammoniaLoss
        {
            get => m_ammoniaLoss;
            set
            {
                Invoke();
                m_ammoniaLoss = value;
            }
        }

        public bool waterLoss
        {
            get => m_waterLoss;
            set
            {
                Invoke();
                m_waterLoss = value;
            }
        }

        public int precision
        {
            get => m_precision;
            set
            {
                Invoke();
                m_precision = value;
            }
        }

        public FragmentLadderOptions()
        {
            m_modificationList = new Dictionary<char, double>();
            m_checkedHeaders = new List<string>();
            m_checkedHeaders.AddRange(new string[] {
                "b",
                "b++",
                "y",
                "y++"
            });
        }

        public FragmentLadderOptions(FragmentLadderOptions rhsOptions)
        {
            m_modificationList = rhsOptions.m_modificationList;
            m_checkedHeaders = rhsOptions.m_checkedHeaders;
            m_ammoniaLoss = rhsOptions.ammoniaLoss;
            m_waterLoss = rhsOptions.waterLoss;
            m_precision = rhsOptions.m_precision;
        }
    }
}
