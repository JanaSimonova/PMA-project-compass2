using MauiPopup.Views;

namespace Pololetni_projekt;

public partial class PopupPage : BasePopupPage
{
	public PopupPage()
	{
		InitializeComponent();
        //Task.Delay(5000);
        //Vibration.Default.Vibrate(500);
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

    }

    private async void Btn_Start(object sender, EventArgs e)
    {
        string originalText = btn_start.Text;
        btn_start.Text = "Calibrating...";
        btn_start.IsEnabled= false;
        // Wait for 5 seconds
        await Task.Delay(5000);
        btn_start.IsEnabled=true;
        btn_start.Text=originalText;

        // Vibrate for 0.5 seconds
        Vibration.Vibrate(500);
        MauiPopup.PopupAction.ClosePopup();
    }

    /*
    private void Btn_Done(object sender, EventArgs e)
    {
        
    }*/
}