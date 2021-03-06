using System;
using Xunit;
using Xunit.Abstractions;
using Muntr.Business.Misc;
using RichardSzalay.MockHttp;
using Muntr.Server.Core;
using System.Net.Http;

namespace Muntr.Tests
{
    public class IntegrationTests
    {
        private readonly ITestOutputHelper output;
        public IntegrationTests(ITestOutputHelper output)
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

        [Fact]
        public void TestParseWikipediaExtract() {
            var mockHttp = new MockHttpMessageHandler();
            var IOptions<AppSettings> settings = 
            
            var httpClient = new HttpClient(mockHttp);

            var artistQueryRepository = new ArtistQueryRepository(null, )
        }


    }
}
