using System;
using System.Collections.Generic;

namespace haberPortali
{
    public partial class AspnetPaths
    {
        public AspnetPaths()
        {
            AspnetPersonalizationPerUser = new HashSet<AspnetPersonalizationPerUser>();
        }

        public Guid ApplicationId { get; set; }
        public Guid PathId { get; set; }
        public string Path { get; set; }
        public string LoweredPath { get; set; }

        public AspnetApplications Application { get; set; }
        public AspnetPersonalizationAllUsers AspnetPersonalizationAllUsers { get; set; }
        public ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; }
    }
}
