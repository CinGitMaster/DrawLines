using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawlines2.Models
{
  public class PiecePlate{
		public int Width { get; set; } = 0;
		public int Amount { get; set; } = 0;

		public int getAmountMillimeters() {
			var retVal = 0;
			return retVal;
		}

		public int getWidthMillimeters() {
			var retVal = 0;
			return retVal;
		}

		


		public static PiecePlate NewPiecePlate(string Width, string Amount) {

			var width_int = 0; Int32.TryParse(Width,out width_int);
			var amount_int = 0;  Int32.TryParse(Amount, out amount_int);
			var piece = new PiecePlate() { Width = width_int, Amount = amount_int };
			return piece;
		}
	}
}
