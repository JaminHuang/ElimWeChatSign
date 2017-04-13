using System;
using System.Collections.Generic; 
using System.Text;
using System.Data;

namespace Titan.SqlTracer
{
    public class ConsoleSqlTracer : ISqlTracer
    { 
        
        public void Trace(IDbCommand command)
        {
            if (command == null) return ;

            StringBuilder sb = new StringBuilder();
            sb.Append("Time:");
            sb.Append(DateTime.Now);
            sb.Append("\t");

            sb.Append("CommandType:");
            sb.Append(command.CommandType);
            sb.Append("\r\n");
            sb.Append(command.CommandText);
            sb.Append("\r\n");
            if (command.Parameters != null)
            {
                sb.Append("Parameters(Count=");
                sb.Append(command.Parameters.Count);
                sb.Append("):\r\n");
                foreach (IDataParameter parameter in command.Parameters)
                {
                    sb.Append("\t");
                    sb.Append(parameter.ParameterName);
                    sb.Append("\t");
                    sb.Append(parameter.DbType);
                    sb.Append("\t");
                    sb.Append(parameter.Direction);
                    sb.Append("\t");
                    sb.Append(parameter.Value);
                    sb.Append("\r\n");
                }
            }

            Console.WriteLine(sb.ToString());
             
        }


        //public void Initialize(IDictionary<string, string> parameters)
        //{
             
        //}
    }
}
