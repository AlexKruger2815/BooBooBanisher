using BBB_Desktop.Data;
using BBB_Desktop.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BBB_Desktop.ViewModels;

public class StatsViewModel : INotifyPropertyChanged
{
    public ObservableCollection<StatsModel> Stats { get; } = new();

    
    public int Successes 
    {
        get { return _successes; } 
        private set 
        {  
            _successes = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Successes"));
        }
    }
    public int Failures
    {
        get { return _failures; }
        private set
        {
            _failures = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Failures"));
        }
    }

    private int _successes = 0;
    private int _failures = 0;

    public event PropertyChangedEventHandler? PropertyChanged;

    public StatsViewModel()
    {
        
    }

    public async Task LoadAsync(DateTime? start = null, DateTime? end = null)
    {
        Stats.Clear();
        var stats = await BBBDBContext.GetStatsAsync(
                (int)App.Current.Properties["userID"]!,
                (string)App.Current.Properties["access_token"]!, 
                start, end
            );
        foreach ( var stat in stats ) 
        {
            Stats.Add(stat);
        }
        Successes = Stats.Where(stat => stat.category == "Success").Count();
        Failures = Stats.Where(stat => stat.category == "Failure").Count();
    }
}
