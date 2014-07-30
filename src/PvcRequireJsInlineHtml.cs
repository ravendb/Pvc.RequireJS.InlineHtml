using PvcCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvcPlugins
{
    /// <summary>
    /// Converts input HTML files into a string containing RequireJS module definitions.
    /// 
    /// Before:
    ///     (first.html): <span>hello, world!</span>
    ///     (second.html): <span class="test">testing second!</span>
    ///     
    /// After: a single stream containing:
    ///     "define("text!views/first.html", [], function() { return '<span>hello, world!</span>' })
    ///      define("text!views/second.html", [], function() { return '<span class="test">testing, second!</span>' })"
    /// </summary>
    public class PvcRequireJsInlineHtml : PvcPlugin
    {
        private readonly string output;
        private readonly Func<string, string> moduleNameGetter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output">The output stream name.</param>
        /// <param name="moduleNameGetter">Function that takes a stream name and returns the module name. Default will be a function that returns "some\\folder\\foo.html" and returns "text!foo.html".</param>
        public PvcRequireJsInlineHtml(
            string output = "views_merged.js",
            Func<string, string> moduleNameGetter = null)
        {
            this.output = output;
            this.moduleNameGetter = moduleNameGetter ?? PvcRequireJsInlineHtml.DefaultModuleNameGetter;
        }

        public override string[] SupportedTags
        {
            get
            {
                return new[] { ".html" };
            }
        }

        public override IEnumerable<PvcStream> Execute(IEnumerable<PvcStream> inputStreams)
        {
            var moduleDefinitions = inputStreams.Select(s => this.ConvertToInlineHtmlModule(s));
            var moduleDefinitionsString = string.Join("\r\n", moduleDefinitions);
            return new[] 
            { 
                PvcUtil.StringToStream(moduleDefinitionsString, this.output) 
            };
        }

        private string ConvertToInlineHtmlModule(PvcStream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var rawHtml = reader.ReadToEnd();
                var singleLineHtml = rawHtml
                    .Replace("\r\n", string.Empty)
                    .Replace("\n", string.Empty)
                    .Replace("'", "\\'") // Replace ' with \'
                    .Replace("\"", "\\\""); // Replace " with \"

                return string.Format("define(\"{0}\", [], function () {{ return '{1}'; }} )",
                    this.moduleNameGetter(stream.StreamName),
                    singleLineHtml);
            }
        }

        private static string DefaultModuleNameGetter(string streamName)
        {
            var fileName = Path.GetFileName(streamName);
            return "text!views/" + fileName;
        }
    }
}
