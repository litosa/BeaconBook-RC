using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeaconBookingApi.ViewModels
{
    public class ZoneViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserViewModel> UserViewModels { get; set; }
    }
}
