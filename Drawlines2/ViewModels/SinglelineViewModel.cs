using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using Drawlines2.Models;
using Drawlines2.Services;
using System;

namespace Drawlines2.ViewModels
{
  [INotifyPropertyChanged]
  public partial class SinglelineViewModel {

    private string _YLength;
    public string YLength {
      get => _YLength;
      set {
        _YLength = value;
        SetProperty(ref _YLength, value);
        UpdateSingleLine(this);
      }
    }

    private string _XLength;
    public string XLength { get => _XLength;
      set {
        _XLength = value;
        SetProperty(ref _XLength, value);
        UpdateSingleLine(this);
      }
    }

    public Singleline SingleLine = null;

    public void UpdateSingleLine(SinglelineViewModel SingleLineVM) {
      var XLength = 0;
      var YLength = 0;

      if(!String.IsNullOrEmpty(SingleLineVM.XLength)) {
        Int32.TryParse(SingleLineVM.XLength, out XLength);
      }
      SingleLine.XLength = XLength;

      if (!String.IsNullOrEmpty(SingleLineVM.YLength)) {
        Int32.TryParse(SingleLineVM.YLength, out YLength);
      }
      SingleLine.YLength = YLength;
    }

    private ServiceGenerate_GCodeDrawLines serviceGenerate_GCodeDrawLines = null;

    public SinglelineViewModel() {
      SingleLine = new Singleline();

      serviceGenerate_GCodeDrawLines = Ioc.Default.GetService<ServiceGenerate_GCodeDrawLines>();
    }
    

  }
}
