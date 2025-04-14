
namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio <T>
    {
        void Add(T obj);
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetByEmail(string correo);
        void Remove(int id);
        void Update(int id, T obj);
    }
}
