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

        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                UploadedOrSelectedImage.Source = photo?.FullPath;
                var fileInfo = new FileInfo(photo?.FullPath);
                var fileLength = fileInfo.Length;
                FileSizeLabel.Text = $"Image size: {fileLength / 1000} kB";

            }
        }

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

