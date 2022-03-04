using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class EmployeeServiceManager:IEmployeeServiceService
    {
        private IEmployeeServiceDal _employeeServiceDal;

        public EmployeeServiceManager(IEmployeeServiceDal employeeServiceDal)
        {
            _employeeServiceDal = employeeServiceDal;
        }

        public void TAdd(EmployeeService t)
        {
            _employeeServiceDal.Insert(t);
        }

        public List<EmployeeService> GetEmployeeServiceByUserId(string id)
        {
            return _employeeServiceDal.GetListAll(x => x.Id == id);
        }
        public List<EmployeeService> GetUserIdByServiceId(int id)
        {
            return _employeeServiceDal.GetListAll(x => x.ServiceId == id);
        }

        public void TDelete(EmployeeService t)
        {
            throw new System.NotImplementedException();
        }

        public void TUpdate(EmployeeService t)
        {
            throw new System.NotImplementedException();
        }

        public List<EmployeeService> GetList()
        {
            return _employeeServiceDal.GetListAll();
        }
        
        public List<EmployeeService> GetListByServiceId(int serviceId)
        {
            return _employeeServiceDal.GetListAll(x=> x.ServiceId == serviceId);
        }

        public EmployeeService TGetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}