using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElimWeChatSign.IService;
using ElimWeChatSign.Service;

namespace ElimWeChatSign.Business
{
	public class UserInfoBusiness
	{
		IUserInfoSercive userInfoSercive = new UserInfoService();

		public void Login(string mobile, string pwd)
		{
			
		}
	}
}
