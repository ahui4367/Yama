namespace Membership.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using Data.Access;
    using DbModel.AspnetDb;
    using View.Model;
    using YesHJ.Fx;
    using YesHJ.Fx.Error;
    using YesHJ.Fx.Pattern;

    public class UserServiceImpl : UserService
    {
        private static readonly DateTime ORIGINAL_DATETIME = DateTime.Parse("2000-1-1");
        public RepositoryFactory DbFactory 
        {
            get
            {
                return ServiceLocator.Current.Find<RepositoryFactory>();
            }
        }

        #region Methods

        public override UserModel Login(LoginModel model)
        {
            Assert.ThrowIfNullOrEmpty("model", model);
            Assert.ThrowIfNullOrEmpty("login name", model.UserCode);
            Assert.ThrowIfNullOrEmpty("login password", model.Password);

            UserModel result = null;

            using (var repo = DbFactory.Create<User_T>())
            {
                var item = repo.GetFiltered("Ucode = @ucode AND Upass = @upass")
                    .Parameter("ucode", model.UserCode)
                    .Parameter("upass", model.Password)
                    .FirstOrDefault();
                if (item != null)
                {
                    result = new UserModel() 
                    {
                        Uid = item.Uid,
                        Oid = item.Oid,
                        Ucode = item.Ucode,
                        Uname = item.Uname,
                        Email = item.Email,
                        Im = item.Im,
                        Utag = item.Utag,
                        Comment = item.Comment,
                        Created = item.Created.ToString("yyyy-MM-dd"),
                        Lastmodified = item.Lastmodified.ToString("yyyy-MM-dd"),
                        Active = item.Active,
                    };
                }
            }

            return result;
        }

        public override void Logout()
        {
            throw new NotImplementedException();
        }

        public override void Add(UserModel model)
        {
            Assert.ThrowIfNullOrEmpty("model", model);
            Assert.ThrowIfNullOrEmpty("model.uid", model.Uid);
            Assert.ThrowIfNullOrEmpty("model.ucode", model.Ucode);
            Assert.ThrowIfNullOrEmpty("model.uname", model.Uname);

            using (var repo = DbFactory.Create<User_T>())
            {
                User_T t = new User_T 
                {
                    Oid = model.Oid,
                    Ucode = model.Ucode,
                    Uname = model.Uname,
                    Upass = model.Upass,
                    Ucard = model.Ucard,
                    Email = model.Email,
                    Utag = model.Utag,
                    Im = model.Im,
                    Mobile = model.Mobile,
                    Comment = model.Comment,
                    Bday = DateTime.Now,
                    Created = DateTime.Now,
                    Lastmodified = DateTime.Now,
                    Active = true,
                };

                repo.Add(t);
            }
        }

        public override void Update(UserModel model)
        {
            Assert.ThrowIfNullOrEmpty("model", model);
            Assert.ThrowIfNullOrEmpty("model.uid", model.Uid);
            Assert.ThrowIfNullOrEmpty("model.ucode", model.Ucode);
            Assert.ThrowIfNullOrEmpty("model.uname", model.Uname);

            using (var repo = DbFactory.Create<User_T>())
            {
                User_T t = new User_T
                {
                    Oid = model.Oid,
                    Ucode = model.Ucode,
                    Uname = model.Uname,
                    Email = model.Email,
                    Utag = model.Utag,
                    Im = model.Im,
                    Mobile = model.Mobile,
                    Bday = DateTime.Now,
                    Created = DateTime.Now,
                    Lastmodified = DateTime.Now,
                    Active = true,
                };

                repo.Save(t);
            }
        }

        public override void Remove(UserModel model)
        {
            Assert.ThrowIfNullOrEmpty("model", model);
            Assert.ThrowIf(model.Uid < 1, () => { return new ArgumentException("Invalid uid!"); });

            using (var repo = DbFactory.Create<User_T>())
            {
                repo.Remove(new User_T 
                {
                    Uid = model.Uid,
                    Bday = DateTime.Now,
                    Created = DateTime.Now,
                    Lastmodified = DateTime.Now,
                });
            }
        }

        public override void Import(string file)
        {
            throw new NotImplementedException();
        }

        #endregion Methods

        public override IEnumerable<UserModel> Load(PagingModel paging)
        {
            IEnumerable<UserModel> result = new List<UserModel>();

            paging.ValidateParameters();

            StringBuilder exprBuilder = new StringBuilder(" (ucode <> 'admin') ");

            foreach (var pair in paging.Parameters)
            {
                switch(pair.Key)
                {
                    case "email":
                        exprBuilder.AppendFormat(" AND (email LIKE '{0}%')", pair.Value);
                        break;
                    case "":
                        break;
                }
                
            }
            using (var repo = DbFactory.Create<User_T>())
            {
                var list = repo.GetFiltered(exprBuilder.ToString()).GetPaged<User_T>(paging.Page, paging.Rows, "uid asc");
                if (list != null)
                {
                    result = list.Select(t => new UserModel() 
                    {
                        Uid = t.Uid,
                        Ucode = t.Ucode,
                        Uname = t.Uname,
                        Ucard = t.Ucard,
                        Email = t.Email,
                        Utag = t.Utag,
                        Mobile = t.Mobile,
                        Im = t.Im,
                        Oid = t.Oid,
                        Created = t.Created.ToString("yyyy-MM-dd"),
                        Lastmodified = t.Lastmodified.ToString("yyyy-MM-dd"),
                    }).ToList();
                }
            }

            return result;
        }

        public override IEnumerable<RoleModel> Roles()
        {
            IEnumerable<RoleModel> result = new List<RoleModel>();
            using (var repo = DbFactory.Create<Role_T>())
            {
                result = repo.GetAll().Select(role => new RoleModel()
                {
                    RoleID = role.Rid,
                    RoleName = role.Rname,
                    Description = role.Comment,
                    Created = role.Created.ToString("yyyy-MM-dd"),
                    LastModified = role.Lastmodified.ToString("yyyy-MM-dd"),
                }).ToList();
            }

            return result;
        }
    }
}