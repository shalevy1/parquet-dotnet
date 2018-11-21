using System;
using System.Collections.Generic;
using System.Text;
using Parquet.CLI.Models;
using Parquet.CLI.Models.Tabular;
using Parquet.Data;

namespace Parquet.CLI.Views
{
   enum Level
   {
      Top, 
      RowGroup,
      ColumnChunk,
      Statistics
   }
   public class MetadataView : IDrawViews<Metadata>
   {
      Level _level = Level.Top;
      public void Draw(Metadata viewModel, ViewSettings settings)
      {
         DrawTopLevelMetadataTable(viewModel);

         AwaitInput(viewModel);
      }

      private void AwaitInput(Metadata viewModel)
      {
         switch (_level)
         {
            case Level.Top:
               Console.WriteLine("Press -> for Row Group details, ENTER to quit.");
               break;
            case Level.RowGroup:
               Console.WriteLine("Press <- for Top Level details, -> for ColumnChunk details, ENTER to quit.");
               break;
            case Level.ColumnChunk:
               break;
            case Level.Statistics:
               break;
            default:
               break;
         }
         ConsoleKeyInfo key = Console.ReadKey();
         if ((key.Key == ConsoleKey.RightArrow && _level == Level.Top)
             || (key.Key == ConsoleKey.LeftArrow && _level == Level.ColumnChunk))
         {
            _level = Level.RowGroup;
            Console.Clear();
            DrawRowGroupMetadataTable(viewModel);
            AwaitInput(viewModel);

         }
         if (key.Key == ConsoleKey.RightArrow && _level == Level.RowGroup)
         {
            _level = Level.ColumnChunk;
            Console.Clear();
            DrawColumnChunkMetadataTable(viewModel.RowGroups[0].Columns[0]);
            AwaitInput(viewModel);

         }
         if (key.Key == ConsoleKey.LeftArrow && _level == Level.RowGroup)
         {
            _level = Level.Top;
            Console.Clear();
            DrawTopLevelMetadataTable(viewModel);
            AwaitInput(viewModel);
         }
         if (key.Key == ConsoleKey.Enter)
         {
            return;
         }
         AwaitInput(viewModel);
      }

      private void DrawColumnChunkMetadataTable(ColumnChunk columnChunk)
      {
         DisplayTable displayTable = new DisplayTable();
         displayTable.Header = TableRow.FromBasicValues("FileOffset", "FilePath", "Codec", "Data Page Offset", "Dict Page Offset", "Encodings", "Number of Values", "Path In Schema", "Total Compressed Size", "Total Uncompressed Size");
         displayTable.Rows = new TableRow[]
         {
            TableRow.FromBasicValues(
               columnChunk.FileOffset.ToString(),
               columnChunk.FilePath,
               columnChunk.Metadata.Codec,
               columnChunk.Metadata.Data_page_offset.ToString(),
               columnChunk.Metadata.Dictionary_page_offset.ToString(),
               string.Join(',', columnChunk.Metadata.Encodings.ToArray()),
               columnChunk.Metadata.Num_values.ToString(),
               columnChunk.Metadata.Path_in_schema.ToString(),
               columnChunk.Metadata.Total_compressed_size.ToString(),
               columnChunk.Metadata.Total_uncompressed_size.ToString()
            )
         };
         displayTable.AutoComputeColumnDetails(new ViewPort());
         new Tablular.TableWriter(new ViewPort()).Draw(displayTable);
      }

      private void DrawRowGroupMetadataTable(Metadata viewModel)
      {
         DisplayTable displayTable = new DisplayTable();
         displayTable.Header = GenerateRowGroupHeader();
         displayTable.ColumnDetails = GenerateRowGroupColumns();
         displayTable.Rows = GenerateRowGroupRows(viewModel);
         new Tablular.TableWriter(new ViewPort()).Draw(displayTable);
      }

      private ColumnDetails[] GenerateRowGroupColumns()
      {
         return new ColumnDetails[]
           {
            new ColumnDetails { columnName="RowGroup", columnWidth=20, isNullable=false, type=DataType.String },
            new ColumnDetails { columnName="Total Size", columnWidth=20, isNullable=false, type=DataType.String },
            new ColumnDetails { columnName="NumRows", columnWidth=20, isNullable=false, type=DataType.String },
            new ColumnDetails { columnName="Columns", columnWidth=30, isNullable=false, type=DataType.String },
           };
      }

