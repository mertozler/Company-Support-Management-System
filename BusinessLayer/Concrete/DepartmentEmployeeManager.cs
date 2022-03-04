using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class DepartmentEmployeeManager:IDepartmentEmployeeService
    {
        IDepartmentEmployeeDal _departmentEmployeeDal;

        public DepartmentEmployeeManager(IDepartmentEmployeeDal departmentEmployeeDal)
        {
            _departmentEmployeeDal = departmentEmployeeDal;
        }

        public void TAdd(DepartmentEmployee t)
        {
            _departmentEmployeeDal.Insert(t);
        }

        public List<DepartmentEmployee> GetDepartmentEmployeeByUserId(string id)
        {
            return _departmentEmployeeDal.GetListAll(x => x.Id == id);
        }
        
        public List<DepartmentEmployee> GetUserIdByDepartmentId(int id)
        {
            return _departmentEmployeeDal.GetListAll(x => x.DepartmentId == id);
        }

        public void TDelete(DepartmentEmployee t)
        {
            throw new System.NotImplementedException();
        }

        public void TUpdate(DepartmentEmployee t)
        {
            throw new System.NotImplementedException();
        }

        public List<DepartmentEmployee> GetList()
        {
            return _departmentEmployeeDal.GetListAll();
        }

        public DepartmentEmployee TGetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}