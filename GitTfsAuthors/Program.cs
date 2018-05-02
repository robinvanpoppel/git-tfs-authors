using System;
using System.IO;
using System.Linq;
using NDesk.Options;

namespace Konpyuta.GitTfsAuthors
{
    class Program
    {
        private static readonly TfsIdentityProvider TfsIdentityProvider = new TfsIdentityProvider();

        static int Main(string[] args)
        {
            var executable = AppDomain.CurrentDomain.FriendlyName;

            try
            {
                var options = new Options().Parse(args);
                if (options.ShowHelp)
                {
                    Console.WriteLine($"{executable}: ");
                    options.WriteOptionDescriptions(Console.Out);
                    Console.WriteLine("Examples");
                    Console.WriteLine($"{executable} --tfs-server-uri http://tfs:8080/tfs");
                    Console.WriteLine($"{executable} --tfs-server-uri http://tfs:8080/tfs --authors=output.txt");
                    Console.WriteLine($"{executable} --tfs-server-uri http://tfs:8080/tfs --authors=output.txt --sort");

                    return 0;
                }

                options.Validate();

                CreateAuthorsFile(options);

                return 0;
            }
            catch (OptionException e)
            {
                Console.Write($"{executable}: ");
                Console.WriteLine(e.Message);
                Console.WriteLine($"Try '{executable} --help' for more information.");
                return -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured: ");
                Console.WriteLine(ex.Message);
                return ex.GetHashCode();
            }
        }

        private static void CreateAuthorsFile(Options options)
        {
            var identities = TfsIdentityProvider.GetIdentities(options.TfsServerUri);

            if (!options.KeepEmptyEmail)
            {
                identities = identities.Where(c => !string.IsNullOrWhiteSpace(c.MailAddress));
            }

            var entries = identities.Select(c => new AuthorEntry(c));
            if (options.Sort)
            {
                entries = entries.OrderBy(c => c.TfsUser);
            }

            using (var sw = new StreamWriter(options.AuthorsFile) { AutoFlush = true })
            {
                foreach (var user in entries)
                {
                    sw.WriteLine(user);
                }

                sw.Flush();
            }
        }
    }
}
