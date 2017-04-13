using System;
using System.Data;

namespace Titan
{
    public class ParameterBase : IParameter
    {
        public ParameterBase()
        {
            Direction = ParameterDirection.Input;
        }
        public string ParameterName { get; set; }
        public ParameterDirection Direction { get; set; }
        public int Size { get; set; }
        public Type PropertyType { get; set; }
         
    }
}
