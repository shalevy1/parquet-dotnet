using System.Collections.Generic;

namespace Parquet.Data
{
   /// <summary>
   /// Metadata for this column
   /// </summary>
   public class ColumnMetadata
   {
      /// <summary>
      /// Optional key/value metadata *
      /// </summary>
      public Dictionary<string, string> Key_value_metadata { get; internal set; }

      /// <summary>
      /// Path in schema *
      /// </summary>
      public List<string> Path_in_schema { get; set; }    
      
      /// <summary>
      /// Number of values in this column *
      /// </summary>
      public long Num_values { get; set; }

      /// <summary>
      /// total byte size of all uncompressed pages in this column chunk (including the headers) *
      /// </summary>
      public long Total_uncompressed_size { get; set; }

      /// <summary>
      /// total byte size of all compressed pages in this column chunk (including the headers) *
      /// </summary>
      public long Total_compressed_size { get; set; }

      /// <summary>
      /// Byte offset from beginning of file to first data page *
      /// </summary>
      public long Data_page_offset { get; set; }

      /// <summary>
      /// Byte offset from the beginning of file to first (only) dictionary page *
      /// </summary>
      public long Dictionary_page_offset { get; set; }

      /// <summary>
      /// Type of this column *
      /// 
      /// <seealso cref="Type"/>
      /// </summary>
      public string Type { get; set; }

      /// <summary>
      /// Set of all encodings used for this column. The purpose is to validate
      /// whether we can decode those pages. *
      /// </summary>
      public List<string> Encodings { get; set; }
      /// <summary>
      /// Compression codec *
      /// </summary>
      public string Codec { get; set; }
      /// <summary>
      /// optional statistics for this column chunk
      /// </summary>
      public Statistics Statistics { get; set; }

      /* todo
      /// <summary>
      /// Set of all encodings used for pages in this column chunk.
      /// This information can be used to determine if all data pages are
      /// dictionary encoded for example *
      /// </summary>
      public List<PageEncodingStats> Encoding_stats
      {get;set;}
      */
   }
}
