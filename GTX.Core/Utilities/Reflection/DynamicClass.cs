using System.Text;
using System.Reflection;
using System;

namespace GTX
{
    [Serializable]
    public abstract class DynamicClass
    {
        public override string ToString()
        {
            var props = GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var stringArr = new string[props.Length];
            var counter = 0;
            foreach(var prop in props)
            {
                stringArr[counter] = string.Format("{0}={1}", prop.Name, prop.GetValue(this, null));
                counter++;
            }
            var sb = new StringBuilder();
            sb.Append(string.Format("{{0}}",string.Join(",",stringArr)));
            return sb.ToString();
        }
    }
}

