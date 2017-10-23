using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using YamlDotNet.Serialization;
using System.Composition;
using System.Globalization;

namespace expertise_extras
{
    public class DateTimeCap : DfmCustomizedRendererPartBase<IMarkdownRenderer, MarkdownCodeInlineToken, MarkdownInlineContext>
    {
        public override string Name => "DateTimeCap";

        public override bool Match(IMarkdownRenderer renderer, MarkdownCodeInlineToken token, MarkdownInlineContext context)
        {
            return token.Content == "{datetime}";
        }

        public override StringBuffer Render(IMarkdownRenderer renderer, MarkdownCodeInlineToken token, MarkdownInlineContext context)
        {
            return DateTime.Now.ToString("MMM dd, yyyy hh:mm", CultureInfo.InvariantCulture);
        }
    }

    [Export(typeof(IDfmCustomizedRendererPartProvider))]
    public class DateTimeCapProvider : IDfmCustomizedRendererPartProvider
    {
        public IEnumerable<IDfmCustomizedRendererPart> CreateParts(IReadOnlyDictionary<string, object> parameters)
        {
            yield return new DateTimeCap();
        }
    }

}
