using System;
using System.Collections.Generic;
using System.Text;

namespace Parquet.Data
{
   /// <summary>
   /// Statistics for a Column Chunk
   /// </summary>
   public class Statistics
   {
      /// <summary>
      /// max value of the column, encoded in PLAIN encoding
      /// </summary>
      public byte[] Max { get; set; }

      /// <summary>
      /// min value of the column, encoded in PLAIN encoding
      /// </summary>
      public byte[] Min { get; set; }

      /// <summary>
      /// count of null value in the column
      /// </summary>
      public long? Null_count { get; set; }

      /// <summary>
      /// count of distinct values occurring
      /// </summary>
      public long? Distinct_count { get; set; }
   }
}
