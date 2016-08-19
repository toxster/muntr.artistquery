using System;
using Xunit;
using Xunit.Abstractions;
using Muntr.Business.Misc;

namespace Muntr.Tests
{
    public class UtilsTests
    {
        private readonly ITestOutputHelper output;
        public UtilsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestGetWikipediaArticleStub()
        {
            var s = @"http://wikipedia.com/article/Nirvana_(band)";
            Assert.Equal("Nirvana_(band)", HelpUtils.GetWikipediaArticleStub(s));
        }

        [Fact]
        public void TestGetWikipediaArticleStubEmptyString()
        {
            var s = @"";
            Assert.Equal("", HelpUtils.GetWikipediaArticleStub(s));
        }


    }
}
