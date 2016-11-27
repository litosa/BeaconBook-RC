using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class AuthenticatedUserViewModel
    {
        public string UserName { get; set; }

        public string AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
