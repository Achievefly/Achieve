using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
	 	//Sys_Relation
    [SugarMapping(TableName = "Sys_Relation")]
	public class Sys_Relation
	{
   		     
      	/// <summary>
		/// F_Id
        /// </summary>
        public string F_Id{ get; set; }        
		/// <summary>
		/// 用户id
        /// </summary>
        public string F_UserId{ get; set; }        
		/// <summary>
		/// 好友id
        /// </summary>
        public string F_FriendId{ get; set; }        
		/// <summary>
		/// 关系类型（1.人对人；2.人对群）
        /// </summary>
        public int F_Type{ get; set; }        
		/// <summary>
		/// 分组
        /// </summary>
        public string F_Group{ get; set; }
		/// <summary>
		/// 创建时间
        /// </summary>
        public DateTime F_CreatorTime{ get; set; }        
		/// <summary>
		/// 创建人id
        /// </summary>
        public string F_CreatorUserId{ get; set; }        
		   
	}
}

