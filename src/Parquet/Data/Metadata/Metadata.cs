using System;
using System.Collections.Generic;
using System.Text;

namespace Parquet.Data
{

   /// <summary>
   /// Represents file metadata
   /// </summary>
   public class Metadata
   {
      /// <summary>
      /// Version of this file *
      /// </summary>
      public int Version { get; internal set; }

      /// <summary>
      /// Number of rows in this file *
      /// </summary>
      public long Num_rows { get; internal set; }

      /// <summary>
      /// String for application that wrote this file.  This should be in the format
      /// [Application] version [App Version] (build [App Build Hash]).
      /// e.g. impala version 1.0 (build 6cf94d29b2b7115df4de2c06e2ab4326d721eb55)
      /// </summary>
      public string Created_by { get; internal set; }

      /// <summary>
      /// Is the KV Metadata Set?
      /// </summary>
      public bool Has_key_value_metadata { get; internal set; }

      /// <summary>
      /// Is the CreatedBy property set?
      /// </summary>
      public bool Has_created_by { get; internal set; }

      /// <summary>
      /// Optional key/value metadata *
      /// </summary>
      public Dictionary<string, string> Key_value_metadata { get; internal set; }

      /// <summary>
      /// RowGroups in this file
      /// </summary>
      public List<RowGroup> RowGroups { get; set; }
   }
}
