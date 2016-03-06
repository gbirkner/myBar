using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace myBar.Models {

    [MetadataType(typeof(PlacesMetaData))]
    public partial class Places {
    }



    public class PlacesMetaData {

        [Display(Name = "Place")]
        public string Title { get; set; }

        [Display(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        [Display(Name = "ModifiedBy")]
        public string ModifiedBy { get; set; }

    }


}
