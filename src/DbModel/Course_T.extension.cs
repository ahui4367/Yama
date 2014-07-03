namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Course_T
	public partial class Course_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Course_T("
        		+ "cname,"
        		+ "category,"
        		+ "rank,"
        		+ "limit,"
        		+ "type,"
        		+ "media,"
        		+ "created,"
        		+ "lastmodified,"
        		+ "active)"
        + " Values("
        		+ "@cname,"
        		+ "@category,"
        		+ "@rank,"
        		+ "@limit,"
        		+ "@type,"
        		+ "@media,"
        		+ "GETDATE(),"
        		+ "GETDATE(),"
        		+ "@active)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Course_T "
        		+ "cname = @cname,"
        		+ "category = @category,"
        		+ "rank = @rank,"
        		+ "limit = @limit,"
        		+ "type = @type,"
        		+ "media = @media,"
        		+ "created = @created,"
        		+ "lastmodified = GETDATE(),"
        		+ "active = @active"
        + " WHERE cid = @cid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Course_T WHERE cid=@id";

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
                return context.Sql(sql).Parameter("id", Cid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("cid", Cid)
					.Parameter("cname", Cname)
					.Parameter("category", Category)
					.Parameter("rank", Rank)
					.Parameter("limit", Limit)
					.Parameter("type", Type)
					.Parameter("media", Media)
					.Parameter("created", DateTime.Now)
					.Parameter("lastmodified", DateTime.Now)
					.Parameter("active", true);
        }

		public override string DbTable
		{
			get
			{
				return "Course_T";
			}
		}
        #endregion Autogen by tools
    }
}
