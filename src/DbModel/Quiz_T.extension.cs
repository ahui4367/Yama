namespace DbModel.AspnetDb
{
	using System;
	using FluentData;
	using Data.Access.Impl;
	//table: Quiz_T
	public partial class Quiz_T : Entity
    {
        #region Autogen by tools
        
        private static readonly string SQLFORMAT_INSERT = 
        "INSERT INTO Quiz_T("
        		+ "cid,"
                + "pageno,"
        		+ "quiz,"
        		+ "op1,"
        		+ "op2,"
        		+ "op3,"
        		+ "op4,"
        		+ "type,"
                + "seq,"
        		+ "created,"
        		+ "lastmodified,"
        		+ "active)"
        + " Values("
        		+ "@cid,"
                + "@pageno,"
        		+ "@quiz,"
        		+ "@op1,"
        		+ "@op2,"
        		+ "@op3,"
        		+ "@op4,"
        		+ "@type,"
                + "@seq,"
        		+ "GETDATE(),"
        		+ "GETDATE(),"
        		+ "@active)";
        
        
		private static readonly string SQLFORMAT_UPDATE =
        "UPDATE Quiz_T "
         + "SET cid = @cid,"
                + "pageno = @pageno,"
        		+ "quiz = @quiz,"
        		+ "op1 = @op1,"
        		+ "op2 = @op2,"
        		+ "op3 = @op3,"
        		+ "op4 = @op4,"
        		+ "type = @type,"
                + "seq = @seq,"
        		+ "lastmodified = GETDATE(),"
        		+ "active = @active"
        + " WHERE qid = @qid";
        
        private static readonly string SQLFORMAT_DELETE =
        "DELETE FROM Quiz_T WHERE qid=@id";

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
                return context.Sql(sql).Parameter("id", Qid);
            }
			else
				return null;
            

            return context.Sql(sql)
					.Parameter("qid", Qid)
					.Parameter("cid", Cid)
                    .Parameter("pageno", Pageno)
					.Parameter("quiz", Quiz)
					.Parameter("op1", Op1)
					.Parameter("op2", Op2)
					.Parameter("op3", Op3)
					.Parameter("op4", Op4)
					.Parameter("type", Type)
                    .Parameter("seq", Seq)
					.Parameter("active", Active);
        }

		public override string DbTable
		{
			get
			{
				return "Quiz_T";
			}
		}
        #endregion Autogen by tools
    }
}
