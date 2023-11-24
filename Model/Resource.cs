using System;
 using System.ComponentModel;

 namespace Lab6_Starter.Model;

 [Serializable()]
 public class Resource : INotifyPropertyChanged
 {
     string link;
     string name;

     public Resource(string link, string name) 
     {
         this.link = link;
         this.name = name;
     }

     public String Link
     {
         get { return link; }
         set
         {
             link = value;
             OnPropertyChanged(nameof(Link));
         }
     }
     public string Name
     {
         get { return name; }
         set
         {
             name = value;
             OnPropertyChanged(nameof(name));
         }
     }
     public event PropertyChangedEventHandler PropertyChanged;

     protected virtual void OnPropertyChanged(string propertyName)
     {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
     }
 }