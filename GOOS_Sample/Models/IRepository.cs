using GOOS_Sample.Models.DataModels;

namespace GOOS_Sample.Models
{
    public interface IRepository<T>
    {
        void Save(T Entity);
    }
}