using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class SettingManager:ISettingService
    {
        private ISettingDal _settingDal;

        public SettingManager(ISettingDal settingDal)
        {
            _settingDal = settingDal;
        }

        public void TAdd(Setting t)
        {
           _settingDal.Insert(t);
        }

        public void TDelete(Setting t)
        {
            _settingDal.Delete(t);
        }

        public void TUpdate(Setting t)
        {
            _settingDal.Update(t);
        }

        public List<Setting> GetList()
        {
            return _settingDal.GetListAll();
        }

        public Setting TGetById(int id)
        {
            return _settingDal.GetById(id);
        }
    }
}