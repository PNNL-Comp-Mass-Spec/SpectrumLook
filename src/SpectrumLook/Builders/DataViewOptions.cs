using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpectrumLook.Builders
{
    class DataViewOptions
    {
        public struct FilterElement
        {
            public string SearchClm1;
            public string SearchClm2;
            public string SearchClm3;
            public string SearchClm4;
            public string Operator1;
            public string Operator2;
            public string Operator3;
            public string Operator4;
            public string InputText1;
            public string InputText2;
            public string InputText3;
            public string InputText4;
            public bool AndOr1;
            public bool AndOr2;
            public bool AndOr3;
        }

        public void AdvancedFilter(FilterElement InputFilter)
        {
            if ((InputFilter.SearchClm1 == null) || (InputFilter.Operator1 == null) || (InputFilter.InputText1== null))
            {
                MessageBox.Show("Option Selected not correctly");
            }
            else
            {
                AdvancedSearch(true,InputFilter.SearchClm1, InputFilter.Operator1, InputFilter.InputText1);
                if ((InputFilter.SearchClm2 != null) && (InputFilter.Operator2 != null) && (InputFilter.InputText2 != null))
                {
                    AdvancedSearch(InputFilter.AndOr1, InputFilter.SearchClm2, InputFilter.Operator2, InputFilter.InputText2);
                    if ((InputFilter.SearchClm3 != null) && (InputFilter.Operator3 != null) && (InputFilter.InputText3 != null))
                    {
                        AdvancedSearch(InputFilter.AndOr2, InputFilter.SearchClm3, InputFilter.Operator3, InputFilter.InputText3);
                        if ((InputFilter.SearchClm4 != null) && (InputFilter.Operator4 != null) && (InputFilter.InputText4 != null))
                        {
                            AdvancedSearch(InputFilter.AndOr3, InputFilter.SearchClm4, InputFilter.Operator4, InputFilter.InputText4);

                        }
                        else
                        {
                            if ((InputFilter.SearchClm4 != null) || (InputFilter.Operator4 != null) || (InputFilter.InputText4 != null))
                            {
                                MessageBox.Show("Option Selected not correctly in the fourth condition");
                            }
                        }
                    }
                    else
                    {
                        if ((InputFilter.SearchClm2 != null) || (InputFilter.Operator2 != null) || (InputFilter.InputText2 != null))
                        {
                            MessageBox.Show("Option Selected not correctly in the third condition");
                        }
                    }
                }
                else
                {
                    if ((InputFilter.SearchClm2 != null) || (InputFilter.Operator2 != null) || (InputFilter.InputText2 != null))
                    {
                        MessageBox.Show("Option Selected not correctly in the second condition");
                    }
                }

             }
        }


        /// <summary>
        /// Get the information from the Advanced Search Options and Search under the condition
        /// </summary>
        /// <param name="AndOr"></param> User select either 'And' or 'Or' for searching in several columns
        /// <param name="SelCol"></param>
        /// <param name="SelOpt"></param> 'contains' means the string is included '=' exact match string or number '<' larger than the value user input the '>' less than the user input
        /// <param name="TextInput"></param>
        public void AdvancedSearch(bool AndOr, string SelCol, string SelOpt, string TextInput)
        {
            int indexOfDataTable = 0;
            int ColNum = ReturnNum(SelCol);
            double SelDoubleValue;
            double InputDoubleValue;
            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[DataGridTable.DataSource];
            currencyManager1.SuspendBinding();
            for (int i = 0; i < 19; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            for (indexOfDataTable = 0; indexOfDataTable < DataGridTable.Rows.Count; ++indexOfDataTable)
            {
                if (AndOr == "AND")// and
                {
                    if (DataGridTable.Rows[indexOfDataTable].Visible == true)
                    {
                        if (SelOpt == "contains")// contains
                        {
                            if (!SearchingForSubString(TextInput, DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString()))
                                DataGridTable.Rows[indexOfDataTable].Visible = false;
                        }
                        else if (SelOpt == ">")//>
                        {
                            if (Double.TryParse(TextInput, out InputDoubleValue))
                            {
                                if (Double.TryParse(DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString(), out SelDoubleValue))
                                {
                                    if (SelDoubleValue <= InputDoubleValue)
                                    {
                                        DataGridTable.Rows[indexOfDataTable].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Input type");
                            }
                        }
                        else if (SelOpt == "<")//<
                        {
                            if (Double.TryParse(TextInput, out InputDoubleValue))
                            {
                                if (Double.TryParse(DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString(), out SelDoubleValue))
                                {
                                    if (SelDoubleValue >= InputDoubleValue && DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString() != null)
                                    {
                                        // SearchBox.Text = DataGridTable.Rows[indexOfDataTable].Cells[1].Value.ToString();
                                        DataGridTable.Rows[0].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Input type");
                            }
                        }
                        else if (SelOpt == "=")//=
                        {
                            // Console.WriteLine("DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString  {0}", DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString());

                            if (Double.TryParse(TextInput, out InputDoubleValue))
                            {

                                if (Double.TryParse(DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString(), out SelDoubleValue))
                                {
                                    if (SelDoubleValue > InputDoubleValue || SelDoubleValue > InputDoubleValue)
                                    {
                                        DataGridTable.Rows[indexOfDataTable].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Input type");
                            }
                        }

                    }
                }
                else // or
                {
                    if (DataGridTable.Rows[indexOfDataTable].Visible == false)
                    {
                        if (SelOpt == "contains")// contains
                        {
                            if (!SearchingForSubString(TextInput, DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString()))
                                DataGridTable.Rows[indexOfDataTable].Visible = false;
                        }
                        else if (SelOpt == ">")//>
                        {
                            if (Double.TryParse(TextInput, out InputDoubleValue))
                            {
                                if (Double.TryParse(DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString(), out SelDoubleValue))
                                {
                                    if (SelDoubleValue <= InputDoubleValue)
                                    {
                                        DataGridTable.Rows[indexOfDataTable].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Input type");
                            }
                        }
                        else if (SelOpt == "<")//<
                        {
                            if (Double.TryParse(TextInput, out InputDoubleValue))
                            {
                                if (Double.TryParse(DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString(), out SelDoubleValue))
                                {
                                    if (SelDoubleValue >= InputDoubleValue)
                                    {
                                        DataGridTable.Rows[indexOfDataTable].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Input type");
                            }
                        }
                        else if (SelOpt == "=")//=
                        {
                            if (Double.TryParse(TextInput, out InputDoubleValue))
                            {
                                if (Double.TryParse(DataGridTable.Rows[indexOfDataTable].Cells[ColNum].Value.ToString(), out SelDoubleValue))
                                {
                                    if (SelDoubleValue > InputDoubleValue || SelDoubleValue > InputDoubleValue)
                                    {
                                        DataGridTable.Rows[indexOfDataTable].Visible = false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Wrong Input type");
                            }
                        }
                    }
                }

            }
            for (int i = 0; i < 19; i++)
            {
               // DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            }

        }
    }
}
