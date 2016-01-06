using System;
using Carrot.Contracts;

namespace Carrot.ComposedApplication
{
    public class ApplicationCompositionHelper
    {
        private static IComposedApplication shadowCopiedApp;

        public static IComposedApplication GetInstance()
        {
            if (shadowCopiedApp == null)
            {
                //shadowCopiedApp = new ShadowCopiedComposedApp();
                shadowCopiedApp = new UpdatableComposedApp();
            }
            return shadowCopiedApp;
        }
    }
}
