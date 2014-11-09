using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Aav.Validation.Tests
{
    public class StringExtensionTests
    {
        [TestFixture]
        public class IsValidPath
        {
            [Test]
            public void ValidPath()
            {
                @"c:\".IsValidPath().Should().BeTrue();
            }

            [Test]
            public void InvalidPath()
            {
                string.Join(string.Empty, Path.GetInvalidPathChars()).IsValidPath().Should().BeFalse();
            }
        }

        [TestFixture]
        public class IsValidFileName
        {
            [Test]
            public void ValidFileName()
            {
                @"test.txt".IsValidFileName().Should().BeTrue();
            }

            [Test]
            public void InvalidFileName()
            {
                string.Join(string.Empty, Path.GetInvalidFileNameChars()).IsValidFileName().Should().BeFalse();
            }
        }

        [TestFixture]
        public class IsValidEmailAddress
        {
            [Test]
            public void ValidEmailAddress()
            {
                @"steve@mac.com".IsValidEmailAddress().Should().BeTrue();
            }

            [Test]
            public void InvalidFileName()
            {
                string.Empty.IsValidEmailAddress().Should().BeFalse();
            }
        }
    }
}