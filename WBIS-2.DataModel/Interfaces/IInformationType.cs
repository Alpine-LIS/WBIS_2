using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    [DisplayOrder(Index = int.MaxValue)]
    public interface IInformationType
    {
        public Guid Guid { get; set; }
        public IInfoTypeManager Manager { get; }
    }

    
    public interface IWildlifeRecord
    { }
    public interface IBotanyRecord
    { }
}
