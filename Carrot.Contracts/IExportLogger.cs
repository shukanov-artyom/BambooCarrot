using System;

namespace Carrot.Contracts
{
    public interface IExportLogger : IExport
    {
        void Info(string format, params string[] args);

        void Warn(string format, params string[] args);

        void Error(string format, params string[] args);

        void Error(Exception e, string format, params string[] args);
    }
}
