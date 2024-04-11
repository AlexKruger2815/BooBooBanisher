using BBB_Desktop.Data;
using BBB_Desktop.Models;
using System.Collections.ObjectModel;

namespace BBB_Desktop.ViewModels;

public class StatsViewModel
{
    public ObservableCollection<StatsModel> Stats { get; } = new();

    public StatsViewModel()
    {
        
    }

    public async Task LoadAsync(DateTime? start = null, DateTime? end = null)
    {
        var stats = await BBBDBContext.GetStatsAsync(
                (int)App.Current.Properties["userID"]!,
                (string)App.Current.Properties["access_token"]!, 
                start, end
            );
        foreach ( var stat in stats ) 
        {
            Stats.Add(stat);
        }
    }
}
