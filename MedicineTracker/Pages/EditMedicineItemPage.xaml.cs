//
//  EditMedicineItemPage.cs
//  Handle Adding and Editing of Medicine Items
//
//  Created by Steven F. Daniel on 22/11/2017.
//  Copyright © 2017 GENIESOFT STUDIOS. All rights reserved.
//
using System;
using MedicineTracker.Services;
using MedicineTracker.ViewModels;
using Xamarin.Forms;

namespace MedicineTracker.Pages
{
    public partial class EditMedicineItemPage : ContentPage
    {
        // Return the binding context for our ViewModel
        EditMedicineItemPageViewModel _viewModel
        {
            get { return BindingContext as EditMedicineItemPageViewModel; }
        }

        public EditMedicineItemPage()
        {
            InitializeComponent();

            // Declare and initialise our Model Binding Context
            this.BindingContext = new EditMedicineItemPageViewModel(DependencyService.Get<INavigationService>());
            SetBinding(Page.TitleProperty, new Binding(EditMedicineItemPageViewModel.TitlePropertyName));

            //CameraButton.Clicked += CameraButton_Clicked;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)         {
            // An error occurs here...
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)             {                 TestImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });             }         } 

        private Action Save()
        {
            return async () =>
            {
                // Prompt the user with a confirmation dialog to confirm
                var alertResult = await DisplayAlert("Save Smoothie", "Proceed and save changes?", "OK", "Cancel");
                if (alertResult == true)
                {
                    // Attempt to save our medicine item
                    var saveResult = _viewModel.Save();
                    if (!saveResult)
                        // Error Saving - Must have Brand name and description
                        await DisplayAlert("Error", "Name and Description are required.", "OK");
                    else
                        // Navigate back to the Medicine Listing page
                        await _viewModel.Navigation.RemoveViewFromStack();
                }
                else
                {
                    // Navigate back to the Medicine Listing page
                    await _viewModel.Navigation.RemoveViewFromStack();
                }
            };
        }

        ToolbarItem toolbarItem;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initialize our View Model
            if (_viewModel != null)
            {
                await _viewModel.Init();
            }
            ToolbarItems.Add(toolbarItem = new ToolbarItem("Save", null, Save(), 0, 0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(toolbarItem);
        }
    }
}