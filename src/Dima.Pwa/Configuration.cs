using MudBlazor;
using MudBlazor.Utilities;

namespace Dima.Pwa;

public static class Configuration
{
    public const string ApiUrlHttps = "https://localhost:7279";
    public const string HttpClientName = "dima";
    public static MudTheme Theme = new()
        {
            Typography = new()
            {
                Default = new DefaultTypography()
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            PaletteLight = new PaletteLight()
            {
              Primary = new MudColor("#1EFA2D"),
              Secondary = new MudColor("#16C623"),
          
              Background = Colors.Gray.Lighten5,
              Surface = Colors.Shades.White,
          
              AppbarBackground = new MudColor("#1EFA2D"),
              AppbarText = Colors.Shades.Black,
          
              DrawerBackground = Colors.Shades.White,
              DrawerText = Colors.Shades.Black,
          
              TextPrimary = Colors.Gray.Darken4,
              TextSecondary = Colors.Gray.Darken2,
          
              TertiaryContrastText = new MudColor("#000000"),
              
              PrimaryContrastText = Colors.Shades.Black,
          
              Divider = Colors.Gray.Lighten2,
              ActionDefault = Colors.Gray.Darken1,
              ActionDisabled = Colors.Gray.Lighten1,
            },
            PaletteDark = new PaletteDark()
            {
                Primary = new MudColor("#1EFA2D"),
                Secondary = new MudColor("#4CAF50"),

                Background = new MudColor("#121212"),
                Surface = new MudColor("#1E1E1E"),

                AppbarBackground = new MudColor("#181818"),
                AppbarText = Colors.Shades.White,

                TextPrimary = Colors.Shades.White,
                TextSecondary = Colors.Gray.Lighten1,

                DrawerBackground = new MudColor("#161616"),
                DrawerText = Colors.Shades.White,

                PrimaryContrastText = Colors.Shades.Black
            }
        };
}