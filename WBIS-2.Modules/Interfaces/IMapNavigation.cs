using System;
using System.Collections.Generic;
using System.Text;

namespace WBIS_2.Modules.Interfaces
{
    public interface IMapNavigation
    {
        string TableKeyField { get; }
        string LayerKeyField { get; }
        string LayerName { get; }
        void ZoomToLayer();
        
    }
}
