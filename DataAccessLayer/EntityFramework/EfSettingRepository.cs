using DataAccessLayer.Abstract;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;

namespace DataAccessLayer.EntityFramework
{
    public class EfSettingRepository: GenericRepository<Setting>, ISettingDal
    {
        
    }
}