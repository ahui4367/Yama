namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Score_T
	public partial class Score_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Score_T("
        		+ "pid,"
        		+ "uid,"
        		+ "cid,"
        		+ "score,"
        		+ "prescore,"
        		+ "created,"
        		+ "lastmodified)"
        + " Values("
        		+ "@pid,"
        		+ "@uid,"
        		+ "@cid,"
        		+ "@score,"
        		+ "@prescore,"
        		+ "@created,"
        		+ "@lastmodified)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Score_T "
        		+ "uid = @uid,"
        		+ "cid = @cid,"
        		+ "score = @score,"
        		+ "prescore = @prescore,"
        		+ "created = @created,"
        		+ "lastmodified = @lastmodified"
        + " WHERE pid = @pid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Score_T WHERE pid=@id";

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
                return context.Sql(sql).Parameter("id", Pid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("pid", Pid)
					.Parameter("uid", Uid)
					.Parameter("cid", Cid)
					.Parameter("score", Score)
					.Parameter("prescore", Prescore)
					.Parameter("created", Created)
					.Parameter("lastmodified", Lastmodified);
        }

		public override string DbTable
		{
			get
			{
				return "Score_T";
			}
		}
        #endregion Autogen by tools
    }
}
