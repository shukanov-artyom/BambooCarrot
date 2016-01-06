using System;

namespace Carrot.Contracts
{
    public interface IComposedApplication
    {
        IComponentsInteraction Interaction { get; }

        IExport[] Exports { get; }

        TInterface GetExport<TInterface>() where TInterface : IExport;
    }
}
