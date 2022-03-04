using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class EmployeeDemandManager:IEmployeeDemandService
    {
        private IEmployeeDemandDal _employeeDemandDal;

        public EmployeeDemandManager(IEmployeeDemandDal employeeDemandDal)
        {
            _employeeDemandDal = employeeDemandDal;
        }

        public void TAdd(EmployeeDemand t)
        {
            _employeeDemandDal.Insert(t);
        }

        public void TDelete(EmployeeDemand t)
        {
            _employeeDemandDal.Delete(t);
        }

        public void TUpdate(EmployeeDemand t)
        {
            _employeeDemandDal.Update(t);
        }

        public List<EmployeeDemand> GetList()
        {
            return _employeeDemandDal.GetListAll();
        }
        public List<EmployeeDemand> GetDemandByEmployee(string id)
        {
            return _employeeDemandDal.GetListAll(x=> x.EmployeeId == id);
        }
        
        public List<EmployeeDemand> GetDemandByDemandId(int id)
        {
            return _employeeDemandDal.GetListAll(x=> x.DemandId == id);
        }

        public EmployeeDemand TGetById(int id)
        {
            return _employeeDemandDal.GetById(id);
        }
    }
}