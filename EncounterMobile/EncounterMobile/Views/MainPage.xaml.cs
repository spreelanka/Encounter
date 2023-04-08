namespace EncounterMobile.Views;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
		
	}

    public void OnCollectionViewRemainingItemsThresholdReached(object sender, EventArgs e)
    {
        var s = sender.ToString();
        // Retrieve more data here and add it to the CollectionView's ItemsSource collection.
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
    //	count++;

    //	if (count == 1)
    //		CounterBtn.Text = $"Clicked {count} time";
    //	else
    //		CounterBtn.Text = $"Clicked {count} times";

    //	SemanticScreenReader.Announce(CounterBtn.Text);

    //}
}


