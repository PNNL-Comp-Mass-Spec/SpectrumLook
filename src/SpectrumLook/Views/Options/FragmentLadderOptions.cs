using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


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
            get
            {
                return m_modificationList;
            }
            set
            {
                //This is a property that does need to be synced.
                this.Invoke();
                m_modificationList = value;
            }
        }

        public List<string> checkedHeaders
        {
            get
            {
                return m_checkedHeaders;
            }
            set
            {
                //This is a property that does not need to be synced.
                this.Invoke();
                m_checkedHeaders = value;
            }
        }

        public bool ammoniaLoss
        {
            get
            {
                return m_ammoniaLoss;
            }
            set
            {
                this.Invoke();
                m_ammoniaLoss = value;
            }
        }

        public bool waterLoss
        {
            get
            {
                return m_waterLoss;
            }
            set
            {
                this.Invoke();
                m_waterLoss = value;
            }
        }

        public int precision
        {
            get
            {
                return m_precision;
            }
            set
            {
                this.Invoke();
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
            this.m_modificationList = rhsOptions.m_modificationList;
            this.m_checkedHeaders = rhsOptions.m_checkedHeaders;
            this.m_ammoniaLoss = rhsOptions.ammoniaLoss;
            this.m_waterLoss = rhsOptions.waterLoss;
            this.m_precision = rhsOptions.m_precision;
        }
    }
}
