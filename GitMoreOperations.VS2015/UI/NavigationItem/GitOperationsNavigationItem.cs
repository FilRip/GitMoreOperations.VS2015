﻿using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TeamFoundation.Git.Extensibility;
using TeamExplorer.Common;
using System.Windows.Media;

namespace GitMoreOperations.VS2015.UI
{
    [TeamExplorerNavigationItem("A14BB4C5-1A56-4E67-A575-8531F4DE1F94", 1901)]
    public class GitOperationsNavigationItem : TeamExplorerBaseNavigationItem
    {
        private readonly IGitExt gitService;
        private readonly ITeamExplorer teamExplorer;

        [ImportingConstructor]
        public GitOperationsNavigationItem([Import(typeof (SVsServiceProvider))] IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            try
            {
                UpdateVisible();
                Text = "Git Operations";
                Image = Resources.LinkIcon;
                IsVisible = true;
                teamExplorer = GetService<ITeamExplorer>();
                gitService = (IGitExt) serviceProvider.GetService(typeof (IGitExt));
                teamExplorer.PropertyChanged += TeamExplorerOnPropertyChanged;
                ArgbColor = Color.FromRgb(0xAE, 0x3C, 0xBA).ToInt32();
                IsEnabled = true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void HandleException(Exception ex)
        {
            ShowNotification(ex.Message, NotificationType.Error);
        }

        protected override void ContextChanged(object sender, ContextChangedEventArgs e)
        {
            UpdateVisible();
            base.ContextChanged(sender, e);
        }

        private void TeamExplorerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateVisible();
        }

        private void UpdateVisible()
        {
            IsVisible = false;
            if (gitService != null && gitService.ActiveRepositories.Any())
            {
                IsVisible = true;
            }
        }

        public override void Execute()
        {
            teamExplorer.NavigateToPage(new Guid(GitOperationsGuids.GitOperationsPage), null);
        }
    }
}