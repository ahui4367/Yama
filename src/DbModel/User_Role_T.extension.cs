namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: User_Role_T
	public partial class User_Role_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO User_Role_T("
        		+ "uid,"
        		+ "rid)"
        + " Values("
        		+ "@uid,"
        		+ "@rid)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE User_Role_T "
        		+ "rid = @rid"
        + " WHERE uid = @uid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM User_Role_T WHERE uid=@id";

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
					.Parameter("rid", Rid);
        }

		public override string DbTable
		{
			get
			{
				return "User_Role_T";
			}
		}
        #endregion Autogen by tools
    }
}
