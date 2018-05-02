using System;
using System.IO;
using NDesk.Options;

namespace Konpyuta.GitTfsAuthors
{
    internal sealed class Options
    {
        private readonly OptionSet _optionSet;

        public Uri TfsServerUri { get; private set; }
        public bool Sort { get; private set; }
        public bool ShowHelp { get; private set; }
        public string AuthorsFile { get; private set; }
        public bool KeepEmptyEmail { get; private set; }

        public Options()
        {
            AuthorsFile = "authors.txt";
            ShowHelp = false;
            KeepEmptyEmail = false;
            Sort = false;

            _optionSet = new OptionSet() {
                { "h|help",  "show this message and exit",
                    v => this.ShowHelp = v != null },
                { "a|authors=", "the name of the output file",
                    v => this.AuthorsFile = v },
                { "k|keep-empty-email", "keep accounts without an e-mail address (default false)",
                    v => this.KeepEmptyEmail = v != null },
                { "s|sort",  "sort the entries in the output file (default false)",
                    v => this.Sort = v != null },
                { "t|tfs-server-uri=", "the uri of the tfs server (e.g. http://tfs:8080/tfs)",
                    v =>
                    {
                        if (Uri.TryCreate(v, UriKind.Absolute, out var tfs))
                        {
                            TfsServerUri = tfs;
                        }
                    }
                },
            };
        }

        public void WriteOptionDescriptions(TextWriter writer)
        {
            _optionSet.WriteOptionDescriptions(writer);
        }
    
        /// <summary>
        /// Validates the parsed arguments.
        /// </summary>
        public void Validate()
        {
            if (TfsServerUri == null)
            {
                throw new OptionException("TfsServerUri must be a valid uri.", "TfsServerUri");
            }

            if (string.IsNullOrWhiteSpace(AuthorsFile))
            {
                throw new OptionException("AuthorsFile must be set.", "AuthorsFile");
            }
        }

        public Options Parse(string[] args)
        {
            _optionSet.Parse(args);

            return this;
        }
    }
}