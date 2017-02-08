using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
	 	//Sys_Flock
    [SugarMapping(TableName = "Sys_Flock")]
	public class Sys_Flock
	{
   		     
      	/// <summary>
		/// F_Id
        /// </summary>
        public string F_Id{ get; set; }        
		/// <summary>
		/// 群名称
        /// </summary>
        public string F_FlockName{ get; set; }        
		/// <summary>
		/// 群照片
        /// </summary>
        public string F_Flockphotos{ get; set; }        
		/// <summary>
		/// 群公告
        /// </summary>
        public string F_Description{ get; set; } 
		/// <summary>
		/// 创建时间
        /// </summary>
        public DateTime F_CreatorTime{ get; set; }        
		/// <summary>
		/// 创建人
        /// </summary>
        public string F_CreatorUserId{ get; set; }        
		/// <summary>
		/// 修改时间
        /// </summary>
        public DateTime F_LastModifyTime{ get; set; }        
		/// <summary>
		/// 修改人
        /// </summary>
        public string F_LastModifyUserId{ get; set; }        
		   
	}
}

