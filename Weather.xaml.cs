using Lab6_Starter.Model;


 namespace Lab6_Starter;

 public partial class Weather : ContentPage
 {
     public Weather()
     {
         InitializeComponent();
     }

     public void OnSearch_Clicked (object sender, EventArgs e)
     {
         MetarLabel.Text = Meteorologist.GetMetar(entry.Text);
         TafLabel.Text = Meteorologist.GetTaf(entry.Text);
     }

 }