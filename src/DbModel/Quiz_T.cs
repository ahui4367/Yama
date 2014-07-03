namespace DbModel.AspnetDb
{
	using System; 
	//table: Quiz_T
	public partial class Quiz_T
	{
        #region Properties
		public Int32 Qid  { get; set; }                                        
				
		public Int32 Cid  { get; set; }                                        
				
		public Int32 Pageno  { get; set; }                                        
				
		public String Quiz  { get; set; }                                        
				
		public String Op1  { get; set; }                                        
				
		public String Op2  { get; set; }                                        
				
		public String Op3  { get; set; }                                        
				
		public String Op4  { get; set; }                                        
				
		public Int32 Type  { get; set; }                                        
				
		public Int32 Answer  { get; set; }                                        
				
		public Int32 Seq  { get; set; }                                        
				
		public DateTime Created  { get; set; }                                        
				
		public DateTime Lastmodified  { get; set; }                                        
				
		public Boolean Active  { get; set; }                                        
				
		#endregion Properties
	}
}
			