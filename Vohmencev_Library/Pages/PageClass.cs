using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vohmencev_Library.Pages
{
    public static class PageClass
    {
        private static Authorization authorization;
        private static SuperUserRegistration superuserregistration;
        private static AdminPage administrator;
        public static StaffPage staff;

        public static Authorization GetAuthorization()
        {
            if (authorization == null)
            {
                authorization = new Authorization();
            }
            return authorization;
        }

        public static SuperUserRegistration GetSuperUserRegistration()
        {
            if (superuserregistration == null)
            {
                superuserregistration = new SuperUserRegistration();
            }
            return superuserregistration;
        }

        public static AdminPage GetAdministrator()
        {
            if (administrator == null)
            {
                administrator = new AdminPage();
            }
            return administrator;
        }

        public static StaffPage GetStaff()
        {
            if (staff == null)
            {
                staff = new StaffPage();
            }
            return staff;
        }
    }
}
