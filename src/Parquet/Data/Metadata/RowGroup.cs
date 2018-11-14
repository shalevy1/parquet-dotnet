using System;
using System.Collections.Generic;
using System.Text;

namespace Parquet.Data
{
   /// <summary>
   /// Row group holding columnar infomration
   /// </summary>
   public class RowGroup
   {
      /// <summary>
      /// Columns in this Row group
      /// </summary>
      public List<ColumnChunk> Columns { get; set; }

      /// <summary>
      /// [total_byte_size] in Parquet RowGroup
      /// </summary>
      public long TotalByteSize { get; set; }

      /// <summary>
      /// [num_rows] in Parquet RowGroup
      /// </summary>
      public long RowCount { get; set; }
   }
}
