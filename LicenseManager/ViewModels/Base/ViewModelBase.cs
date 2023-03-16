using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LicenseManager.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
