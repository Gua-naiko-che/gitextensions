using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitUIPluginInterfaces;

namespace JenkinsHelper
{
    public partial class JenkinsHelperForm : Form
    {
        private readonly JenkinsHelperSettings _settings;

        private readonly string[] _actions = { "New", "Update", "Delete" };

        public JenkinsHelperForm(JenkinsHelperSettings settings, IGitModule gitModule)
        {
            _settings = settings;
            InitializeComponent();

            string[] branchNames = gitModule.GetRefs(false).Select(b => b.Name).OrderBy(b => b).ToArray();
            cbBranch.DataSource = branchNames;
            cbBranch.SelectedItem = gitModule.GetSelectedBranch();

            cbAction.DataSource = _actions;
            cbAction.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnBuild_ClickAsync(object sender, EventArgs e)
        {
            if (IsConfigurationIncomplete())
            {
                MessageBox.Show("The configuration should be completed in the settings panel to proceed.", "Configuration incomplete", MessageBoxButtons.OK);
                return;
            }

            string resultMessage;
            try
            {
                HttpResponseMessage response = await BuildAsync();
                resultMessage = response.IsSuccessStatusCode ? "The operation was performed successfully." : $"An error happened: {response.ReasonPhrase}";
            }
            catch (HttpRequestException)
            {
                resultMessage = "An error occurred while connecting with the Jenkins server. Please verify that it is accessible.";
            }
            catch (Exception exception)
            {
                resultMessage = exception.ToString();
            }

            if (MessageBox.Show(resultMessage, "Build result", MessageBoxButtons.OK) == DialogResult.OK)
            {
                Close();
            }
        }

        private async Task<HttpResponseMessage> BuildAsync()
        {
            HttpClient client = CreateClient(_settings.JenkinsUsername, _settings.JenkinsPassword);
            FormUrlEncodedContent content = GetContent();

            return await client.PostAsync($"{_settings.JenkinsDeployUrl}buildWithParameters", content);
        }

        private bool IsConfigurationIncomplete()
        {
            return string.IsNullOrWhiteSpace(_settings.JenkinsUsername)
                   || string.IsNullOrWhiteSpace(_settings.JenkinsPassword)
                   || string.IsNullOrWhiteSpace(_settings.JenkinsDeployUrl)
                   || string.IsNullOrWhiteSpace(_settings.GitRepository)
                   || string.IsNullOrWhiteSpace(_settings.TenantName)
                   || string.IsNullOrWhiteSpace(_settings.BackupPath);
        }

        private static HttpClient CreateClient(string username, string password)
        {
            string encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);

            return client;
        }

        private FormUrlEncodedContent GetContent()
        {
            string selectedBranch = cbBranch.SelectedValue.ToString();
            string selectedAction = cbAction.SelectedValue.ToString();

            var values = new Dictionary<string, string>
            {
                ["EnvironmentName"] = new Regex("[^a-zA-Z0-9]").Replace(selectedBranch, ""),
                ["EnvironmentOperation"] = selectedAction,
                ["CareerRepository"] = _settings.GitRepository,
                ["CareerBranch"] = selectedAction != "Delete" ? selectedBranch : "",
                ["TenantName"] = selectedAction == "New" ? _settings.TenantName : "",
                ["TenantKind"] = selectedAction == "New" ? "Career" : "Recruiting",
                ["CareerBackupPath"] = selectedAction == "New" ? _settings.BackupPath : ""
            };

            var content = new FormUrlEncodedContent(values);

            return content;
        }
    }
}