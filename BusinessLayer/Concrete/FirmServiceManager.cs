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
    public class FirmServiceManager : IFirmServiceService
    {
        IFirmServiceDal _firmServiceDal;
        ServiceManager serviceManager = new ServiceManager(new EfServiceRepository());

        public FirmServiceManager(IFirmServiceDal firmServiceDal)
        {
            _firmServiceDal = firmServiceDal;
        }

        public List<FirmService> GetList()
        {
            throw new NotImplementedException();
        }

        public List<FirmService> GetFirmServiceByServiceId(int id)
        {
            return _firmServiceDal.GetListAll(x=> x.ServiceId == id);
        }

        public List<Service> GetServiceByFirmId(int id)
        {
            var getUserFirm = _firmServiceDal.GetListAll(x => x.FirmId == id);
            List<Service> serviceList = new List<Service>();
            foreach (var item in getUserFirm)
            {
                var service = serviceManager.TGetById(item.ServiceId);
                
                    serviceList.Add(service);

                
            }
            return serviceList;
        }

        public void TAdd(FirmService t)
        {
            _firmServiceDal.Insert(t);
        }

        public void TDelete(FirmService t)
        {
            _firmServiceDal.Delete(t);
        }

        public FirmService TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(FirmService t)
        {
            throw new NotImplementedException();
        }
    }

}
