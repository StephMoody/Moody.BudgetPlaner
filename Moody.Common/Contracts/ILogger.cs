using System.Xml.Schema;

namespace Moody.Common.Contracts;

public interface ILogger
{
    public void Error(Exception e);
}