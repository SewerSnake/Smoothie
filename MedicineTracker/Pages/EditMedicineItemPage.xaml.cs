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
        const string SAVETITLE = "Save Smoothie";
        const string SAVEPROMPT = "Proceed and save changes?";
        const string OKBUTTONTITLE = "OK";
        const string CANCELBUTTONTITLE = "Cancel";

        const string ERRORTITLE = "Error";
        const string ERRORPROMPT = "Name and Description are required.";

        const string SAVEBUTTONTITLE = "Save";


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
            // An error occurs here... Because of lacking Xam.Plugin.Media in
            // MedicineTracker folder?
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)             {                 TestImage.Source = ImageSource.FromStream(() => {
                    return photo.GetStream(); 
                });             }         }

        private Action Save()
        {
            return async () =>
            {
                // Prompt the user with a confirmation dialog to confirm
                var alertResult = await DisplayAlert(SAVETITLE,
                                                     SAVEPROMPT,
                                                     OKBUTTONTITLE,
                                                     CANCELBUTTONTITLE);

                if (alertResult == true)
                {
                    // Attempt to save our smoothie item
                    var saveResult = _viewModel.Save();
                    if (!saveResult)
                        // Error Saving - Must have Brand name and description
                        await DisplayAlert(ERRORTITLE, 
                                           ERRORPROMPT, 
                                           OKBUTTONTITLE);
                    else
                        // Navigate back to the Smoothie Listing page
                        await _viewModel.Navigation.RemoveViewFromStack();
                }
                else
                {
                    // Navigate back to the Smoothie Listing page
                    await _viewModel.Navigation.RemoveViewFromStack();
                }
            };
        }

        // MILJA START

        private void AddRow_Clicked(object sender, EventArgs args)
        {
            // Let ViewModel know that more rows are needed.
        }

        private void GetNutrition_Clicked(object sender, EventArgs args)
        {
            // 1. Get Inserted ingredients and amounts
            //      - Add all to Smoothie ingredients array
            

            // 2. Fetch nutrition for each item

            // 3. Calculate total nutrition value
        }

        // MILJA END

        ToolbarItem toolbarItem;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Initialize our View Model
            if (_viewModel != null)
            {
                await _viewModel.Init();
            }
            ToolbarItems.Add(toolbarItem = new ToolbarItem(SAVEBUTTONTITLE,
                                                           null, 
                                                           Save(), 
                                                           0, 
                                                           0));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ToolbarItems.Remove(toolbarItem);
        }
    }
}