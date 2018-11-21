using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cpf.App;
using LogMagic;
using Parquet.CLI.Models;
using Parquet.CLI.Views;
using Parquet.Data;
using Parquet.Data.Rows;

namespace Parquet.CLI.Commands
{
    class MetadataCommand<TViewType> where TViewType : IDrawViews<Metadata>, new()
    {
      private readonly string _path;

      public MetadataCommand(string path)
      {
         _path = path;
      }

      internal void Execute(ViewSettings settings)
      {
         Telemetry.CommandExecuted("metadata",
            "path", _path);

         using (var time = new TimeMeasure())
         {
            using (var reader = ParquetReader.OpenFromFile(_path))
            {
               Metadata metadata = reader.Metadata;

               new TViewType().Draw(metadata, settings);
            }
         }
      }
   }
}
