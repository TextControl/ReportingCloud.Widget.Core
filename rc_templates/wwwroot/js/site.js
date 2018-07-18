var textControl1;
var loadedDocument;
var streamType;

// create a new Text Control Widget instance
window.onload = function () {
    textControl1 = new TXTextControlWeb("myWidgetContainer");
};

function LoadDocument(template) {
    var serviceURL = "/ReportingCloud/Template?TemplateName=" + template;

    // call the "GET Template" controller method with a template name
    $.ajax({
        type: "GET",
        url: serviceURL,
        contentType: 'application/json',
        success: successFunc,
        error: errorFunc
    });

    function successFunc(data, status) {

        streamType = TXTextControl.StreamType.InternalUnicodeFormat;

        // create the proper StreamType based on the extension
        if (template.endsWith("docx")) {
            streamType = TXTextControl.StreamType.WordprocessingML;
        }
        else if (template.endsWith("doc")) {
            streamType = TXTextControl.StreamType.MSWord;
        }
        else if (template.endsWith("rtf")) {
            streamType = TXTextControl.StreamType.RichTextFormat;
        }

        // load the document into the widget
        textControl1.loadDocument(streamType, data);

        // enable the save button and set heading
        $("#saveBtn").removeAttr("disabled");
        loadedDocument = template;
        $("#documentName").text(loadedDocument);
    }

    function errorFunc(data, success) {
        console.log(data);
    }
}

function SaveDocument() {

    textControl1.saveDocument(streamType, function (e) {

        var serviceURL = "/ReportingCloud/Template";

        // call the "POST Template" controller method
        $.ajax({
            type: "POST",
            url: serviceURL,
            contentType: 'application/json',
            data: JSON.stringify({
                Name: loadedDocument,
                Document: e.data
            }),
            success: successFunc,
            error: errorFunc
        });

        function successFunc(data, status) {
            alert("Document saved successfully");
        }

        function errorFunc(data, success) {
            console.log(data);
        }

    });
}

if (!String.prototype.endsWith) {
    String.prototype.endsWith = function (search, this_len) {
        if (this_len === undefined || this_len > this.length) {
            this_len = this.length;
        }
        return this.substring(this_len - search.length, this_len) === search;
    };
}