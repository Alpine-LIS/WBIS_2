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
        void ZoomToFeature(object ZoomObject);
        void MapShowAFS(Dictionary<Guid, string> selection);

    }
}
