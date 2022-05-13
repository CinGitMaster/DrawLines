using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawlines2.Helpers {
	public static class DirectoryHelper {

		public static bool DoesDirectoryExistsElseCreate(string pathFolder) {
      var dirSeperatorChar = ' ';
      var retVal = false;
      if (!Directory.Exists(pathFolder)) {
        if (pathFolder.Contains(Path.AltDirectorySeparatorChar)) {
          dirSeperatorChar = Path.AltDirectorySeparatorChar;
        }
        if (pathFolder.Contains(Path.DirectorySeparatorChar)) {
          dirSeperatorChar = Path.DirectorySeparatorChar;
        }

        
        var pathPieces = pathFolder.Split(dirSeperatorChar);
        var path = "";
        foreach (var pathPiece in pathPieces) {
          path = Path.Combine(path, pathPiece);
          if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
          }
        }
        
      }
      retVal = Directory.Exists(pathFolder);
      return retVal;

    }
  }
}
