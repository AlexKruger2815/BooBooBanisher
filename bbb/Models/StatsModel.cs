namespace bbb.Models;

public class StatsModel
{
    //"category"	"content"	"createdat"
    public required string category { get; set; }
    public required string content { get; set; }
    public DateTime createdAt { get; set; }
    public override string ToString()
    {
        return $"cat: {category} cont: {content} time: {createdAt}";
    }
}