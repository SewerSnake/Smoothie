//
//  EditMedicineItemPageViewModel.cs
//  Handle Adding or Editing of Smoothie Items

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MedicineTracker.Models;
using MedicineTracker.Services;
using MedicineTracker.Validation;

namespace MedicineTracker.ViewModels
{
    public class EditMedicineItemPageViewModel : BaseViewModel
    {
        IsNotNullOrEmptyRule<string> rule = new IsNotNullOrEmptyRule<string>();


        // Create and declare our ViewModel class constructor
        public EditMedicineItemPageViewModel(INavigationService navService) : base(navService)
        {
            // If we are creating a new item, we need to update the title
            if (App.SelectedItem == null)
            {
                Title = "Add Smoothie Details";
                App.SelectedItem = new MedicineItem();
                DateDoseTaken = DateTime.Now;
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
            var brandName = App.SelectedItem.BrandName;
            var description = App.SelectedItem.Description;

            if (App.SelectedItem != null && rule.Check(brandName) && rule.Check(description))
            {
                new Database.Database().SaveItem(App.SelectedItem);
            }
            else
            {
                return false;
            }
            return true;
        }

        // Extract all fields entered within the form
        public string BrandName
        {
            get { return App.SelectedItem.BrandName; }
            set
            {
                App.SelectedItem.BrandName = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return App.SelectedItem.Description; }
            set { App.SelectedItem.Description = value; OnPropertyChanged(); }
        }

        public string SideEffects
        {
            get { return App.SelectedItem.SideEffects; }
            set { App.SelectedItem.SideEffects = value; OnPropertyChanged(); }
        }

        public string Dosage
        {
            get { return App.SelectedItem.Dosage; }
            set { App.SelectedItem.Dosage = value; OnPropertyChanged(); }
        }

        public DateTime DateDoseTaken
        {
            get { return App.SelectedItem.DateDoseTaken; }
            set { App.SelectedItem.DateDoseTaken = value; OnPropertyChanged(); }
        }

        public TimeSpan TimeDoseTaken
        {
            get { return App.SelectedItem.TimeDoseTaken; }
            set { App.SelectedItem.TimeDoseTaken = value; OnPropertyChanged(); }
        }

        public override async Task Init()
        {
            await Task.Factory.StartNew(() =>
            {
                // Check to see if we are creating a new item
                if (App.SelectedItem == null)
                {
                    BrandName = "New";
                    Dosage = "1 per day";
                }
            });
        }
    }
}