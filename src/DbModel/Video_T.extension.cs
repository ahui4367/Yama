
namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Video_T
	public partial class Video_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Video_T("
        		+ "vname,"
        		+ "comment,"
        		+ "media,"
        		+ "created,"
        		+ "lastmodified)"
         + " Values("
        		+ "@vname,"
        		+ "@comment,"
        		+ "@media,"
        		+ "getdate(),"
        		+ "getdate())";

		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Video_T "
        		+ "SET vname = @vname,"

        		+ "comment = @comment,"

        		+ "media = @media,"

        		+ "lastmodified = getdate()"
        + " WHERE vid = @vid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Video_T WHERE vid=@id";

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
                return context.Sql(sql).Parameter("id", Vid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("vid", Vid)
					.Parameter("vname", Vname)
					.Parameter("comment", Comment)
					.Parameter("media", Media);

        }

		public override string DbTable
		{
			get
			{
				return "Video_T";
			}
		}
        #endregion Autogen by tools
    }
}
