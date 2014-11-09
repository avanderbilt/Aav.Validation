using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Aav.Validation
{
    /// <summary>
    /// DataTable Extension Methods
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// Determines whether the target of the extension contains the
        /// specified column names.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="requiredColumnNames">The required column names.</param>
        /// <returns>
        /// <c>true</c> if the target of the extension contains the
        /// specified column names; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsRequiredColumns(this DataTable table, out string errorMessage,
                                                   params string[] requiredColumnNames)
        {
            Contract.Requires(table != null);
            Contract.Requires(requiredColumnNames != null);

            errorMessage = null;

            if (requiredColumnNames.Any(column => !table.Columns.Contains(column)))
            {
                var missingColumns =
                    requiredColumnNames.Where(columnName => !table.Columns.Contains(columnName)).ToList();

                var moreThanOneMissingColumn = missingColumns.Count > 1;

                errorMessage = string.Format("The table {0} did not contain {1}the required column{2}: {3}.",
                                             table.TableName,
                                             moreThanOneMissingColumn ? "any of " : string.Empty,
                                             moreThanOneMissingColumn ? "s" : string.Empty,
                                             string.Join(", ", missingColumns));

                return false;
            }

            return true;
        }
    }
}