using System.Collections.Generic;
using System.Linq;

namespace Titan
{
    public abstract class ObjectDescriptorBase
    { 
        private PropertyExpression[] _all;
        private readonly string _prefix;

        protected ObjectDescriptorBase(string prefix)
        {
            _prefix = prefix;
        }



        public PropertyExpression[] ALL
        {
            get { return _all; }
            protected set { _all = value; }
        }

        protected string Prefix
        {
            get { return _prefix; } 
        }

        public IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return Exclude((IEnumerable<PropertyExpression>)properties);
        }
        public IEnumerable<PropertyExpression> Exclude(IEnumerable<PropertyExpression> properties)
        {
            return _all.Except(properties, EqualityComparer<PropertyExpression>.Default);
        }
    }
}
