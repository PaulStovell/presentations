using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Infrastructure
{
    public interface IApplicationResourcesManager
    {
        void LoadResources(string path);
    }
}
