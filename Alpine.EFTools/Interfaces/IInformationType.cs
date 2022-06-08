using Alpine.EFTools.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Alpine.EFTools.Interfaces
{
    //[DisplayOrder(Index = int.MaxValue)]
    public interface IInformationType
    {
        public Guid Guid { get; set; }
        public IInfoTypeManager Manager { get; }
    }
}
