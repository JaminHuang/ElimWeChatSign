using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Diagnostics; 

namespace Titan
{
    [KnownType(typeof(ConditionExpression))]
    [DataContract]
    [DebuggerDisplay("Count = {Count} Relation={ConditionRelation}")]
    public class ConditionExpressionCollection : IConditionExpressionCollection
    {
        [DataMember]
        private List<ICondition> Items;
        public ConditionExpressionCollection()
        {
            Items = new List<ICondition>();
            ConditionRelation = ConditionRelation.And;
        }
        public ConditionExpressionCollection(int capcity)
        {
            Items = new List<ICondition>(capcity);
            ConditionRelation = ConditionRelation.And;
        }

        [DataMember]
        public ConditionRelation ConditionRelation { get; set; }


        public void Add(ICondition conditionExpression)
        {
            if (conditionExpression is ConditionExpression)
            {
                Items.Add(conditionExpression);
            }
            else
            {
                IConditionExpressionCollection conditionCollection = (IConditionExpressionCollection)conditionExpression;
                if (ConditionRelation == ConditionRelation.And)
                {
                    if (conditionCollection.ConditionRelation == ConditionRelation.And)
                    {
                        foreach (ICondition item in conditionCollection)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        Items.Add(conditionCollection);
                    }
                }
                else
                {
                    if (conditionCollection.ConditionRelation == ConditionRelation.Or)
                    {
                        foreach (ICondition item in conditionCollection)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        Items.Add(conditionCollection);
                    }
                }
            }
        }

        public void Add(string propertyName, ConditionOperator conditionOperator, object conditionValue)
        {
            Add(new ConditionExpression(propertyName, conditionOperator, conditionValue));
        }

        public void Add(string propertyName,GroupFunction groupFunction, ConditionOperator conditionOperator, object conditionValue)
        {
            Add(new ConditionExpression(propertyName, groupFunction,conditionOperator, conditionValue));
        }



        private IConditionExpressionCollection Copy()
        {
            ConditionExpressionCollection cs = new ConditionExpressionCollection(Items.Count);
            cs.ConditionRelation = ConditionRelation;
            foreach (ICondition conditionExpression in this)
            {
                cs.Add(conditionExpression);
            }
            return cs;
        }
        public void Not()
        {
            ConditionRelation = ConditionRelation == ConditionRelation.And ? ConditionRelation.Or : ConditionRelation.And;
            foreach (ConditionExpression item in this)
            {
                item.Not();
            }
        }
        public ICondition And(ICondition conditionExpression)
        {
            if (ConditionRelation == ConditionRelation.And)
            {
                if (conditionExpression is IConditionExpression)
                {
                    Add(conditionExpression);
                }
                else
                {
                    IConditionExpressionCollection conditionCollection = (IConditionExpressionCollection)conditionExpression;
                    if (conditionCollection.ConditionRelation == ConditionRelation.And)
                    {
                        foreach (ICondition item in conditionCollection)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        Add(conditionCollection);
                    }
                }
            }
            else
            {
                IConditionExpressionCollection cs = Copy();
                Items.Clear();
                Add(cs);
                ConditionRelation = ConditionRelation.And;

                if (conditionExpression is IConditionExpression)
                {
                    Add(conditionExpression);
                }
                else
                {
                    IConditionExpressionCollection conditionCollection = (IConditionExpressionCollection)conditionExpression;
                    if (conditionCollection.ConditionRelation == ConditionRelation.And)
                    {
                        foreach (ICondition item in conditionCollection)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        Add(conditionCollection);
                    }
                }
            }
            return this;
        }
        public ICondition Or(ICondition conditionExpression)
        {
            if (ConditionRelation == ConditionRelation.Or)
            {
                if (conditionExpression is IConditionExpression)
                {
                    Add(conditionExpression);
                }
                else
                {
                    IConditionExpressionCollection conditionCollection = (IConditionExpressionCollection)conditionExpression;
                    if (conditionCollection.ConditionRelation == ConditionRelation.Or)
                    {
                        foreach (ICondition item in conditionCollection)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        Add(conditionCollection);
                    }
                }
            }
            else
            {
                IConditionExpressionCollection cs = Copy();
                Items.Clear();
                Add(cs);
                ConditionRelation = ConditionRelation.Or;

                if (conditionExpression is IConditionExpression)
                {
                    Add(conditionExpression);
                }
                else
                {
                    IConditionExpressionCollection conditionCollection = (IConditionExpressionCollection)conditionExpression;
                    if (conditionCollection.ConditionRelation == ConditionRelation.Or)
                    {
                        foreach (ICondition item in conditionCollection)
                        {
                            Add(item);
                        }
                    }
                    else
                    {
                        Add(conditionCollection);
                    }
                }
            }
            return this;
        }



        #region overload operator
        public static ICondition operator &(ConditionExpressionCollection conditionCollection, ICondition value)
        {
            return conditionCollection.And(value);
        }
        public static ICondition operator |(ConditionExpressionCollection conditionCollection, ICondition value)
        {
            return conditionCollection.Or(value);
        }
        #endregion


        #region List Interface
        public int IndexOf(ICondition item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, ICondition item)
        {
            Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }

        public ICondition this[int index]
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

        public bool Contains(ICondition item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(ICondition[] array, int arrayIndex)
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

        public bool Remove(ICondition item)
        {
            return Items.Remove(item);
        }

        public IEnumerator<ICondition> GetEnumerator()
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
