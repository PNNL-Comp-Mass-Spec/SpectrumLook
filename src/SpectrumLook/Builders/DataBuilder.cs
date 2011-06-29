using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SpectrumLook.Builders
{
    static class DataBuilder
    {
        static public DataTable GetDataTable(ISynopsysParser synopsisParser)
        {
            DataRow row = null;
            DataTable dataTable = new DataTable("DataViewTable");
            string[] InLine;
            InLine = synopsisParser.GetNextColumn();
            if (InLine == null)
            {
                throw new System.InvalidProgramException("The synopsis file is empty");
            }
            else
            {
                foreach (string i in InLine)
                {
                    dataTable.Columns.Add(new DataColumn(i, typeof(string)));
                    if (i.ToLower().Contains("peptide_p"))
                    {
                        dataTable.Columns[i].ReadOnly = false;
                    }
                    else
                    {
                        dataTable.Columns[i].ReadOnly = true;
                    }
                }
                InLine = synopsisParser.GetNextColumn();

                while (InLine != null)
                {
                    row = dataTable.NewRow();
                    row.ItemArray = InLine;
                    dataTable.Rows.Add(row);

                    InLine = synopsisParser.GetNextColumn();
                }
            }

            return dataTable;
        }
    }
}
