using System;

namespace Muntr.Business.Misc
{
    public static class HelpUtils {
        
        public static string GetWikipediaArticleStub(string fullUrl) {
            if (String.IsNullOrEmpty(fullUrl))
                return String.Empty;
                
            var uri = new Uri(fullUrl);
            return uri.Segments[uri.Segments.Length-1];
        }



    }

}

