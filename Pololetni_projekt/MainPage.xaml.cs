using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Pololetni_projekt.Services;
using Pololetni_projekt.Models;


namespace Pololetni_projekt;


[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage : ContentPage
{


	public MainPage()
	{
		InitializeComponent();
        MauiPopup.PopupAction.DisplayPopup(new PopupPage());

        ToggleCompass();
        GetCurrentLocation();

    }

    private void ToggleCompass()
    {
        if (Compass.Default.IsSupported)
        {
            // Turn on compass
            Compass.Default.ReadingChanged += Compass_ReadingChanged;
            Compass.Default.Start(SensorSpeed.UI);

        }
        else
            Direction.Text = "Your device does not have default compass";
    }

    private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
    {
        // Update UI Label with compass state
        //CompassLabel.Text = Math.Round($"Compass: {e.Reading}");

        decimal roundedReading = decimal.Round((decimal)e.Reading.HeadingMagneticNorth, 0); // Zaokrouhlení na dvě desetinná místa

        CompassLabel.Text = Convert.ToString( roundedReading)+"°";
        compass.BindingContext = -roundedReading; 

        // Direction conditions
        
        if(roundedReading > 337 || roundedReading <= 22 )
        {
            Direction.Text = "North";
            // border.Background= Color.FromArgb("#61d6fa");
        }
        if (roundedReading > 22 && roundedReading <= 67)
        {
            Direction.Text = "North-East";
            // border.Background = Color.FromArgb("8cf5e2");
        }
        if (roundedReading > 67 && roundedReading <= 112)
        {
            Direction.Text = "East";
            // border.Background = Color.FromRgba("73de77");
        }
        if(roundedReading > 112 && roundedReading <= 157)
        {
            Direction.Text = "South-East";
            // border.Background = Color.FromRgba("d7fa6e");
        }
        if(roundedReading > 157 && roundedReading <= 202 )
        {
            Direction.Text = "South";
            // border.Background = Color.FromRgba("fff769");
        }
        if (roundedReading > 202 && roundedReading <= 247)
        {
            Direction.Text = "South-West";
            // border.Background = Color.FromArgb("#ffb587");
        }
        if (roundedReading > 247 && roundedReading <= 292)
        {
            Direction.Text = "West";
            // border.Background = Color.FromArgb("#fc9090");
        }
        if (roundedReading > 292 && roundedReading <= 337)
        {
            Direction.Text = "North-West";
            // border.Background = Color.FromArgb("#ffb3fb");
        }


    }

    private void Calibrate(object sender, EventArgs e)
    {
        MauiPopup.PopupAction.DisplayPopup(new PopupPage());
    }


    // (konci moje chapani docela ) -- zkopirovano z microsoft, mata
    // poloha

    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;

    double latitude;
    double longitude;

    public async Task GetCurrentLocation()
    {
        try
        {
            _isCheckingLocation = true;
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
            _cancelTokenSource = new CancellationTokenSource();
            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);
            if (location != null)
            {
                latitude = location.Latitude;
                longitude= location.Longitude;
                LocationLabel.Text =  $"Location: lat: {latitude}, long: {longitude}";
            }
        }
        // Catch one of the following exceptions:
        //   FeatureNotSupportedException
        //   FeatureNotEnabledException
        //   PermissionException
        catch (Exception ex)
        {
            // Unable to get location
        }
        finally
        {
            _isCheckingLocation = false;
        }


        // -- Work with API
        var result = await ApiService.GetWeather(latitude, longitude);
        
        temperature.Text = Convert.ToString(result.main.temperature) + "°C";
        city.Text = result.name;
        pocasi.Text = result.weather[0].description;
        // ## icon ## 
        // - 1.method
        // image.Source = result.weather[0].fullIconUrl;
        // - 2.method
        string ico = result.weather[0].icon;
        image.Source = $"https://openweathermap.org/img/wn/{ico}@2x.png";

    }
    // konec polohy
}


