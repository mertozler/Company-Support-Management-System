using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class LogsManager:ILogsService
    {
        private ILogsDal _logsDal;

        public LogsManager(ILogsDal logsDal)
        {
            _logsDal = logsDal;
        }

        public void TAdd(Logs t)
        {
            throw new System.NotImplementedException();
        }

        public void TDelete(Logs t)
        {
            _logsDal.Delete(t);
        }

        public void TUpdate(Logs t)
        {
            throw new System.NotImplementedException();
        }

        public List<Logs> GetList()
        {
            return _logsDal.GetListAll();
        }

        public Logs TGetById(int id)
        {
            return _logsDal.GetById(id);
        }
    }
}