/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = 'vi';
    config.filebrowserBrowserUrl = '/Assets/admin/js/plugins/ckfinder/ckfinder.html';
    config.filebrowserImageBrowserUrl = '/Assets/admin/js/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowserUrl = '/Assets/admin/js/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/Assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpLoad&type=Files';
    config.filebrowserImageUploadUrl = '/Data/';
    config.filebrowserFlashUploadUrl = '/Assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpLoad&type=Flash';

    CKFinder.setupCKEditor(null, '/Assets/admin/js/plugins/ckfinder/');
};
