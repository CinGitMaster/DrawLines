using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Drawlines2.Services;

namespace Drawlines2.ViewModels
{
  [INotifyPropertyChanged]
  public partial class PlateTotalSizeViewModel {

    private string _plateYLength;
    public string PlateYLength {
      get => _plateYLength;
      set {
        _plateYLength = value;
        SetProperty(ref _plateYLength, value);
        OnPropertyChanged(nameof(DisplayPlateSize));
      }
    }

    private string _plateWidth;
    public string PlateWidth { get => _plateWidth;
      set {
        _plateWidth = value;
        SetProperty(ref _plateWidth, value);
        OnPropertyChanged(nameof(DisplayPlateSize));
      }
    }

    private ServicesCalculateRemainder serviceCalcRemainder = null;

    public PlateTotalSizeViewModel() {
      serviceCalcRemainder = Ioc.Default.GetService<ServicesCalculateRemainder>();
    }


    public string DisplayPlateSize { 
      get {
        var retVal = "";
        if (serviceCalcRemainder != null) {
          serviceCalcRemainder.UpdateTotalPlateSize(this.PlateYLength, this.PlateWidth);
        }
        if( !string.IsNullOrEmpty(PlateYLength) && !string.IsNullOrEmpty(PlateWidth)) {
          retVal = $"{PlateYLength} x {PlateWidth}";
        }
        return retVal;
      }
    }


  }
}
