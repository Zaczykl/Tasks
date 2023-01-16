namespace Tasks.Persistence.Observer
{
    public interface IObserver
    {
        void OnUpdate(string name = null);
    }
}
