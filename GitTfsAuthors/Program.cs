using System;
using System.IO;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Server;

namespace Konpyuta.GitTfsAuthors
{
    class Program
    {
        static int Main(string[] args)
        {
            var tfsServerUri = args.FirstOrDefault();
            Uri uri;
            if (!Uri.TryCreate(tfsServerUri, UriKind.Absolute, out uri))
            {
                Console.WriteLine("Please specify a tfs server uri (e.g. http://tfs:8080/tfs)");
                return -1;
            }

            var outFile = args.Skip(1).SingleOrDefault() ?? "authors.txt";
            
            CreateAuthorsFile(tfsServerUri, outFile);

            return 0;
        }

        private static void CreateAuthorsFile(string tfsServerUri, string outFile)
        {
            using (var tfs = new TfsTeamProjectCollection(new Uri(tfsServerUri)))
            {
                tfs.EnsureAuthenticated();

                var gss = tfs.GetService<IGroupSecurityService>();
                var sids = gss.ReadIdentity(SearchFactor.AccountName, "Project Collection Valid Users",
                    QueryMembership.Expanded);
                var identities = gss.ReadIdentities(SearchFactor.Sid, sids.Members, QueryMembership.None);

                using (var sw = new StreamWriter(outFile) {AutoFlush = true})
                {
                    foreach (var user in identities.Where(c => c != null).Where(c => c.Type == IdentityType.WindowsUser))
                    {
                        // Wrtie lines in the form of DISSRVTFS03\peter.pan = Peter Pan <peter.pan@disney.com>
                        var line = $@"{user.Domain}\{user.AccountName} = {user.DisplayName} <{user.MailAddress}>";
                        sw.WriteLine(line);
                    }

                    sw.Flush();
                }
            }
        }
    }
}
