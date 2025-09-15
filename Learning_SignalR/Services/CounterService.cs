namespace Learning_SignalR.Services;

public interface ICounterService
{
    public int GetCount();
    public void IncrementCount();
}

public class CounterService: ICounterService
{
    private int _currentCount = 0;
    
    public void IncrementCount()
    {
        _currentCount++;
    }
    
    public int GetCount()
    {
        return _currentCount;
    }
}