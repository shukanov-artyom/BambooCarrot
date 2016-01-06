using System;

namespace Carrot.Contracts
{
    public interface IComponentsInteraction : IExport
    {
        void Setup(params IExport[] exports);

        void TearDown();
    }
}
