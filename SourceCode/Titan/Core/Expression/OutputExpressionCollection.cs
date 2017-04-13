using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Titan
{
    [DataContract]
    [KnownType(typeof(PropertyExpression))]
    public class OutputExpressionCollection : IOutputExpressionCollection
    {


        [DataMember]
        private List<IOutputExpression> Items;
        public OutputExpressionCollection()
        {
            Items = new List<IOutputExpression>();
        }
        public OutputExpressionCollection(int capcity)
        {
            Items = new List<IOutputExpression>(capcity);
        }


        public void Add(params object[] propertys)
        {
            if (propertys == null) return;
            foreach (object obj in propertys)
            {
                if (obj == null) continue;

                if (obj is string)
                {
                    Add(new OutputExpression(obj as string));
                }
                else if (obj is IPropertyExpression)
                {
                    IPropertyExpression propertyExpression = (IPropertyExpression)obj;
                    Add(new OutputExpression(propertyExpression.PropertyName, propertyExpression.GroupFunction));
                }
                else if (obj is IOutputExpression)
                {
                    IOutputExpression outputExpression = (IOutputExpression)obj;
                    Add(outputExpression);
                }
                else if (obj is IEnumerable<string>)
                {
                    foreach (string s in obj as IEnumerable<string>)
                    {
                        Add(new OutputExpression(s));
                    }
                }
                else if (obj is IEnumerable<IOutputExpression>)
                {
                    foreach (IOutputExpression s in obj as IEnumerable<IOutputExpression>)
                    {
                        Add(s);
                    }
                }
                else if (obj is IEnumerable<IPropertyExpression>)
                {
                    foreach (IPropertyExpression s in obj as IEnumerable<IPropertyExpression>)
                    {
                        Add(new OutputExpression(s.PropertyName, s.GroupFunction));
                    }
                }
                else
                {
                    throw new Exception("不支持的类型");
                }
            }
        }

        #region List Interface
        public void Add(IOutputExpression item)
        {
            Items.Add(item);
        }
        public int IndexOf(IOutputExpression item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, IOutputExpression item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public IOutputExpression this[int index]
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

        public bool Contains(IOutputExpression item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(IOutputExpression[] array, int arrayIndex)
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

        public bool Remove(IOutputExpression item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<IOutputExpression> GetEnumerator()
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
