using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpine.EFTools.Interfaces
{
    public interface IDeleteRepository
    {
        public bool _delete { get; set; }
        public bool Repository { get; set; }
    }
}
