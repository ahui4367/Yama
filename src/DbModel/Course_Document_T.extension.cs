
namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Course_Document_T
	public partial class Course_Document_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Course_Document_T("
        		+ "cid,"

        		+ "did)"

        
+ " Values("
        		+ "@cid,"

        		+ "@did)";

        

        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Course_Document_T "
        		+ "did = @did"

        
+ " WHERE cid = @cid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Course_Document_T WHERE cid=@id";

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
					.Parameter("did", Did);

        }

		public override string DbTable
		{
			get
			{
				return "Course_Document_T";
			}
		}
        #endregion Autogen by tools
    }
}
