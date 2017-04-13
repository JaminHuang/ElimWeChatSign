using System; 

namespace Titan.ExpressionAnalyse
{
    class PrefixParser
    {
        public string Prefix;
        public string PropertyName;
        public bool HasPrefix;
        public PrefixParser(string s)
        {
            int pos = s.IndexOf('.');
            if (pos > 0)
            {
                HasPrefix = true;
                Prefix = s.Substring(0, pos);
                PropertyName = s.Substring(pos + 1, s.Length - pos - 1);
            }
            else
            {
                HasPrefix = false;
                Prefix = "";
                PropertyName = s;
            }

            //Console.WriteLine(Prefix + "    " + PropertyName);
        }
        public static string JoinPrefix(string prefix, string propertyName)
        {
            string s = propertyName;
            string prefix2 = prefix;
            //string Prefix2=String.EnsureNotNull(Prefix);
            if (!String.IsNullOrEmpty(prefix2))
            {
                s = prefix2 + "." + s;
            }
            //Console.WriteLine(Prefix +"    "+s);
            return s;
        }
    }
}
