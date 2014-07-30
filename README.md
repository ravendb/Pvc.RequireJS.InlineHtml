Pvc.RequireJS.InlineHtml
========================

PVC Build plugin for RequireJS projects that inlines HTML files inside a single .js file where the views are return as RequireJS-defined strings.

*Before*:
  (first.html): <span>hello, world!</span>
  (second.html): <span class="test">testing second!</span>


*After*: a single stream containing the following string:
  "define("text!views/first.html", [], function() { return '<span>hello, world!</span>' })
  define("text!views/second.html", [], function() { return '<span class="test">testing, second!</span>' })"
