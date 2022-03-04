using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ServiceDepartmentManager: IServiceDepartmentService
    {
        private IServiceDepartmentDal _serviceDepartmentDal;

        public ServiceDepartmentManager(IServiceDepartmentDal serviceDepartmentDal)
        {
            _serviceDepartmentDal = serviceDepartmentDal;
        }

        public void TAdd(ServiceDepartment t)
        {
            _serviceDepartmentDal.Insert(t);
        }

        public void TDelete(ServiceDepartment t)
        {
            throw new System.NotImplementedException();
        }

        public void TUpdate(ServiceDepartment t)
        {
            throw new System.NotImplementedException();
        }

        public List<ServiceDepartment> GetList()
        {
            throw new System.NotImplementedException();
        }

        public ServiceDepartment TGetById(int id)
        {
            return _serviceDepartmentDal.GetById(id);
        }
        public ServiceDepartment GetDepartmentIdByServiceId(int id)
        {
            return _serviceDepartmentDal.GetListAll(x => x.ServiceId==id)[0];
        }
    }
}