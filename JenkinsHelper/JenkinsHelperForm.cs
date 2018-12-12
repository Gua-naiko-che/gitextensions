using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [SuppressMessage("ReSharper", "LocalizableElement")]
    public partial class JenkinsHelperForm : Form
    {
        private readonly JenkinsHelperSettings _settings;

        private readonly string[] _actions = { "New", "Update", "Delete" };

        private HttpClient _client;

        public HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    string encoded = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_settings.JenkinsUsername}:{_settings.JenkinsPassword}"));
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);
                }

                return _client;
            }
        }

        public JenkinsHelperForm(JenkinsHelperSettings settings, IGitModule gitModule)
        {
            _settings = settings;
            InitializeComponent();

            string[] branchNames = gitModule.GetRefs(false).Select(b => b.Name).OrderBy(b => b).ToArray();
            cbBranch.DataSource = branchNames;
            cbBranch.SelectedItem = gitModule.GetSelectedBranch();

            cbAction.DataSource = _actions;
            cbAction.SelectedIndex = 0;
            cbAction.SelectedValueChanged += cbAction_SelectedIndexChanged;

            txtGitRepository.Text = _settings.GitRepository;

            txtTenantName.Text = _settings.TenantName;

            txtBackupPath.Text = _settings.BackupPath;

            ToggleOnDemandCheckboxEnablement();
        }

        private void cbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleOnDemandCheckboxEnablement();
        }

        private void ToggleOnDemandCheckboxEnablement()
        {
            chkOnDemand.Enabled = !string.IsNullOrWhiteSpace(_settings.JenkinsOnDemandUrl)
                                 && cbAction.SelectedValue.ToString() != "Delete";

            if (!chkOnDemand.Enabled)
            {
                chkOnDemand.Checked = false;
            }
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

            if (AreBuildParametersIncomplete())
            {
                MessageBox.Show("The build parameters should be completed to proceed.", "Build parameters incomplete", MessageBoxButtons.OK);

                return;
            }

            string resultMessage;
            try
            {
                HttpResponseMessage buildResponse = await LaunchBuildAsync();

                if (buildResponse.IsSuccessStatusCode)
                {
                    if (chkOnDemand.Checked)
                    {
                        HttpResponseMessage onDemandResponse = await LaunchOnDemandAsync();

                        resultMessage = onDemandResponse.IsSuccessStatusCode
                            ? "The operation was performed successfully."
                            : $"An error happened: {onDemandResponse.ReasonPhrase}";
                    }

                    resultMessage = "The operation was performed successfully.";
                }
                else
                {
                    resultMessage = $"An error happened: {buildResponse.ReasonPhrase}";
                }
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

        private bool IsConfigurationIncomplete()
        {
            return string.IsNullOrWhiteSpace(_settings.JenkinsUsername)
                   || string.IsNullOrWhiteSpace(_settings.JenkinsPassword)
                   || string.IsNullOrWhiteSpace(_settings.JenkinsDeployUrl);
        }

        private bool AreBuildParametersIncomplete()
        {
            return string.IsNullOrWhiteSpace(txtGitRepository.Text)
                   || string.IsNullOrWhiteSpace(txtTenantName.Text)
                   || string.IsNullOrWhiteSpace(txtBackupPath.Text);
        }

        private async Task<HttpResponseMessage> LaunchBuildAsync()
        {
            FormUrlEncodedContent buildContent = GetBuildContent();

            return await Client.PostAsync($"{_settings.JenkinsDeployUrl}buildWithParameters", buildContent);
        }

        private FormUrlEncodedContent GetBuildContent()
        {
            string selectedBranch = cbBranch.SelectedValue.ToString();
            string selectedAction = cbAction.SelectedValue.ToString();

            var values = new Dictionary<string, string>
            {
                ["EnvironmentName"] = new Regex("[^a-zA-Z0-9]").Replace(selectedBranch, ""),
                ["EnvironmentOperation"] = selectedAction,
                ["CareerRepository"] = txtGitRepository.Text,
                ["CareerBranch"] = selectedAction != "Delete" ? selectedBranch : "",
                ["TenantName"] = selectedAction == "New" ? txtTenantName.Text : "",
                ["TenantKind"] = selectedAction == "New" ? "Career" : "Recruiting",
                ["CareerBackupPath"] = selectedAction == "New" ? txtBackupPath.Text : ""
            };

            var content = new FormUrlEncodedContent(values);

            return content;
        }

        private async Task<HttpResponseMessage> LaunchOnDemandAsync()
        {
            FormUrlEncodedContent content = GetOnDemandContent();

            return await Client.PostAsync($"{_settings.JenkinsOnDemandUrl}buildWithParameters", content);
        }

        private FormUrlEncodedContent GetOnDemandContent()
        {
            var values = new Dictionary<string, string>
            {
                ["REPO"] = txtGitRepository.Text,
                ["BRANCH"] = cbBranch.SelectedValue.ToString(),
                ["CONTINUE_ON_ERROR"] = "false",
                ["BUILD_CONFIGURATION"] = "Release",
            };

            var content = new FormUrlEncodedContent(values);

            return content;
        }
    }
}