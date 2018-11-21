using System;
using System.Collections.Generic;
using System.Text;

namespace Parquet.Data
{
   /// <summary>
   /// Column Chunk Metadata
   /// </summary>
   public class ColumnChunk
   {
      /// <summary>
      /// File where column data is stored.  If not set, assumed to be same file as
      /// metadata.  This path is relative to the current file. 
      /// </summary>
      public string FilePath { get; set; }

      /// <summary>
      /// Byte offset in file_path to the ColumnMetaData *
      /// </summary>
      public long FileOffset { get; set; }


      /// <summary>
      /// Column metadata for this chunk. This is the same content as what is at
      /// file_path/file_offset.  Having it here has it replicated in the file
      /// metadata.
      /// </summary>
      public ColumnMetadata Metadata { get; set; }
   }
}
