//
//  MedicineListPageViewModel.cs
//  Medicine List Page View Model Class

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MedicineTracker.Services;

namespace MedicineTracker.ViewModels
{
    // Declare our MedicineListItem class object
    public class SmoothieListItem
    {
        public int Id;
        public string Name { get; set; }
        public string Description { get; set; }
        //public string SideEffects { get; set; }
        //public string DateDoseTaken { get; set; }
    }

    public class MedicineListPageViewModel : BaseViewModel
    {
        // Create our MedicineList Observable Collection
        public ObservableCollection<SmoothieListItem> SmoothieList;

        // Create and declare our ViewModel class constructor
        public MedicineListPageViewModel(INavigationService navService) : base(navService)
        {
        }

        // Retrieve the Medicine items from our SQLite database
        //public void GetMedicineItems()
        public void GetSmoothies()
        {
            // Specify our List Collection to store the items being read
            //MedicineList = new ObservableCollection<MedicineListItem>();
            SmoothieList = new ObservableCollection<SmoothieListItem>();

            // Iterate through each item stored within our SQLite database
            foreach (var item in new Database.Database().GetItems())
            {
                // Add each item to our MedicineList Collection
                SmoothieList.Add(new SmoothieListItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    //SideEffects = item.SideEffects,
                    //DateDoseTaken = item.DateDoseTaken.ToString("dd-MMM-yyyy"),
                });
            }
        }

        // View Model Initialise method
        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
            });
        }
    }
}