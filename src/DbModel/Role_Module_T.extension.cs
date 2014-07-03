namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Role_Module_T
	public partial class Role_Module_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Role_Module_T("
        		+ "rid,"
        		+ "mid)"
        + " Values("
        		+ "@rid,"
        		+ "@mid)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Role_Module_T "
        		+ "mid = @mid"
        + " WHERE rid = @rid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Role_Module_T WHERE rid=@id";

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
					.Parameter("mid", Mid);
        }

		public override string DbTable
		{
			get
			{
				return "Role_Module_T";
			}
		}
        #endregion Autogen by tools
    }
}
