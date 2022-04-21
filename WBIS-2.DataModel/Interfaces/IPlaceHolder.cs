using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WBIS_2.DataModel
{
    public interface IPlaceHolder
    {
        //Records might be created from imports that shouldn't be used when creating new records.
        public bool PlaceHolder { get; set; }
    }
}
