namespace Demo.App
{
    #region imports

    using Android.App;
    using Android.Content;
    using Android.Net;
    using Android.OS;
    using Android.Support.V7.App;
    using Android.Widget;

    using Com.Theartofdev.Edmodo.Cropper;

    using Java.Lang;

    #endregion

    /// <summary>
    /// Main activity for simple demo app.
    /// </summary>
    /// <seealso cref="Android.Support.V7.App.AppCompatActivity" />
    [Activity(Label = "Demo.App", MainLauncher = true, Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {
        /// <summary>
        /// The image selection button backing field.
        /// </summary>
        private Button imageSelectionButton;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="savedInstanceState">State of the saved instance.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            // Find the button and attach event.
            this.imageSelectionButton = this.FindViewById<Button>(Resource.Id.selectImageButton);
            this.imageSelectionButton.Click += this.SelectImageButtonClick;
        }

        /// <summary>
        /// Perform any final cleanup before an activity is destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            // Find the button and dettach event.
            this.imageSelectionButton.Click -= this.SelectImageButtonClick;

            base.OnDestroy();
        }

        /// <summary>
        /// Called when [OnActivityResult]
        /// </summary>
        /// <param name="requestCode">The request code.</param>
        /// <param name="resultCode">The result code.</param>
        /// <param name="data">Intent data.</param>
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == CropImage.CropImageActivityRequestCode)
            {
                CropImage.ActivityResult result = CropImage.GetActivityResult(data);

                if (resultCode == Result.Ok)
                {
                    Uri resultUri = result.Uri;
                    ImageView view = this.FindViewById<ImageView>(Resource.Id.ImageView_image);
                    view.SetImageURI(resultUri);
                }
                else if (resultCode == (Result)CropImage.CropImageActivityResultErrorCode)
                {
                    // To Do, show something about the error.
                    Exception error = result.Error;
                    Toast.MakeText(this, error.Message, ToastLength.Long);
                }
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        /// <summary>
        /// Selects the image button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SelectImageButtonClick(object sender, System.EventArgs e)
        {
            CropImage.Activity()
                .SetGuidelines(CropImageView.Guidelines.On)
                .Start(this);
        }
    }
}