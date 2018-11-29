//
//  BaseViewModel.cs
//  BaseView Model Class

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MedicineTracker.Services;

namespace MedicineTracker.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigationService Navigation { get; set; }

        public const string TitlePropertyName = "Title";

        string title;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }
        protected BaseViewModel(INavigationService navService)
        {
            Navigation = navService;
        }

        public abstract Task Init();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public abstract class BaseViewModel<TParam> : BaseViewModel
    {
        protected BaseViewModel(INavigationService navService) : base(navService)
        {
        }
        public override async Task Init()
        {
            await Init(default(TParam));
        }
        public abstract Task Init(TParam medicineDetails);
    }
}