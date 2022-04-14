using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IInformationType
    {
        public IInfoTypeManager Manager { get; }
    }
}
