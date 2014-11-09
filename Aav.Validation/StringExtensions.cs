using System.Diagnostics.Contracts;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Aav.Validation
{
    /// <summary>
    /// String Extension Methods
    /// </summary>
    public static class StringExtensions
    {
        private static readonly string ValidEmailAddressRegularExpression;

        /// <summary>
        /// Initializes static members of the <see cref="StringExtensions"/>
        /// class.
        /// </summary>
        static StringExtensions()
        {
            ValidEmailAddressRegularExpression =
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        }

        /// <summary>
        /// Determines whether the target of the extension is a valid path.
        /// </summary>
        /// <param name="value">The target of the extension.</param>
        /// <returns>
        /// <c>true</c> if the target is a valid path; otherwise, <c>false
        /// </c>.
        /// </returns>
        public static bool IsValidPath(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var invalidPathCharsRegex = string.Format("[{0}]",
                                                      Regex.Escape(string.Join(string.Empty, Path.GetInvalidPathChars())));

            var containsAnInvalidCharacter = new Regex(invalidPathCharsRegex);

            return !containsAnInvalidCharacter.IsMatch(value);
        }

        /// <summary>
        /// Determines whether the target of the extension is a valid file
        /// name.
        /// </summary>
        /// <param name="value">The target of the extension.</param>
        /// <returns>
        /// <c>true</c> if the target is a valid file name; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static bool IsValidFileName(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var invalidFileNameCharsRegex = string.Format("[{0}]",
                                                          Regex.Escape(string.Join(string.Empty,
                                                                                   Path.GetInvalidFileNameChars())));

            var containsAnInvalidCharacter = new Regex(invalidFileNameCharsRegex);

            return !containsAnInvalidCharacter.IsMatch(value);
        }

        /// <summary>
        /// Determines whether the target of the extension is a valid email
        /// address.
        /// </summary>
        /// <param name="value">The target of the extension.</param>
        /// <returns>
        /// <c>true</c> if the target is a valid email address; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static bool IsValidEmailAddress(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var emailAddress = Regex.Replace(value, @"(@)(.+)$", DomainMapper, RegexOptions.None);

            return Regex.IsMatch(emailAddress, ValidEmailAddressRegularExpression, RegexOptions.IgnoreCase);
        }

        private static string DomainMapper(Match match)
        {
            Contract.Requires(match != null);
            Contract.Requires(match.Length > 2);

            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;

            domainName = idn.GetAscii(domainName);

            return match.Groups[1].Value + domainName;
        }
    }
}