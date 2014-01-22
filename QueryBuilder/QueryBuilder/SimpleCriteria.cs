using System;
using System.Collections.Generic;
using System.Linq;

namespace QueryBuilder
{
    public class SimpleCriteria
    {
        public enum TypeCriteria
        {
            AND,
            OR
        }

        protected string _criteria;
        protected TypeCriteria _type;
        protected string[] _parameters;

        protected SimpleCriteria()
        {
            _criteria = "";
            _type = TypeCriteria.AND;
            _parameters = null;
        }

        public SimpleCriteria(string criteria, TypeCriteria type, string[] parameters)
        {
            _criteria = criteria;
            _type = type;
            _parameters = parameters;
        }

        public virtual string build()
        {
            return _criteria;
        }

        public void setType(TypeCriteria value)
        {
            _type = value;
        }

        public TypeCriteria getType()
        {
            return _type;
        }

        public void setParameters(string[] value)
        {
            _parameters = value;
        }

        public virtual string[] getParameters()
        {
            return _parameters;
        }
    }
}