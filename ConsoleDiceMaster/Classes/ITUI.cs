namespace DiceMasters.Classes;

public interface ITUI
{
    public void ClearScreen(List<string> lines);
    public void ClearConsoleRegion(int left, int top, int width, int height);
}