using System;

namespace Common.Exceptions
{
  public class KrakenException : Exception
  {
    private KrakenExceptionCode _code;

    public KrakenException()
    {
      _code = KrakenExceptionCode.Default;
    }

    public KrakenException(string message)
      : base(message)
    {
      _code = KrakenExceptionCode.Default;
    }

    public KrakenException(KrakenExceptionCode code, string message)
      : base(message)
    {
      _code = code;
    }

    public KrakenException(KrakenExceptionCode code, string pattern, params string[] @params)
      : base(string.Format(pattern, @params))
    {
      _code = code;
    }
  }
}