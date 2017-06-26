using System;
using System.Linq;

namespace BusinessLogic.Data
{
  public static class AttributeExtensions
  {
    public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector)
        where TAttribute : Attribute
    {
      var att = type
        .GetCustomAttributes(typeof(TAttribute), true)
        .FirstOrDefault() as TAttribute;

      return att != null ? valueSelector(att) : default(TValue);
    }
  }
}