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

    public class Fields
    {
        private Dictionary<string, List<string>> _fields;
        public Fields() 
        {
            _fields = new Dictionary<string, List<string>>();
        }

        public void Add<TKey>(Expression<Func<TKey, object>> field) where TKey : PatreonObject
        {
            Type type = typeof(TKey);
            var entityName = typeof(TKey).Name.ToLower();

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

            if (propInfo.PropertyType.IsSubclassOf(typeof(PatreonObject)) ||
                  (typeof(IEnumerable).IsAssignableFrom(propInfo.PropertyType) && propInfo.PropertyType.IsGenericType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a relationship, not a field.",
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
            
            if (_fields.ContainsKey(entityName))
                _fields[entityName].Add(propJsonName);
            else
                _fields.Add(entityName, new List<string>() { propJsonName });
        }
        
        public void AddAllField<TKey>() where TKey : PatreonObject
        {
            var jsonFields = GetJsonFields<TKey>();

            var entityName = typeof(TKey).Name.ToLower();

            if (_fields.ContainsKey(entityName))
                _fields[entityName].AddRange(jsonFields);
            else
            _fields.Add(entityName, new List<string>(jsonFields));
        }

        public bool IsEmpty()
        {
            return !_fields.Keys.Any();
        }

        public string ToQueryParams()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var key in _fields.Keys)
            {
                if (sb.Length != 0)
                    sb.Append("&");

                sb.Append($"fields%5B{key}%5D=");
                sb.AppendJoin(',', _fields[key]);
            }

            return sb.ToString();
        }

        private static List<string> GetJsonFields<T>() where T : PatreonObject
        {
            var t = typeof(T);
            return t.GetProperties()
                .Where(p => !(p.PropertyType.IsSubclassOf(typeof(PatreonObject)) || (typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && p.PropertyType.IsGenericType)))
                .Select(p => p.GetCustomAttribute<JsonPropertyAttribute>())
                .Where(jp => jp != null)
                .Select(jp => jp.PropertyName)
                .ToList();
        }
    }
}
