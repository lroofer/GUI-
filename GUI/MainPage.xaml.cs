using GUI.ViewModel;

namespace GUI;

public partial class MainPage : ContentPage
{
    MainViewModel v;
    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string current = (e.CurrentSelection.FirstOrDefault() as string);
        if (current == null) return;
        if (current == string.Empty) return;
        foreach(char u in current)
        {
            if (u < '0' || u > '9') return;
        }
        v.Rem(current);
    }
    public MainPage(MainViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;
        v = vm;
    }

	
}

