Pvc.RequireJS.InlineHtml
========================

PVC Build plugin for RequireJS projects that inlines HTML files inside a single .js file where the views are return as RequireJS-defined strings.

**Before**:<br/>
  (first.html): <code>&lt;span&gt;hello, world!&lt;/span&gt;</code><br />
  (second.html): <code>&lt;span class="test"&gt;testing second!&lt;/span&gt;</code>


**After**: a single stream containing the following string:<br/>
  <code>"define("text!views/first.html", [], function() { return '&lt;span&gt;hello, world!&lt;/span&gt;' })
  define("text!views/second.html", [], function() { return '&lt;span class="test"&gt;testing, second!&lt;/span&gt;' })"</code>
