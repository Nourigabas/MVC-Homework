using System.Text.RegularExpressions;

namespace Homework_TV_MVC.Transformer
{
    public class SlugParameterTransformer : IOutboundParameterTransformer
    {
        //استبدال نص بآخر
        //اول محارف بآخرى
        public string? TransformOutbound(object? value)
        {
            if (value is not string)
                return null;
            var result = Regex.Replace(value.ToString(), "[^a-zA-Z ]+", "-", RegexOptions.CultureInvariant, TimeSpan.FromMicroseconds(500)).Trim('-');
            return result;
        }
    }
}