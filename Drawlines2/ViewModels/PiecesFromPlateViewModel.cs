using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Drawlines2.Models;
using Drawlines2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawlines2.ViewModels
{

	[INotifyPropertyChanged]
	public partial class PiecesFromPlateViewModel {


		public void RecalculateRemainder(int piecenr) {
			string width = "";
			string amount = "";
			switch (piecenr) {

				case 1:
					width = Piece1Width;
					amount = Piece1Amount;
					break;
				case 2:
					width = Piece2Width;
					amount = Piece2Amount;
					break;
				case 3:
					width = Piece3Width;
					amount = Piece3Amount;
					break;
				case 4:
					width = Piece4Width;
					amount = Piece4Amount;
					break;
				case 5:
					width = Piece5Width;
					amount = Piece5Amount;
					break;

				case 6:
					width = Piece6Width;
					amount = Piece6Amount;
					break;
				case 7:
					width = Piece7Width;
					amount = Piece7Amount;
					break;
				case 8:
					width = Piece8Width;
					amount = Piece8Amount;
					break;
				case 9:
					width = Piece9Width;
					amount = Piece9Amount;
					break;
				case 10:
					width = Piece10Width;
					amount = Piece10Amount;
					break;


				default:
					break;
			}
			if (width != null && amount != null) {
				var piece = PiecePlate.NewPiecePlate(width, amount);
				serviceCalcRemainder.UpdatePiecePlateInList(piecenr, piece); 
				_remainder = serviceCalcRemainder.GetCalculateRemainder();
				EnableBtnGenerateMultiDinFile = (_remainder != null) && (_remainder >= 0);
			}
		}

		private string _piece1Width;
		public string Piece1Width {
			get => _piece1Width;
			set {
				_piece1Width = value;
				SetProperty(ref _piece1Width, value);
				RecalculateRemainder(piecenr: 1);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece1Amount;
		public string Piece1Amount {
			get => _piece1Amount;  
			set {
				_piece1Amount = value;
				SetProperty(ref _piece1Amount, value);
				RecalculateRemainder(piecenr: 1);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece2Width;
		public string Piece2Width {
			get => _piece2Width;
			set {
				_piece2Width = value;
				SetProperty(ref _piece2Width, value);
				RecalculateRemainder(piecenr: 2);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece2Amount;
		public string Piece2Amount {
			get => _piece2Amount;
			set {
				_piece2Amount = value;
				SetProperty(ref _piece2Amount, value);
				RecalculateRemainder(piecenr: 2);
				OnPropertyChanged(nameof(Remainder));
			}
		}


		private string _piece3Width;
		public string Piece3Width {
			get => _piece3Width;
			set {
				_piece3Width = value;
				SetProperty(ref _piece3Width, value);
				RecalculateRemainder(piecenr: 3);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece3Amount;
		public string Piece3Amount {
			get => _piece3Amount;
			set {
				_piece3Amount = value;
				SetProperty(ref _piece3Amount, value);
				RecalculateRemainder(piecenr: 3);
				OnPropertyChanged(nameof(Remainder));
			}
		}


		private string _piece4Width;
		public string Piece4Width {
			get => _piece4Width;
			set {
				_piece4Width = value;
				SetProperty(ref _piece4Width, value);
				RecalculateRemainder(piecenr: 4);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece4Amount;
		public string Piece4Amount {
			get => _piece4Amount;
			set {
				_piece4Amount = value;
				SetProperty(ref _piece4Amount, value);
				RecalculateRemainder(piecenr: 4);
				OnPropertyChanged(nameof(Remainder));
			}
		}




		private string _piece5Width;
		public string Piece5Width {
			get => _piece5Width;
			set {
				_piece5Width = value;
				SetProperty(ref _piece5Width, value);
				RecalculateRemainder(piecenr: 5);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece5Amount;
		public string Piece5Amount {
			get => _piece5Amount;
			set {
				_piece5Amount = value;
				SetProperty(ref _piece5Amount, value);
				RecalculateRemainder(piecenr: 5);
				OnPropertyChanged(nameof(Remainder));
			}
		}




		private string _piece6Width;
		public string Piece6Width {
			get => _piece6Width;
			set {
				_piece6Width = value;
				SetProperty(ref _piece6Width, value);
				RecalculateRemainder(piecenr: 6);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece6Amount;
		public string Piece6Amount {
			get => _piece6Amount;
			set {
				_piece6Amount = value;
				SetProperty(ref _piece6Amount, value);
				RecalculateRemainder(piecenr: 6);
				OnPropertyChanged(nameof(Remainder));
			}
		}



		private string _piece7Width;
		public string Piece7Width {
			get => _piece7Width;
			set {
				_piece7Width = value;
				SetProperty(ref _piece7Width, value);
				RecalculateRemainder(piecenr: 7);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece7Amount;
		public string Piece7Amount {
			get => _piece7Amount;
			set {
				_piece7Amount = value;
				SetProperty(ref _piece7Amount, value);
				RecalculateRemainder(piecenr: 7);
				OnPropertyChanged(nameof(Remainder));
			}
		}


		private string _piece8Width;
		public string Piece8Width {
			get => _piece8Width;
			set {
				_piece8Width = value;
				SetProperty(ref _piece8Width, value);
				RecalculateRemainder(piecenr: 8);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece8Amount;
		public string Piece8Amount {
			get => _piece8Amount;
			set {
				_piece8Amount = value;
				SetProperty(ref _piece8Amount, value);
				RecalculateRemainder(piecenr: 8);
				OnPropertyChanged(nameof(Remainder));
			}
		}


		private string _piece9Width;
		public string Piece9Width {
			get => _piece9Width;
			set {
				_piece9Width = value;
				SetProperty(ref _piece9Width, value);
				RecalculateRemainder(piecenr: 9);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece9Amount;
		public string Piece9Amount {
			get => _piece9Amount;
			set {
				_piece9Amount = value;
				SetProperty(ref _piece9Amount, value);
				RecalculateRemainder(piecenr: 9);
				OnPropertyChanged(nameof(Remainder));
			}
		}



		private string _piece10Width;
		public string Piece10Width {
			get => _piece10Width;
			set {
				_piece10Width = value;
				SetProperty(ref _piece10Width, value);
				RecalculateRemainder(piecenr: 10);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		private string _piece10Amount;
		public string Piece10Amount {
			get => _piece10Amount;
			set {
				_piece10Amount = value;
				SetProperty(ref _piece10Amount, value);
				RecalculateRemainder(piecenr: 10);
				OnPropertyChanged(nameof(Remainder));
			}
		}

		[ObservableProperty]
		private bool _EnableBtnGenerateMultiDinFile;


		private int _remainder = 0;
		public string Remainder {
			get {
				return _remainder.ToString();
			}
		}

		private ServicesCalculateRemainder serviceCalcRemainder = null;
		
		public PiecesFromPlateViewModel() {
			serviceCalcRemainder = Ioc.Default.GetService<ServicesCalculateRemainder>();

		}
	}
}
