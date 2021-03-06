﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Git.hub;

namespace Github3
{
    public partial class OAuth : Form
    {
        public OAuth()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            try
            {
                webBrowser1.ScriptErrorsSuppressed = true;
                webBrowser1.CausesValidation = false;
                string url = "https://github.com/login/oauth/authorize?client_id=" + GithubAPIInfo.client_id + "&scope=repo,public_repo";
                webBrowser1.Navigate(url);
            }
            catch (NullReferenceException)
            {
                MessageBox.Show(this, "Failure starting WebBrowser.");
            }
        }

        private bool _gotToken = false;

        public void web_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            checkAuth(e.Url.ToString());
        }

        public void web_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            checkAuth(e.Url.ToString());
        }

        static private Dictionary<string, string> GetParams(string uri)
        {
            var matches = Regex.Matches(uri, @"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);
            return matches.Cast<Match>().ToDictionary(
                m => Uri.UnescapeDataString(m.Groups[2].Value),
                m => Uri.UnescapeDataString(m.Groups[3].Value));
        }

        public void checkAuth(string url)
        {
            if (_gotToken)
            {
                return;
            }

            if (url.Contains("?code="))
            {
                Uri uri = new Uri(url);
                var queryParams = GetParams(uri.Query);
                if (queryParams.TryGetValue("code", out var code))
                {
                    Hide();
                    Close();
                    string token = OAuth2Helper.requestToken(GithubAPIInfo.client_id, GithubAPIInfo.client_secret, code);
                    if (token == null)
                    {
                        return;
                    }

                    _gotToken = true;

                    GithubLoginInfo.OAuthToken = token;

                    MessageBox.Show(Owner as IWin32Window, "Successfully retrieved OAuth token.", "Github Authorization");
                }
            }
        }
    }
}
