using System.IO;
using System.Linq;
using Parquet.Data;
using Xunit;

namespace Parquet.Test
{
   public class ParquetReaderOnTestFilesMetadataTest : TestBase
   {
      private Metadata _m;

      public ParquetReaderOnTestFilesMetadataTest()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               _m = r.Metadata;
            }
         }
      }
      [Fact]
      public void Version()
      {
         Assert.Equal(1, _m.Version);
      }

      [Fact]
      public void Num_rows()
      {
         Assert.Equal(2, _m.Num_rows);
      }
      [Fact]
      public void Created_by()
      {
         Assert.Equal("parquet-mr version 1.8.1 (build 4aba4dae7bb0d4edbcf7923ae1339f28fd3f7fcf)", _m.Created_by);
      }
      [Fact]
      public void Isset_Metadata()
      {
         Assert.True(_m.Has_key_value_metadata);
      }
      [Fact]
      public void Isset_CreatedBy()
      {
         Assert.True(_m.Has_created_by);
      }
      [Fact]
      public void MetadataKV()
      {
         Assert.True(_m.Key_value_metadata.Any());
      }
      [Fact]
      public void RowGroups()
      {
         Assert.True(_m.RowGroups.Any());
      }
      [Fact]
      public void RowGroups_Rows()
      {
         Assert.Equal(2, _m.RowGroups[0].RowCount);
      }
      [Fact]
      public void RowGroups_bytes()
      {
         Assert.Equal(67, _m.RowGroups[0].TotalByteSize);
      }
   }
}