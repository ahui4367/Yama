namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Organ_T
	public partial class Organ_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Organ_T("
        		+ "orgname,"
        		+ "orglevel,"
        		+ "parentid,"
        		+ "created,"
        		+ "lastmodified,"
        		+ "active)"
        + " Values("
        		+ "@orgname,"
        		+ "@orglevel,"
        		+ "@parentid,"
        		+ "getdate(),"
        		+ "getdate(),"
        		+ "@active)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Organ_T "
         + "SET orgname = @orgname,"
        		+ "orglevel = @orglevel,"
        		+ "parentid = @parentid,"
        		+ "lastmodified = getdate(),"
        		+ "active = 1"
        + " WHERE oid = @oid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Organ_T WHERE oid=@id";

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
                return context.Sql(sql).Parameter("id", Oid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("oid", Oid)
					.Parameter("orgname", Orgname)
					.Parameter("orglevel", Orglevel)
					.Parameter("parentid", Parentid)
					.Parameter("created", Created)
					.Parameter("lastmodified", Lastmodified)
					.Parameter("active", Active);
        }

		public override string DbTable
		{
			get
			{
				return "Organ_T";
			}
		}
        #endregion Autogen by tools
    }
}
