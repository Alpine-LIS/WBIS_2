using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IInformationType
    {
        public Guid Guid { get; set; }
        public IInfoTypeManager Manager { get; }
    }
}
