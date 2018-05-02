using System;
using Microsoft.TeamFoundation.Server;

namespace Konpyuta.GitTfsAuthors
{
    /// <summary>
    /// Represents an entry in the authors output file
    /// </summary>
    internal class AuthorEntry
    {
        private readonly Identity _identity;

        public AuthorEntry(Identity identity)
        {
            this._identity = identity ?? throw new ArgumentNullException(nameof(identity));
        }

        /// <summary>
        /// The tfs user
        /// </summary>
        /// <example>DISSRVTFS03\peter.pan</example>
        public string TfsUser => $@"{_identity.Domain}\{_identity.AccountName}";

        /// <summary>
        /// The git user
        /// </summary>
        /// <example>Peter Pan &lt;peter.pan@disney.com&gt;</example>
        public string GitUser => $"{_identity.DisplayName} <{_identity.MailAddress}>";

        /// <summary>
        /// A line in the form of DISSRVTFS03\peter.pan = Peter Pan &lt;peter.pan@disney.com&gt;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{TfsUser} = {GitUser}";
        }
    }
}