using System.Runtime.CompilerServices;
using LicenseManager.DTOs;

namespace LicenseManager.ViewModels
{
    public class ViewModelBaseDTO<T> : ViewModelBase where T : class, IBaseDTO
    {
        public ViewModelBaseDTO(T item)
        {
            Item = item;
        }

        protected override void OnPropertyChanged([CallerMemberName] string name = null)
        {
            Modified = true;
            base.OnPropertyChanged(name);
        }

        public T Item { get; set; }
        public bool Modified { get; private set; }
    }
}
