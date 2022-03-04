using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FirmManager:IFirmService
    {
        IFirmDal _firmDal;

        public FirmManager(IFirmDal firmDal)
        {
            _firmDal = firmDal;
        }

        public List<Firm> GetList()
        {
            return _firmDal.GetListAll();
        }

        public List<Firm> GetFirmById(int id)
        {
            return _firmDal.GetListAll(x => x.FirmId == id);
        }

        public List<Firm> GetFirmByMail(string mail)
        {
            return _firmDal.GetListAll(x => x.FirmMail == mail);
        }

        public void TAdd(Firm t)
        {
            _firmDal.Insert(t);
        }

        public void TDelete(Firm t)
        {
            _firmDal.Delete(t);
        }

        public Firm TGetById(int id)
        {
            return _firmDal.GetById(id);
        }

        public void TUpdate(Firm t)
        {
            _firmDal.Update(t); 
        }
    }
}
