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
    public class DemandFileManager : IDemandFileService
    {
        IDemandFileDal _demandFileDal;

        public DemandFileManager(IDemandFileDal demandFileDal)
        {
            _demandFileDal = demandFileDal;
        }

        public List<DemandFile> GetList()
        {
            throw new NotImplementedException();
        }

        public List<DemandFile> GetFileByDemandId(int id)
        {
            return _demandFileDal.GetListAll(x=> x.DemandId== id);  
        }

        public void TAdd(DemandFile t)
        {
            _demandFileDal.Insert(t);
        }

        public void TDelete(DemandFile t)
        {
            throw new NotImplementedException();
        }

        public DemandFile TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(DemandFile t)
        {
            throw new NotImplementedException();
        }
    }
}
