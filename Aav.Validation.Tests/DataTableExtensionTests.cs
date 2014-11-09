using System.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Aav.Validation.Tests
{
    public class DataTableExtensionTests
    {
        [TestFixture]
        public class ContainsRequiredColumns
        {
            [Test]
            public void ValidTable()
            {
                var value = new DataTable();

                value.Columns.Add("ColumnOne");
                value.Columns.Add("ColumnTwo");

                string errorMessage;

                value.ContainsRequiredColumns(out errorMessage, "ColumnOne", "ColumnTwo").Should().BeTrue();

                errorMessage.Should().BeNull();
            }

            [Test]
            public void OneColumnMissing()
            {
                var value = new DataTable();

                value.Columns.Add("ColumnOne");

                var requiredColumns = new[] {"ColumnOne", "ColumnTwo"};

                string errorMessage;

                value.ContainsRequiredColumns(out errorMessage, requiredColumns).Should().BeFalse();

                var expectedErrorMessage = string.Format(
                    "The table {0} did not contain the required column: ColumnTwo.", value.TableName);

                errorMessage.Should().Be(expectedErrorMessage);
            }

            [Test]
            public void TwoColumnsMissing()
            {
                var value = new DataTable();

                var requiredColumns = new[] {"ColumnOne", "ColumnTwo"};

                string errorMessage;

                value.ContainsRequiredColumns(out errorMessage, requiredColumns).Should().BeFalse();

                var expectedErrorMessage =
                    string.Format("The table {0} did not contain any of the required columns: {1}.", value.TableName,
                                  string.Join(", ", requiredColumns));

                errorMessage.Should().Be(expectedErrorMessage);
            }
        }
    }
}