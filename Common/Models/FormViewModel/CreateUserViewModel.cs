
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Common.Models.FormViewModel
{
    public class CreateUserViewModel
    {
        //  public List<Company> Companies { get; set; }
        public Users Users { get; set; }

        public List<Region> Regions { get; set; }
        public int RegionID { get; set; }

    }
}