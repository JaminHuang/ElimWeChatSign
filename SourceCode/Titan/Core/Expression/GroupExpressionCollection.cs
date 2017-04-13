using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Titan
{
    [DataContract]
    public class GroupExpressionCollection : IGroupExpressionCollection
    {


        [DataMember]
        private List<IGroupExpression> Items;
        public GroupExpressionCollection()
        {
            Items = new List<IGroupExpression>();
        }
        public GroupExpressionCollection(int capcity)
        {
            Items = new List<IGroupExpression>(capcity);
        }



        public void Add(IEnumerable<IGroupExpression> propertys)
        {
            Items.AddRange(propertys);
        }
        public void Add(IEnumerable<string> propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                Items.Add(new GroupExpression(propertyName));
            }
        }
        public void Add(string propertyName)
        {
            Items.Add(new GroupExpression(propertyName));
        }

        #region List Interface
        public void Add(IGroupExpression item)
        {
            Items.Add(item);
        }
        public int IndexOf(IGroupExpression item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, IGroupExpression item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public IGroupExpression this[int index]
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

        public bool Contains(IGroupExpression item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(IGroupExpression[] array, int arrayIndex)
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

        public bool Remove(IGroupExpression item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<IGroupExpression> GetEnumerator()
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
