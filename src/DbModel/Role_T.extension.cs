namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Role_T
	public partial class Role_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Role_T("
        		+ "rid,"
        		+ "rtag,"
        		+ "rname,"
        		+ "comment,"
        		+ "created,"
        		+ "lastmodified,"
        		+ "active)"
        + " Values("
        		+ "@rid,"
        		+ "@rtag,"
        		+ "@rname,"
        		+ "@comment,"
        		+ "getdate(),"
        		+ "getdate(),"
        		+ "@active)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Role_T "
        		+ "rtag = @rtag,"
        		+ "rname = @rname,"
        		+ "comment = @comment,"
        		+ "lastmodified = getdate(),"
        		+ "active = @active"
        + " WHERE rid = @rid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Role_T WHERE rid=@id";

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
                return context.Sql(sql).Parameter("id", Rid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("rid", Rid)
					.Parameter("rtag", Rtag)
					.Parameter("rname", Rname)
					.Parameter("comment", Comment)
					.Parameter("created", Created)
					.Parameter("lastmodified", Lastmodified)
					.Parameter("active", Active);
        }

		public override string DbTable
		{
			get
			{
				return "Role_T";
			}
		}
        #endregion Autogen by tools
    }
}
