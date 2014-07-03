namespace DbModel.AspnetDb
{
	using System; 
	//table: Organ_T
	public partial class Organ_T
	{
        #region Properties
		public Int32 Oid  { get; set; }                                        
				
		public String Orgname  { get; set; }                                        
				
		public Int32 Orglevel  { get; set; }                                        
				
		public Int32 Parentid  { get; set; }                                        
				
		public DateTime Created  { get; set; }                                        
				
		public DateTime Lastmodified  { get; set; }                                        
				
		public Boolean Active  { get; set; }                                        
				
		#endregion Properties
	}
}
			