using System;
using System.Collections.Generic;

namespace Parquet.CLI.Models.Tabular
{
   public class DisplayTable
   {
      public DisplayTable()
      {
         Rows = new TableRow[0];
      }
      public ColumnDetails[] ColumnDetails { get; set; }
      public TableRow Header { get; set; }
      public TableRow[] Rows { get; set; }

      public void AutoComputeColumnDetails(ViewPort viewPort)
      {
         var details = new List<ColumnDetails>();
         foreach (TableCell cell in Header.Cells)
         {
            details.Add((new ColumnDetails { columnWidth = viewPort.Width / Header.Cells.Length, type=Data.DataType.String, isNullable = false }));
         }
         this.ColumnDetails = details.ToArray();

      }
   }
}
