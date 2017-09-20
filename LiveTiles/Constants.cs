namespace LiveTiles
{
    /// <summary>
    /// Static class for containing constants for the app
    /// </summary>
    public static class Constants
    {
        public const string LoadingTxt = "Loading...";
        public const string EmptyEmailTxt = "Please input your email.";
        public const string InvalidEmailTxt = "Your email is not vaild.";

        public const string JsHideBottomBar = "var style = document.createElement('style'); style.innerHTML = '{0}'; document.head.appendChild(style);$('#suiteBarToggle').append('<i style=\"color:#000; float: right; font-size: 24px;\" class=\"show-ribbon fa fa-caret-down\" aria-hidden=\"true\"></i><i style=\"color:#000; float: right; font-size: 24px; display: none;\" class=\"hide-ribbon fa fa-caret-up\" aria-hidden=\"true\"></i>');\n$('#suiteBarDelta').hide();\n$('#s4-ribbonrow').hide();\n$('#s4-workspace').height($('#s4-workspace').height()+90);";
        public const string CssHideTopBar = "@media only screen and (max-width: 748px) {#suiteBarDelta, #s4-ribbonrow{display:none;}}";
        public const string JsFillEmail = "javascript:document.getElementById('cred_userid_inputtext').value ='{0}';";
        public const string LoginUrl = "login.microsoftonline.com";
    }
}
