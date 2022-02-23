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
    public class DemandAnswerManager : IDemandAnswerService
    {
        IDemandAnswerDal _demandAnswerDal;

        public DemandAnswerManager(IDemandAnswerDal demandAnswerDal)
        {
            _demandAnswerDal = demandAnswerDal;
        }

        public List<DemandAnswer> GetList()
        {
            return _demandAnswerDal.GetListAll();
        }

        public List<DemandAnswer> GetDemandAnswerByDemandId(int id)
        {
            List<DemandAnswer> list = new List<DemandAnswer>();
            list = _demandAnswerDal.GetListAll(x => x.DemandId == id);
            return list;
        }

        public void TAdd(DemandAnswer t)
        {
            _demandAnswerDal.Insert(t);
        }

        public void TDelete(DemandAnswer t)
        {
            throw new NotImplementedException();
        }

        public DemandAnswer TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(DemandAnswer t)
        {
            throw new NotImplementedException();
        }
    }
}
