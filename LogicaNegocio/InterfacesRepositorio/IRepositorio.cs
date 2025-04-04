
namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio <T>
    {
        void Add(T obj);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Remove(int id);
        void Update(T obj);
    }
}
