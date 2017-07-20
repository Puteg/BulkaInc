using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkaBussinessLogic.Model.Profile
{
    public class ProfileModel
    {
        public string Id { get; set; }

        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string ImageUrl { get; set; }
    }
}
