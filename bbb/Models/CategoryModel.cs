namespace bbb.Models;
public class CategoryModel
{
    public int categoryID { get; set; }
    public string categoryType { get; set; }

    override public string ToString(){
        return $"{categoryID}: {categoryType}";
    }
}
