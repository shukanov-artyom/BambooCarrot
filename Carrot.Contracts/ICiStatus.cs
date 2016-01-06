using System;

namespace Carrot.Contracts
{
    public interface ICiStatus : IExport
    {
        CiWarningLevel Level { get; set; }

        string[] Offenders { get; set; }

        void Refresh();
    }
}
