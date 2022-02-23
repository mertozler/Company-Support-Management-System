using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class DemandManager : IDemandService
    {
        IDemandDal _demandDal;

        //UserManager userManager = new UserManager(new EfUserRepository());
        public DemandManager(IDemandDal demandDal)
        {
            _demandDal = demandDal;
        }

        public List<Demand> GetList()
        {
            List<Demand> list = new List<Demand>();
            list = _demandDal.GetListAll();
            return list;
        }

       

        public List<Demand> GetDemandById(int id)
        {
            List<Demand> list = new List<Demand>();
            list = _demandDal.GetListAll(x => x.DemandId == id);
            //User newUser = new User();
            foreach (Demand demand in list)
            {
                //newUser = userManager.TGetById(demand.UserId);
                //demand.user = newUser;
            }
            return list;
        }

      

        public void ChangeStatusValue(int id)
        {
            var demands = _demandDal.GetListAll(x => x.DemandId == id);
            demands[0].DemandStatus = false;
            TUpdate(demands[0]);
        }

        public List<Demand> getDemandByUserId(string id)
        {
            var demands = _demandDal.GetListAll(x=> x.UserId==id);
            return demands;
        }

        public void TAdd(Demand t)
        {
            _demandDal.Insert(t);
        }

        public void TDelete(Demand t)
        {
            _demandDal.Delete(t);
        }

        public Demand TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Demand t)
        {
            _demandDal.Update(t);
        }
    }
}