      private TableRow GenerateRowGroupHeader()
      {
         var row = new TableRow();
         List<TableCell> headers = new List<TableCell>();

         var content = new List<ICellContent>();
         content.Add(new BasicCellContent
         {
            Value = "RowGroup"
         });
         headers.Add(new BasicTableCell
         {

            ContentAreas = content.ToArray()
         });
         content = new List<ICellContent>();
         content.Add(new BasicCellContent
         {
            Value = "Total Size (B)"
         });
         headers.Add(new BasicTableCell
         {

            ContentAreas = content.ToArray()
         });
         content = new List<ICellContent>();
         content.Add(new BasicCellContent
         {
            Value = "Number of Rows"
         });
         headers.Add(new BasicTableCell
         {

            ContentAreas = content.ToArray()
         });
         content = new List<ICellContent>();
         content.Add(new BasicCellContent
         {
            Value = "Number of Column Chunks"
         });
         headers.Add(new BasicTableCell
         {

            ContentAreas = content.ToArray()
         });

         row.Cells = headers.ToArray();
         return row;
      }

      private void DrawTopLevelMetadataTable(Metadata viewModel)
      {
         DisplayTable displayTable = new DisplayTable();
         displayTable.Header = GenerateTopLevelHeader();
         displayTable.ColumnDetails = GenerateColumns();
         displayTable.Rows = GenerateRows(viewModel);
         new Tablular.TableWriter(new ViewPort()).Draw(displayTable);
      }

      private TableRow[] GenerateRowGroupRows(Metadata viewModel)
      {
         List<TableRow> rows = new List<TableRow>();
         for (int i = 0; i < viewModel.RowGroups.Count; i++)
         {
            RowGroup item = viewModel.RowGroups[i];
            AddRowGroupRow(rows, i, item.TotalByteSize, item.RowCount, item.Columns.Count);
         }
         return rows.ToArray();
      }

      private void AddRowGroupRow(List<TableRow> rows, int i, long totalByteSize, long rowCount, int columnChunkCount)
      {
         var row = new TableRow();
         List<TableCell> cells = new List<TableCell>();
         cells.Add(new BasicTableCell
         {
            ContentAreas = new[] { new BasicCellContent
                     {
                        Value = i.ToString()
                     }
                  }
         });
         cells.Add(new BasicTableCell
         {
            ContentAreas = new[] { new BasicCellContent
                     {
                        Value = totalByteSize.ToString()
                     }
                  }
         });
         cells.Add(new BasicTableCell
         {
            ContentAreas = new[] { new BasicCellContent
                     {
                        Value = rowCount.ToString()
                     }
                  }
         });
         cells.Add(new BasicTableCell
         {
            ContentAreas = new[] { new BasicCellContent
                     {
                        Value = columnChunkCount.ToString()
                     }
                  }
         });
         row.Cells = cells.ToArray();
         rows.Add(row);
      }

      private TableRow[] GenerateRows(Metadata viewModel)
      {
         List<TableRow> rows = new List<TableRow>();
         AddRow(rows, "Version", viewModel.Version.ToString());
         AddRow(rows, "Number of Rows", viewModel.Num_rows.ToString());
         AddRow(rows, "Created By", viewModel.Created_by.ToString());
         foreach (KeyValuePair<string, string> item in viewModel.Key_value_metadata)
         {
            AddRow(rows, item.Key, item.Value, isCustom: true);
         }
         AddRow(rows, "Number of RowGroups", viewModel.RowGroups.Count.ToString());
         return rows.ToArray();
      }

      private void AddRow(List<TableRow> rows, string key, string value, bool isCustom = false)
      {
         var row = new TableRow();
         List<TableCell> cells = new List<TableCell>();
         cells.Add(new BasicTableCell
         {
            ContentAreas = new[] { new BasicCellContent
                     {
                        Value = key,
                        ForegroundColor = isCustom ? ConsoleColor.Yellow : default(ConsoleColor?)
                     }
                  }
         });
         cells.Add(new BasicTableCell
         {

            ContentAreas = new[] { new BasicCellContent
                     {
                        Value = value,
                        ForegroundColor = isCustom ? ConsoleColor.Yellow : default(ConsoleColor?)
                     }
                  }
         });
         row.Cells = cells.ToArray();
         rows.Add(row);
      }

      private ColumnDetails[] GenerateColumns()
      {
         return new ColumnDetails[]
           {
            new ColumnDetails { columnName="key", columnWidth=20, isNullable=false, type=DataType.String },
            new ColumnDetails { columnName="value", columnWidth=90, isNullable=false, type=DataType.String },
           };
      }

      private TableRow GenerateTopLevelHeader()
      {
         var row = new TableRow();
         List<TableCell> headers = new List<TableCell>();
         
         var content = new List<ICellContent>();
         content.Add(new BasicCellContent
               {
                  Value = "Metadata Key"
               });
         headers.Add(new BasicTableCell
         {

            ContentAreas = content.ToArray()
         });
         content = new List<ICellContent>();
         content.Add(new BasicCellContent
         {
            Value = "Value"
         });
         headers.Add(new BasicTableCell
         {

            ContentAreas = content.ToArray()
         });

         row.Cells = headers.ToArray();
         return row;
      }
   }
}
