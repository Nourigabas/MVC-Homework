using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.RegularExpressions;
using TV_Domain;

namespace Homework_TV_MVC.TagHelpers
{
    [HtmlTargetElement("url-with-slug")]
    public class SlugTagHelper : AnchorTagHelper
    {
        public SlugTagHelper(IHtmlGenerator generator) : base(generator)
        { }

        public TVShow TVShow { get; set; }

        [HtmlAttributeName("for-TVShow-Id")]
        public Guid TVShowId { get; set; }

        [HtmlAttributeName("for-TVShow-slug")]
        public string slug { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            var slugs = Regex.Replace(slug, "[^a-zA-Z ]+", " ").Trim().Replace(" ", "-").ToLower();
            RouteValues.Add("slug", slugs);
            RouteValues.Add("TVShowId", TVShowId.ToString());
            base.Process(context, output);
        }
    }
}