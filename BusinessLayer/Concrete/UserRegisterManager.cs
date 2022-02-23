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
    public class UserRegisterManager : IUserService
    {
        IUserDal _userDal;

        public UserRegisterManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetList()
        {
            return _userDal.GetListAll();
        }

        public List<User> GetUserById(int id)
        {
            return _userDal.GetListAll(x => x.UserId == id);
        }
        
        public void ChangeStatusValue(int id)
        {
            var user = _userDal.GetListAll(x => x.UserId == id);
            user[0].UserStatus = false;
            TUpdate(user[0]);
        }

        public void TAdd(User t)
        {
            _userDal.Insert(t);
        }

        public void TDelete(User t)
        {
            throw new NotImplementedException();
        }

        public User TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public void TUpdate(User t)
        {
            _userDal.Update(t);
        }
    }
}
