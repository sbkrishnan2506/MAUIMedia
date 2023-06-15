using Microsoft.Maui.Devices;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace MauiMedia;

public partial class MainPage : ContentPage
{
	int count = 0;
    private int selectedCompressionQuality;
    public MainPage()
	{
		InitializeComponent();
	}

    private async void mauiMediaBtnClicked(object sender, EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    //string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    



                    if (DeviceInfo.Platform == DevicePlatform.iOS)
                    {

                        // save the file into local storage
                        string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                        using Stream sourceStream = await photo.OpenReadAsync();
                        using FileStream localFileStream = File.OpenWrite(localFilePath);

                        await sourceStream.CopyToAsync(localFileStream);
                        UploadedOrSelectedImage.Source = localFilePath;

                        var fileInfo = new FileInfo(localFilePath);
                        var fileLength = fileInfo.Length;
                        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";
                    }
                    else
                    {
                        UploadedOrSelectedImage.Source = photo?.FullPath;
                        var fileInfo = new FileInfo(photo?.FullPath);
                        var fileLength = fileInfo.Length;
                        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";
                    }




                    //Microsoft.Maui.Controls.ImageSource imagesource;
                    //if (DeviceInfo.Platform = DevicePlatform.iOS)
                    //{
                    //    var localpath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                    //    photo.OpenReadAsync();
                    //}
                    //else
                    //{
                    //    imagesource = photo?.FullPath;
                    //    //UploadedOrSelectedImage.Source = photo?.FullPath;
                    //}
                    //UploadedOrSelectedImage.Source = localFilePath;

                    

                }
            }
        }
        catch(Exception ex)
        {
            await DisplayAlert("Alert", ex.Message, "OK");
        }

    }
    private async void SelectMauiMediaBtnClicked(object sender, EventArgs e)
    {

        var result = await MediaPicker.Default.PickPhotoAsync();
        if (result is null) return;

        UploadedOrSelectedImage.Source = result?.FullPath;

        var fileInfo = new FileInfo(result?.FullPath);
        var fileLength = fileInfo.Length;

        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";

    }
    private async void OnCounterClicked(object sender, EventArgs e)
	{

        var options = new StoreCameraMediaOptions { CompressionQuality = selectedCompressionQuality };
        var result = await CrossMedia.Current.TakePhotoAsync(options);
        if (result is null) return;

        UploadedOrSelectedImage.Source = result?.Path;

        var fileInfo = new FileInfo(result?.Path);
        var fileLength = fileInfo.Length;

        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";

    }
    private async void SelectBtnClicked(object sender, EventArgs e)
    {

        var result = await CrossMedia.Current.PickPhotoAsync();
        if (result is null) return;

        UploadedOrSelectedImage.Source = result?.Path;

        var fileInfo = new FileInfo(result?.Path);
        var fileLength = fileInfo.Length;

        FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";

    }
}

