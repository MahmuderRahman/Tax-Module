
/// <reference path="grid.celledit.js" />
//This file should be used if you want to debug and develop
function CsvXlsXlsxReaderInclude() {
    var pathtojsfiles = "/Scripts/lib/XlsAndCsvToJson/"; // need to be ajusted
    // set include to false if you do not want some modules to be included
    var modules = [
        { include: true, incfile: 'js/papaparse.min.js' },
        { include: true, incfile: 'js/shim.js' },
        { include: true, incfile: 'js/cpexcel.js' },
        { include: true, incfile: 'js/xls.js' },
        { include: true, incfile: 'js/jszip.js' },
        { include: true, incfile: 'js/xlsx.js' },
        { include: true, incfile: 'js/dropsheet.js' },
        { include: true, incfile: 'js/main.js' },
	    { include: true, incfile: 'vendor/spin.js' },
        { include: true, incfile: 'exXslOrCsvToJson.js' }
    ];
    var filename;
    for (var i = 0; i < modules.length; i++) {
        if (modules[i].include === true) {
            filename = pathtojsfiles + modules[i].incfile;
            if (jQuery.browser.safari) {
                jQuery.ajax({ url: filename, dataType: 'script', async: false, cache: true });
            } else {
                if (jQuery.browser.msie) {
                    document.write('<script charset="utf-8" type="text/javascript" src="' + filename + '"></script>');
                } else {
                    IncludeJavaScript(filename);
                }
            }
        }
    }
    function IncludeJavaScript(jsFile) {
        var oHead = document.getElementsByTagName('head')[0];
        var oScript = document.createElement('script');
        oScript.setAttribute('type', 'text/javascript');
        oScript.setAttribute('language', 'javascript');
        oScript.setAttribute('src', jsFile);
        oHead.appendChild(oScript);
    }
}
CsvXlsXlsxReaderInclude();