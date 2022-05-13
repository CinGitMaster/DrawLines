using Drawlines2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawlines2.Services
{
    public class ServicesCalculateRemainder{

		public Pieces Pieces;
		// ctor
		public ServicesCalculateRemainder() {

		}

		public void Init() {
			if (Pieces == null) {
				Pieces = new Pieces();
				for (var i = 0; i < 10; i++) {
					var PiecePlate = new PiecePlate();
					Pieces.ListPieces.Add(PiecePlate);
				}
			}
		}

		public bool UpdatePiecePlateInList(int PieceNr, PiecePlate piece) {
			var retVal = true;
			if (PieceNr > 10) {
				// error
				retVal = false;
			}
			else {				
				Pieces.ListPieces[PieceNr - 1] = piece;

			}
			return retVal;
		}


		public PlateTotalSize PlateTotalSize = new PlateTotalSize();


		public bool UpdateTotalPlateSize(string PlateLengthYDirection, string PlateSizeWidth ) {
			var retVal = false;
			int PlateYLength = 0;
			int PlateWidth = 0;
			Int32.TryParse(PlateLengthYDirection, out PlateYLength); PlateTotalSize.PlateYLength = PlateYLength;
			Int32.TryParse(PlateSizeWidth, out PlateWidth); PlateTotalSize.PlateWidth = PlateWidth;

			retVal = true;
			return retVal;
		}

    public int GetCalculateRemainder() {
			var remainder = PlateTotalSize.PlateWidth;
			var to_use = 0;
			for (var i = 0; i < 10; i++) {
				var PiecePlate = Pieces.ListPieces[i];
				to_use = to_use + PiecePlate.Width * PiecePlate.Amount;
			}
			remainder = remainder - to_use;
			return remainder;
		  }
    }
}
