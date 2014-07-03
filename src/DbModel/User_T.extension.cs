namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: User_T
	public partial class User_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO User_T("
        		+ "ucode,"
        		+ "uname,"
        		+ "upass,"
        		+ "ucard,"
        		+ "utag,"
        		+ "email,"
        		+ "mobile,"
        		+ "im,"
        		+ "bday,"
        		+ "oid,"
        		+ "comment,"
        		+ "created,"
        		+ "lastmodified,"
        		+ "active)"
        + " Values("
        		+ "@ucode,"
        		+ "@uname,"
        		+ "@upass,"
        		+ "@ucard,"
        		+ "@utag,"
        		+ "@email,"
        		+ "@mobile,"
        		+ "@im,"
        		+ "getdate(),"
        		+ "@oid,"
        		+ "@comment,"
        		+ "getdate(),"
        		+ "getdate(),"
        		+ "1)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE User_T "
        		+ "SET ucode = @ucode,"
        		+ "uname = @uname,"
        		+ "upass = @upass,"
        		+ "ucard = @ucard,"
        		+ "utag = @utag,"
        		+ "email = @email,"
        		+ "mobile = @mobile,"
        		+ "im = @im,"
        		+ "bday = getdate(),"
        		+ "oid = @oid,"
        		+ "comment = @comment,"
        		+ "lastmodified = getdate(),"
        		+ "active = 1"
        + " WHERE uid = @uid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM User_T WHERE uid=@id";

		public override IDbCommand BuildSqlCommand(IDbContext context, BuildBehavior behavior)
        {
			string sql = string.Empty;
			if (BuildBehavior.InsertCommand == behavior)
				sql = SQLFORMAT_INSERT;
			else if (BuildBehavior.UpdateCommand == behavior)
				sql = SQLFORMAT_UPDATE;
            else if (BuildBehavior.DeleteCommand == behavior)
            {
                sql = SQLFORMAT_DELETE;
                return context.Sql(sql).Parameter("id", Uid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("uid", Uid)
					.Parameter("ucode", Ucode)
					.Parameter("uname", Uname)
					.Parameter("upass", Upass)
					.Parameter("ucard", Ucard)
					.Parameter("utag", Utag)
					.Parameter("email", Email)
					.Parameter("mobile", Mobile)
					.Parameter("im", Im)
					.Parameter("bday", Bday)
					.Parameter("oid", Oid)
					.Parameter("comment", Comment)
					.Parameter("created", Created)
					.Parameter("lastmodified", Lastmodified)
					.Parameter("active", Active);
        }

		public override string DbTable
		{
			get
			{
				return "User_T";
			}
		}
        #endregion Autogen by tools
    }
}
