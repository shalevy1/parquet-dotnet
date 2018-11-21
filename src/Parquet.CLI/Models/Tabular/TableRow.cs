using System;
using System.Collections.Generic;
using System.Linq;

namespace Parquet.CLI.Models.Tabular
{
   public class TableRow {
      public TableCell[] Cells { get; set; }
      public int MaxCellLineCount { get { return Cells.Max(c => c.CellLineCount); } }

      public static TableRow FromBasicValues(params string[] cellValues)
      {
         var row = new TableRow();

         List<TableCell> cells = new List<TableCell>();
         for (int i = 0; i < cellValues.Length; i++)
         {
            var content = new List<ICellContent>();
            content.Add(new BasicCellContent
            {
               Value = cellValues[i]
            });
            cells.Add(new BasicTableCell
            {

               ContentAreas = content.ToArray()
            });
         }
         row.Cells = cells.ToArray();

         return row;
      }
   }
}
