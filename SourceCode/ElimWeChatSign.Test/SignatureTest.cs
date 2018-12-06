using System;
using System.Collections.Generic;
using JaminHuang.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElimWeChatSign.Test
{
    [TestClass]
    public class SignatureTest
    {
        private static string public_key = "www";
        private static string private_key = "aaX53gZpHngeB1O8Z3Ugpt906JzmZcIarD01oqznU/nfiZw452nkfr==";

        [TestMethod]
        public void GetHeader()
        {
            SortedDictionary<string, string> a = new SortedDictionary<string, string>();
            var nonce = "ajbtvuxbck";
            var timestamp = "1544074190007";
            a.Add("publickey", public_key);
            a.Add("nonce", nonce);
            a.Add("timestamp", timestamp);
            a.Add("privatekey", private_key);
            var str = string.Join("&", ExtendUtil.Kv(a));
            var signature = ExtendUtil.Md5(string.Join("&", ExtendUtil.Kv(a)));
            a.Add("signature", signature);

            Console.WriteLine("signature:" + str);
            Console.WriteLine("publickey:" + public_key);
            Console.WriteLine("nonce:" + nonce);
            Console.WriteLine("timestamp:" + timestamp);
            Console.WriteLine("signature:" + signature);
        }
    }
}
