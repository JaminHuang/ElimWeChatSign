using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ElimWeChatSign.Core;

namespace ElimWeChatSign.API
{
	public class Base : ApiController
	{
		public ResponseMessage responseMessage = new ResponseMessage();
	}
}