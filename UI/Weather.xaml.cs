using CommunityToolkit.Maui.Core.Platform;
using Lab6_Starter.Model;


namespace Lab6_Starter;

public partial class Weather : ContentPage
{
    public Weather()
    {
        InitializeComponent();
    }

    public void OnSearch_Clicked(object sender, EventArgs e)
    {
        _ = entry.HideKeyboardAsync(CancellationToken.None); //close the keyboard after searching

        MetarLabel.Text = Meteorologist.GetMetar(entry.Text);
        TafLabel.Text = Meteorologist.GetTaf(entry.Text);
    }

}