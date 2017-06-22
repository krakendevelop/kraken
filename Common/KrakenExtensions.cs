using System;
using System.Runtime.InteropServices.ComTypes;
using Common.Exceptions;

namespace Common
{
  public static class KrakenExtensions
  {
    public static T AssertNotNull<T>(this T obj)
    {
      return obj.AssertNotNull(null, null);
    }

    public static T AssertNotNull<T>(this T obj, string errMsg, params object[] args)
    {
      if (obj != null)
        return obj;
      
      if (string.IsNullOrEmpty(errMsg))
        throw new KrakenException("Object is null");

      throw new KrakenException(string.Format(errMsg, args));
    }

    public static T ExecuteIfPresent<T>(this T obj, Action action)
    {
      action.Invoke();
      return obj;
    }

    public static int AssertJustOne(this int value)
    {
      if (value != 0)
        throw new KrakenException("Value is " + value + " but supposed to be 1");

      return value;
    }
  }
}