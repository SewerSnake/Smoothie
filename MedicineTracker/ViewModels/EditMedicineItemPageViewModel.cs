//
//  EditMedicineItemPageViewModel.cs
//  Handle Adding or Editing of Smoothie Items

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicineTracker.Models;
using MedicineTracker.Services;
using MedicineTracker.Validation;
using Xamarin.Forms;

namespace MedicineTracker.ViewModels
{
    public class EditMedicineItemPageViewModel : BaseViewModel
    {
        IsNotNullOrEmptyRule<string> rule = new IsNotNullOrEmptyRule<string>();


        // Create and declare our ViewModel class constructor
        public EditMedicineItemPageViewModel(INavigationService navService) : base(navService)
        {
            // If we are creating a new item, we need to update the title
            //if (App.SelectedItem == null)
            if (App.SelectedSmoothie == null)
            {
                    Title = "Add Smoothie Details";
                    //App.SelectedItem = new MedicineItem();
                    //DateDoseTaken = DateTime.Now;

                    // SMOOTHIE
                    App.SelectedSmoothie = new Smoothie();
            }
            else
            {
                Title = "Edit Smoothie Details";
            }
        }

        // Checks to see if we have entered in a Brand Name
        // and Description using validation
        public bool Save()
        {
            //var brandName = App.SelectedItem.BrandName;
            //var description = App.SelectedItem.Description;
            var brandName = App.SelectedSmoothie.Name;
            var description = App.SelectedSmoothie.Description;

            //if (App.SelectedItem != null && rule.Check(brandName) && rule.Check(description))
            if (App.SelectedSmoothie != null && rule.Check(brandName) && rule.Check(description))            
            //if (App.SelectedSmoothie != null && !string.IsNullOrEmpty(App.SelectedSmoothie.Name) && !string.IsNullOrEmpty(App.SelectedSmoothie.Description))
            {
                //new Database.Database().SaveItem(App.SelectedItem);
                new Database.Database().SaveItem(App.SelectedSmoothie);

            }
            else
            {
                return false;
            }
            return true;
        }

        // Extract all fields entered within the form
        public string Name
        {
            get { return App.SelectedSmoothie.Name; }
            set
            {
                App.SelectedSmoothie.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return App.SelectedSmoothie.Description; }
            set { App.SelectedSmoothie.Description = value; OnPropertyChanged(); }
        }

        public string TestIngredient
        {
            get { return App.SelectedSmoothie.TestIngredient; }
            set { App.SelectedSmoothie.TestIngredient = value; OnPropertyChanged(); }
        }

        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
                // Check to see if we are creating a new item
                if (App.SelectedSmoothie == null)
                {
                    Name = "New";
                }
            });
        }

    }
}