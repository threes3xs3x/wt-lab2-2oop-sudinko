namespace WtLab2.Interfaces
{
    public interface IItemService<T>
    {
        void Add(T item);
        List<T> GetAll();
        T? GetById(int id);
    }
}