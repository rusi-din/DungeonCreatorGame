public class MapData
{
    public int x { get; set; }
    public int y { get; set; }
    public string startPoint { get; set; }
    public string endPoint { get; set; }
    public string[] route { get; set; }
    public int[,] nodeTags { get; set; }

    private static MapData instance;

    private MapData()
    {
    }

    public static MapData getInstance()
    {
        if (instance == null)
        {
            instance = new MapData();
        }
        return instance;
    }

    public void setData(int x, int y, string startPoint, string endPoint, string[] route)
    {
        this.x = x;
        this.y = y;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.route = route;
    }

    public void clearData()
    {
        x = 0;
        y = 0;
        startPoint = null;
        endPoint = null;
        route = null;
    }
}