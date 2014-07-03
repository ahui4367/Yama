namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Score_Detail_T
	public partial class Score_Detail_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Score_Detail_T("
        		+ "sdid,"
        		+ "wronglist,"
        		+ "correctlist)"
        + " Values("
        		+ "@sdid,"
        		+ "@wronglist,"
        		+ "@correctlist)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Score_Detail_T "
        		+ "wronglist = @wronglist,"
        		+ "correctlist = @correctlist"
        + " WHERE sdid = @sdid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Score_Detail_T WHERE sdid=@id";

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
                return context.Sql(sql).Parameter("id", Sdid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("sdid", Sdid)
					.Parameter("wronglist", Wronglist)
					.Parameter("correctlist", Correctlist);
        }

		public override string DbTable
		{
			get
			{
				return "Score_Detail_T";
			}
		}
        #endregion Autogen by tools
    }
}
