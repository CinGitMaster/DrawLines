using Drawlines2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawlines2.Helpers;

namespace Drawlines2.Services {
	public class ServiceGenerate_GCodeDrawLines{


    public async Task<(bool, String)> GenerateoMultiplePieces_DinFile(string pathFolder, int widthMillimeters, int yDirectionMillimeters, List<PiecePlate> pieces) {
      var retVal = false;
      var dinFile = "";
      StringBuilder sb = new StringBuilder();

      sb.AppendLine("%1 (export_lines.din)");
      sb.AppendLine("(Post processor: Cinnova-LinePost.scpost)");
      sb.AppendLine("G22 L9800            (Update data)");
      sb.AppendLine("M514");
      sb.AppendLine("(WJ0.8 normal)");
      sb.AppendLine("G61");
      sb.AppendLine("G41 R=P1500 E0");
      sb.AppendLine("G01 F=P1501");

      // Boolean to indicate whether we are moving up and down or down and up to minimize movements.
      bool positiveYDirection = true;
      int currentWidth = 0;
      foreach (var piece in pieces) {
        for (int i = 0; i < piece.Amount; i++) {
          if(piece.Width == 0) {
            continue;
					}
          if (piece.Amount == 0) {
            continue;
          }
          currentWidth += piece.Width;
          var y_dir = positiveYDirection ? 0 : yDirectionMillimeters;
          sb.Append($"G00 X{currentWidth} Y{y_dir}\n");
          sb.Append("M14\n");
          y_dir = positiveYDirection ? yDirectionMillimeters : 0;
          sb.Append($"G01 X{currentWidth} Y{y_dir}\n");
          sb.Append("M15\n");
          positiveYDirection = !positiveYDirection;
        }
      }
      sb.Append("G99\n");

      dinFile = Path.Combine(pathFolder, "export_lines.din");
      try {
        using StreamWriter filewriter = new(dinFile);
        await filewriter.WriteLineAsync(sb.ToString());
        filewriter.Close();
        filewriter.Dispose();
        retVal = System.IO.File.Exists(dinFile);
      }
      catch (Exception ex) {
        retVal = false;      
      }
      return (retVal, dinFile);
    }


    public async Task<(bool, String)> Generate_Singleline_YLength_DinFile(string pathFolder, int YDirectionMillimeters) {
      var retVal = false;
      var dinFile = "";
      StringBuilder sb = new StringBuilder();

      sb.AppendLine("%1 (singleline_ylength.din)");
      sb.AppendLine("(Post processor: Cinnova-LinePost.scpost)");
      sb.AppendLine("G22 L9800            (Update data)");
      sb.AppendLine("M514");
      sb.AppendLine("(WJ0.8 normal)");
      sb.AppendLine("G61");
      sb.AppendLine("G41 R=P1500 E0");
      sb.AppendLine("G01 F=P1501");

      sb.Append($"G00 X{0} Y{0}\n");
      sb.Append("M14\n");
      sb.Append($"G01 X{0} Y{YDirectionMillimeters}\n");
      sb.Append("M15\n");
      sb.Append("G99\n");

      dinFile = Path.Combine(pathFolder, "singleline_ylength.din");
      if (DirectoryHelper.DoesDirectoryExistsElseCreate(pathFolder)) {
        try {
          using StreamWriter filewriter = new(dinFile);
          await filewriter.WriteLineAsync(sb.ToString());
          filewriter.Close();
          filewriter.Dispose();
          retVal = System.IO.File.Exists(dinFile);
        }
        catch (Exception ex) {
          retVal = false;
				}
      }
      return (retVal, dinFile);
    }

    public async Task<(bool, String)> Generate_Singleline_XLength_DinFile(string pathFolder, int XDirectionMillimeters) {
      var retVal = false;
      var dinFile = "";
      StringBuilder sb = new StringBuilder();

      sb.AppendLine("%1 (singleline_xlength.din)");
      sb.AppendLine("(Post processor: Cinnova-LinePost.scpost)");
      sb.AppendLine("G22 L9800            (Update data)");
      sb.AppendLine("M514");
      sb.AppendLine("(WJ0.8 normal)");
      sb.AppendLine("G61");
      sb.AppendLine("G41 R=P1500 E0");
      sb.AppendLine("G01 F=P1501");

      sb.Append($"G00 X{0} Y{0}\n");
      sb.Append("M14\n");
      sb.Append($"G01 X{XDirectionMillimeters} Y{0}\n");
      sb.Append("M15\n");
      sb.Append("G99\n");

      dinFile = Path.Combine(pathFolder, "singleline_ylength.din");
      if (DirectoryHelper.DoesDirectoryExistsElseCreate(pathFolder)) {
        try {
          using StreamWriter filewriter = new(dinFile);
          await filewriter.WriteLineAsync(sb.ToString());
          filewriter.Close();
          filewriter.Dispose();
          retVal = System.IO.File.Exists(dinFile);
        }
        catch (Exception ex) {
          retVal = false;
        }
      }
      return (retVal, dinFile);
    }
  }

}
