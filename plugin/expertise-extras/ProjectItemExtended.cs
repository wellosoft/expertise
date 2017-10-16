using System;
using System.Collections.Generic;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using YamlDotNet.Serialization;
using System.Composition;

namespace expertise_extras
{
    public class ProjectItemExtended : DfmCustomizedRendererPartBase<IMarkdownRenderer, MarkdownCodeBlockToken, MarkdownBlockContext>
    {
        public override string Name => "ProjectItemExtended";

        public override bool Match(IMarkdownRenderer renderer, MarkdownCodeBlockToken token, MarkdownBlockContext context) => token.Lang == "proj";

        Deserializer dezert = new DeserializerBuilder().IgnoreUnmatchedProperties().Build();

        public override StringBuffer Render(IMarkdownRenderer renderer, MarkdownCodeBlockToken token, MarkdownBlockContext context)
        {
            // YAML 1.1 begin with --- and ending with ...
            var meta = dezert.Deserialize<ProjMeta>("---\r\n" + token.Code + "\r\n...");

            // Automatically locate image
            if (string.IsNullOrEmpty(meta.image))
            {
                meta.image = "images/" + meta.tag.ToLower() + "-" + meta.title.ToLower().Replace(" ", string.Empty) + ".jpg";
            }

            // HTML content for project
            StringBuffer result = "<div class=\"projects t-" + meta.tag + " s-" + meta.stat + "\">";
            result += "<img src=\"" + meta.image + "\">";
            result += "<div class=\"context\">";
            result += "<h3>" + meta.title + "</h3>";
            result += "<para>" + meta.desc + "</para>";
            result += "</div><ul>";
            Concat(ref result, "pop", meta.pop, "Visit project site");
            Concat(ref result, "git", meta.git, "View code in GitHub");
            Concat(ref result, "art", meta.art, "View art in ArtStation");
            Concat(ref result, "down", meta.down, "Precompiled download available");
            Concat(ref result, "yt", meta.yt, "Youtube demo available");
            Concat(ref result, "doc", meta.doc, "Documentation available");
            Concat(ref result, "comm", meta.comm, "Forum thread available");
            result += "</ul>";
            result += "<pre>" + meta.date + " - " + meta.tag + " - " + statCodes[meta.stat] + "</pre>";
            return result + "</div>";
        }

        // Sub html for buttons
        void Concat(ref StringBuffer result, string topic, string link, string alt)
        {
            if (!string.IsNullOrEmpty(link))
            {
                result += "<li><a href=\"";
                result += link;
                result += "\"><img src=\"";
                result += "~/images/btn-" + topic + ".png";
                result += "\" title=\"";
                result += alt;
                result += "\"></a></li>";
            }
        }

        // Predefined status code
        static string[] statCodes =
        {
            "<x class=\"x0\">Active</x>",
            "<x class=\"x1\">In Progress</x>",
            "<x class=\"x2\">Archived</x>",
            "<x class=\"x3\">Deprecated</x>",
            "<x class=\"x4\">Abandoned</x>",
        };
    }

    // Project struct
    [Serializable]
    public class ProjMeta
    {
        public string image { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string tag { get; set; }
        public string date { get; set; }
        public int stat { get; set; }
        public string git { get; set; }
        public string comm { get; set; }
        public string doc { get; set; }
        public string down { get; set; }
        public string yt { get; set; }
        public string pop { get; set; }
        public string art { get; set; }
    }

    // Dummy?
    public enum StatCodes
    {
        Active = 0,
        InProgress = 1,
        Archived = 2,
        Deprecated = 3,
        Abandoned = 4,
    }

    // Export mechanism that required by DocFX
    [Export(typeof(IDfmCustomizedRendererPartProvider))]
    public class ProjectItemExpandedProvider : IDfmCustomizedRendererPartProvider
    {
        public IEnumerable<IDfmCustomizedRendererPart> CreateParts(IReadOnlyDictionary<string, object> parameters)
        {
            yield return new ProjectItemExtended();
        }
    }
}
