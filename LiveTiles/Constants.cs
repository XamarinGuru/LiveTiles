using System;
namespace LiveTiles
{
	public class Constants
	{
		public static string INJECT_JS_HIDE_BOTTOM_BAR = "var style = document.createElement('style'); style.innerHTML = '{0}'; document.head.appendChild(style);$('#suiteBarToggle').append('<i style=\"color:#000; float: right; font-size: 24px;\" class=\"show-ribbon fa fa-caret-down\" aria-hidden=\"true\"></i><i style=\"color:#000; float: right; font-size: 24px; display: none;\" class=\"hide-ribbon fa fa-caret-up\" aria-hidden=\"true\"></i>');\n$('#suiteBarDelta').hide();\n$('#s4-ribbonrow').hide();\n$('#s4-workspace').height($('#s4-workspace').height()+90);";
		public static string INJECT_CSS_HIDE_TOP_BAR = "@media only screen and (max-width: 748px) {#suiteBarDelta, #s4-ribbonrow{display:none;}}";
		public static string INJECT_JS_FILL_EMAIL = "javascript:document.getElementById('cred_userid_inputtext').value ='{0}';";
		public static string SYMBOL_LOGIN = "login.microsoftonline.com";
	}
}
