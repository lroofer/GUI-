using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GUI.DataBase;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GUI.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Items = new ObservableCollection<String>();
            Tablename = "usersdb";
            Status = "[Disconnected]";
            But = "Connect";
            En = "True";
            Error = string.Empty;
        }

        [ObservableProperty]
        ObservableCollection<String> items;

        [ObservableProperty]
        string tablename;

        [ObservableProperty]
        string status;

        [ObservableProperty]
        string but;

        [ObservableProperty]
        string en;

        [ObservableProperty]
        string inname;

        [ObservableProperty]
        string inage;

        [ObservableProperty]
        string error;

        ApplicationContext db;

        void updproc()
        {
            Items.Clear();
            foreach (User u in db.Users)
            {
                Items.Add(u.Id.ToString());
                Items.Add(u.Name);
                Items.Add(u.Age.ToString());
            }
        }
        async void Update()
        {
            Error = string.Empty;
            if (Status == "[Connected]")
            {
                await Task.Run(() => Status = "[Updating...]");
                //await Task.Run(() => updproc());
                await Application.Current.Dispatcher.DispatchAsync(() =>
                {
                    updproc();
                });
                Status = "[Connected]";
            }
        }

        [RelayCommand]
        async void Clear()
        {
            Error = string.Empty;
            await Task.Run(() =>
            {
                En = "False";
                Status = "[Clearing...]";
                db.Clear();
                Status = "[Connected]";
                En = "True";
            }); 
            Update();
        }

        [RelayCommand]

        void Add()
        {
            Error = string.Empty;
            if (Inage == string.Empty || Inname == string.Empty)
            {
                Error = "Not given!";
                return;
            }
            foreach (char u in Inage)
            {
                if (u < '0' || u > '9')
                {
                    Inage = string.Empty;
                    Error = "Format error!";
                    return;
                }
            }
            User n = new User();
            n.Age = int.Parse(Inage);
            n.Name = Inname;
            n.Id = ApplicationContext.cnt++;
            db.Add(n);
            db.SaveChanges();
            Update();
            Inage = string.Empty;
            Inname = string.Empty;
        }

        [RelayCommand]
        async void Start()
        {
            await Task.Run(() =>
            {
                //En = "False";
                Status = "[Connecting... wait...]";
                db = new ApplicationContext(Tablename);
                En = "True";
            });
            Status = "[Connected]";
            But = "Reconnect";
            Update();
        }

        public void Rem(string id)
        {
            Error = string.Empty;
            int i = int.Parse(id);
            User u = db.Users.Find(i);
            db.Users.Remove(u);
            db.SaveChanges();
            Update();
        }

    }
}