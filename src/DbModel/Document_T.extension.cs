
namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Document_T
	public partial class Document_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Document_T("
        		+ "dname,"

        		+ "comment,"

        		+ "media,"
                + "count,"
        		+ "created,"

        		+ "lastmodified)"

        
+ " Values("
        		+ "@dname,"

        		+ "@comment,"

        		+ "@media,"
                + "@count,"
        		+ "getdate(),"

        		+ "getdate())";

        

        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Document_T "
         + "SET comment = @comment,"
        	 + "lastmodified = getdate()"
        + " WHERE did = @did";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Document_T WHERE did=@id";

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
                return context.Sql(sql).Parameter("id", Did);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("did", Did)
					.Parameter("dname", Dname)
					.Parameter("comment", Comment)
                    .Parameter("count", Count)
					.Parameter("media", Media);

        }

		public override string DbTable
		{
			get
			{
				return "Document_T";
			}
		}
        #endregion Autogen by tools
    }
}
