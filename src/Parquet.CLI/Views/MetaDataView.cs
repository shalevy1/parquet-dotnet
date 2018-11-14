using System;
using System.Collections.Generic;
using System.Text;
using Parquet.CLI.Models;
using Parquet.Data;

namespace Parquet.CLI.Views
{
   public class MetadataView : IDrawViews<Metadata>
   {
      public void Draw(Metadata viewModel, ViewSettings settings)
      {
         Console.WriteLine($"Version: {viewModel.Version}");
      }
   }
}
