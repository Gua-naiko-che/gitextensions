using System.Collections.Generic;

namespace JenkinsHelper
{
    using GitUIPluginInterfaces;
    using ResourceManager;

    public class JenkinsHelperPlugin : GitPluginBase, IGitPluginForRepository
    {
        private readonly StringSetting _jenkinsUsername = new StringSetting("Jenkins username", string.Empty);
        private readonly PasswordSetting _jenkinsPassword = new PasswordSetting("Jenkins password", string.Empty);
        private readonly StringSetting _jenkinsDeployUrl = new StringSetting("Jenkins deploy URL", string.Empty);
        private readonly StringSetting _jenkinsOnDemandUrl = new StringSetting("Jenkins on demand URL", string.Empty);
        private readonly StringSetting _gitRepository = new StringSetting("Git repository", string.Empty);
        private readonly StringSetting _tenantName = new StringSetting("Tenant name", string.Empty);
        private readonly StringSetting _backupPath = new StringSetting("Backup path", string.Empty);

        public JenkinsHelperPlugin()
        {
            SetNameAndDescription("Jenkins Helper");
            Translate();
        }

        public override IEnumerable<ISetting> GetSettings()
        {
            yield return _jenkinsUsername;
            yield return _jenkinsPassword;
            yield return _jenkinsDeployUrl;
            yield return _jenkinsOnDemandUrl;
            yield return _gitRepository;
            yield return _tenantName;
            yield return _backupPath;
        }

        public override bool Execute(GitUIBaseEventArgs args)
        {
            var settings = new JenkinsHelperSettings
            {
                JenkinsUsername = _jenkinsUsername.ValueOrDefault(Settings),
                JenkinsPassword = _jenkinsPassword.ValueOrDefault(Settings),
                JenkinsDeployUrl = _jenkinsDeployUrl.ValueOrDefault(Settings),
                JenkinsOnDemandUrl = _jenkinsOnDemandUrl.ValueOrDefault(Settings),
                GitRepository = _gitRepository.ValueOrDefault(Settings),
                TenantName = _tenantName.ValueOrDefault(Settings),
                BackupPath = _backupPath.ValueOrDefault(Settings),
            };

            using (var frm = new JenkinsHelperForm(settings, args.GitModule))
            {
                frm.ShowDialog(args.OwnerForm);
            }

            return true;
        }
    }
}