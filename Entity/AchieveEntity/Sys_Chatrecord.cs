using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using SqlSugar;
namespace AchieveEntity
{
	 	//Sys_Chatrecord
    [SugarMapping(TableName = "Sys_Chatrecord")]
	public class Sys_Chatrecord
	{
   		     
      	/// <summary>
		/// F_Id
        /// </summary>
        public string F_Id{ get; set; }        
		/// <summary>
		/// 发送人id
        /// </summary>
        public string F_SendId{ get; set; }        
		/// <summary>
		/// 接收人id
        /// </summary>
        public string F_AcceptId{ get; set; }        
		/// <summary>
		/// 类型（1.人对人；2.人对群）
        /// </summary>
        public int F_Type{ get; set; }        
		/// <summary>
		/// 消息类容
        /// </summary>
        public string F_Message{ get; set; }        
		/// <summary>
		/// 创建时间
        /// </summary>
        public DateTime F_CreatorTime{ get; set; }        
		/// <summary>
		/// 创建人
        /// </summary>
        public string F_CreatorUserId{ get; set; }        
		   
	}
}

