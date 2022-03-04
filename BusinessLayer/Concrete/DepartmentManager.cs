using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    
    public class DepartmentManager:IDepartmentService
    {
        IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public void TAdd(Department t)
        {
            _departmentDal.Insert(t);
        }

        public void TDelete(Department t)
        {
            
            _departmentDal.Delete(t);
        }

        public void TUpdate(Department t)
        {
            _departmentDal.Update(t);
        }
        
        public Department GetDepartmentById(int id)
        {
            var selectedDepartment = _departmentDal.GetListAll(x => x.DepartmentId == id);
            return selectedDepartment[0];
        }

        public Department GetDefaultDepartment()
        {
            var departmentList = GetList();
            Department defaultDepartment = new Department();
            foreach (var item in departmentList)
            {
                if (item.DepartmentisDefault == true)
                {
                    defaultDepartment = item;
                }
            }

            return defaultDepartment;
        }
        

        public List<Department> GetList()
        {
            return _departmentDal.GetListAll();
        }

        public Department TGetById(int id)
        {
            return _departmentDal.GetById(id);
        }
    }
}