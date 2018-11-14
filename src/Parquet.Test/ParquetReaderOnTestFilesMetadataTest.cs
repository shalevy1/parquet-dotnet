using System.IO;
using System.Linq;
using Parquet.Data;
using Xunit;

namespace Parquet.Test
{
   public class ParquetReaderOnTestFilesMetadataTest : TestBase
   {
      [Fact]
      public void Version()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.Equal(1, m.Version);
            }
         }
      }

      [Fact]
      public void Num_rows()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.Equal(2, m.Num_rows);
            }
         }
      }
      [Fact]
      public void Created_by()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.Equal("parquet-mr version 1.8.1 (build 4aba4dae7bb0d4edbcf7923ae1339f28fd3f7fcf)", m.Created_by);
            }
         }
      }
      [Fact]
      public void Isset_Metadata()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.True(m.Has_key_value_metadata);
            }
         }
      }
      [Fact]
      public void Isset_CreatedBy()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.True(m.Has_created_by);
            }
         }
      }
      [Fact]
      public void MetadataKV()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.True(m.Key_value_metadata.Any());
            }
         }
      }
      [Fact]
      public void RowGroups()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.True(m.RowGroups.Any());
            }
         }
      }
      [Fact]
      public void RowGroups_Rows()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.Equal(2, m.RowGroups[0].RowCount);
            }
         }
      }
      [Fact]
      public void RowGroups_bytes()
      {
         using (Stream s = OpenTestFile("fixedlenbytearray.parquet"))
         {
            using (var r = new ParquetReader(s))
            {
               Metadata m = r.Metadata;
               Assert.Equal(67, m.RowGroups[0].TotalByteSize);
            }
         }
      }
   }
}