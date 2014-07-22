
namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Course_Video_T
	public partial class Course_Video_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Course_Video_T("
        		+ "cid,"

        		+ "vid)"

        
+ " Values("
        		+ "@cid,"

        		+ "@vid)";

        

        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Course_Video_T "
        		+ "vid = @vid"

        
+ " WHERE cid = @cid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Course_Video_T WHERE cid=@cid vid=@vid";

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
					.Parameter("vid", Vid);

        }

		public override string DbTable
		{
			get
			{
				return "Course_Video_T";
			}
		}
        #endregion Autogen by tools
    }
}
