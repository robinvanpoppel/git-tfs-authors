using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;

namespace Konpyuta.GitTfsAuthors
{
    public class TfsIdentityProvider
    {
        /// <summary>
        /// Retrieve all the identities for the given <see cref="tfsServerUri"/>
        /// </summary>
        /// <param name="tfsServerUri"></param>
        /// <returns></returns>
        public IEnumerable<Identity> GetIdentities(Uri tfsServerUri)
        {
            if (tfsServerUri == null)
            {
                throw new ArgumentNullException(nameof(tfsServerUri));
            }

            using (var tfs = new TfsTeamProjectCollection(tfsServerUri))
            {
                tfs.EnsureAuthenticated();

                var gss = tfs.GetService<IGroupSecurityService>();
                var sids = gss.ReadIdentity(SearchFactor.AccountName, "Project Collection Valid Users", QueryMembership.Expanded);
                var identities = gss.ReadIdentities(SearchFactor.Sid, sids.Members, QueryMembership.None)
                    .Where(c => c != null)
                    .Where(c => c.Type == IdentityType.WindowsUser);

                return identities;
            }
        }
    }
}