using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class AspnetRoles
    {
        public AspnetRoles()
        {
            AspnetUsersInRoles = new HashSet<AspnetUsersInRoles>();
        }

        public Guid ApplicationId { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string LoweredRoleName { get; set; }
        public string Description { get; set; }

        public AspnetApplications Application { get; set; }
        public ICollection<AspnetUsersInRoles> AspnetUsersInRoles { get; set; }
    }
}
