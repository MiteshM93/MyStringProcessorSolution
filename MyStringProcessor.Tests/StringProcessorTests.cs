using NUnit.Framework;
using Shouldly;
using MyStringProcessor;
using System.Collections.Generic;

namespace MyStringProcessor.Tests
{
    [TestFixture]
    public class StringProcessorTests
    {
        [Test]
        public void ProcessStrings_InputIsNull_ReturnsEmptyCollection()
        {
            var result = StringProcessor.ProcessStrings(null);
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Test]
        public void ProcessStrings_InputIsEmpty_ReturnsEmptyCollection()
        {
            var result = StringProcessor.ProcessStrings(new List<string>());
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Test]
        public void ProcessStrings_InputContainsEmptyStrings_ReturnsEmptyCollection()
        {
            var result = StringProcessor.ProcessStrings(new List<string> { "", "  ", null });
            result.ShouldNotBeNull();
            result.ShouldBeEmpty();
        }

        [Test]
        public void ProcessStrings_NormalInputString_ProcessesCorrectly()
        {
            var input = new List<string> { "AAAc91%cWwWkLq$1ci3_848v3d__K" };
            var result = StringProcessor.ProcessStrings(input);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].ShouldBe("Ac91%cWkLq£1ci3");
        }

        [Test]
        public void ProcessStrings_InputWithSpecialChars_ProcessesCorrectly()
        {
            var input = new List<string> { "AA$$$c__44_LK" };
            var result = StringProcessor.ProcessStrings(input);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].ShouldBe("A£cLK");
        }

        [Test]
        public void ProcessStrings_InputWithContiguousDuplicates_ProcessesCorrectly()
        {
            var input = new List<string> { "AABBBccDDee" };
            var result = StringProcessor.ProcessStrings(input);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].ShouldBe("ABcDe");
        }

        [Test]
        public void ProcessStrings_InputWithTruncation_ProcessesCorrectly()
        {
            var input = new List<string> { "A12345678901234567890" };
            var result = StringProcessor.ProcessStrings(input);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(1);
            result[0].ShouldBe("A12345678901234");
        }

        [Test]
        public void ProcessStrings_MixedInput_ProcessesCorrectly()
        {
            var input = new List<string> { "1234567890123456", "AA___BB$$$CC" };
            var result = StringProcessor.ProcessStrings(input);
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result[0].ShouldBe("123456789012345");
            result[1].ShouldBe("A£BCC");
        }
    }
}
