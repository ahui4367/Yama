namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Module_T
	public partial class Module_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Module_T("
        		+ "mname,"
        		+ "mtag,"
        		+ "parentid,"
        		+ "enable,"
        		+ "enable_expr,"
        		+ "created,"
        		+ "lastmodified,"
        		+ "active)"
        + " Values("
        		+ "@mname,"
        		+ "@mtag,"
        		+ "@parentid,"
        		+ "@enable,"
        		+ "@enable_expr,"
        		+ "@created,"
        		+ "@lastmodified,"
        		+ "@active)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Module_T "
        		+ "mname = @mname,"
        		+ "mtag = @mtag,"
        		+ "parentid = @parentid,"
        		+ "enable = @enable,"
        		+ "enable_expr = @enable_expr,"
        		+ "created = @created,"
        		+ "lastmodified = @lastmodified,"
        		+ "active = @active"
        + " WHERE mid = @mid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Module_T WHERE mid=@id";

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
                return context.Sql(sql).Parameter("id", Mid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("mid", Mid)
					.Parameter("mname", Mname)
					.Parameter("mtag", Mtag)
					.Parameter("parentid", Parentid)
					.Parameter("enable", Enable)
					.Parameter("enable_expr", Enable_Expr)
					.Parameter("created", Created)
					.Parameter("lastmodified", Lastmodified)
					.Parameter("active", Active);
        }

		public override string DbTable
		{
			get
			{
				return "Module_T";
			}
		}
        #endregion Autogen by tools
    }
}
