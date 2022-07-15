using System;
using System.Collections.Generic;
using System.Text;
using WBIS_2.DataModel;

namespace WBIS_2.Modules.Interfaces
{
    public interface IMapNavigation
    {
        string LayerKeyField { get; }
        string LayerName { get; }
        void ZoomToLayer();
        void ZoomToFeature(IInformationType ZoomObject);
        void MapShowAFS(Dictionary<Guid, Guid> selection);

    }
}
