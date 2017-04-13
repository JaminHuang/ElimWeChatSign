using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Titan
{
    [DataContract]
    [KnownType(typeof(OrderExpression))]
    public class OrderExpressionCollection : IOrderExpressionCollection
    {
        [DataMember]
        private List<IOrderExpression> Items;
         public OrderExpressionCollection()
        {
            Items = new List<IOrderExpression>(); 
        }
         public OrderExpressionCollection(int capcity)
        {
            Items = new List<IOrderExpression>(capcity); 
        }
        public void Add(string propertyName, OrderType orderType)
        {
            Add(new OrderExpression(propertyName, orderType));
        }


        #region List Interface
        public void Add(IOrderExpression item)
        {
            Items.Add(item);
        }
        public int IndexOf(IOrderExpression item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, IOrderExpression item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public IOrderExpression this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }


        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(IOrderExpression item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(IOrderExpression[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IOrderExpression item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<IOrderExpression> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        #endregion

    }
}
