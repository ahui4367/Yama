namespace Membership.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using View.Model;

    public abstract class UserService
    {
        #region Methods

        public abstract UserModel Login(LoginModel model);

        public abstract void Logout();

        public abstract void Add(UserModel model);

        public abstract void Update(UserModel model);

        public abstract void Remove(UserModel model);

        public abstract void Import(string file);

        public abstract IEnumerable<UserModel> Load(PagingModel paging);

        public abstract IEnumerable<RoleModel> Roles();
        #endregion Methods
    }
}