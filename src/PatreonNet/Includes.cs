using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using PatreonNet.Resources;

namespace PatreonNet
{
    public class Includes
    {
        private IList<string> _includes;

        public Includes() 
        {
            _includes = new List<string>();
        }

        public void Add<TKey>(Expression<Func<TKey, object>> field) where TKey : PatreonObject
        {
            Type type = typeof(TKey);

            MemberExpression member = field.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    field.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    field.ToString()));

            if (!(propInfo.PropertyType.IsSubclassOf(typeof(PatreonObject)) ||
                  (typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType) && propInfo.PropertyType.IsGenericType)))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a relationship.",
                    field.ToString()));


            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    field.ToString(),
                    type));
            
            var propJsonName = propInfo.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName;
            if (string.IsNullOrWhiteSpace(propJsonName))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that have not JsonPropertyAttribute.",
                    field.ToString()));

            _includes.Add(propJsonName);
        }
        
        public string ToQueryParams()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"include=");
            sb.AppendJoin(',', _includes);

            return sb.ToString();
        }

        public bool Any()
        {
            return _includes.Any();
        }
    }
}
