﻿using System;
using System.Windows.Forms;
using Bloom.Properties;
//using Bloom.SendReceive;
using Bloom.Workspace;
using L10NSharp;
using SIL.Reporting;
using System.Drawing;
using SIL.Windows.Forms.SettingProtection;

namespace Bloom.CollectionTab
{
	public partial class LibraryView :  UserControl, IBloomTabArea
	{
		private readonly LibraryModel _model;


		private LibraryListView _collectionListView;
		private LibraryBookView _bookView;

		public LibraryView(LibraryModel model, LibraryListView.Factory libraryListViewFactory,
			LibraryBookView.Factory templateBookViewFactory,
			SelectedTabChangedEvent selectedTabChangedEvent,
			SendReceiveCommand sendReceiveCommand)
		{
			_model = model;
			InitializeComponent();

			_toolStrip.Renderer = new NoBorderToolStripRenderer();

			_collectionListView = libraryListViewFactory();
			_collectionListView.Dock = DockStyle.Fill;
			splitContainer1.Panel1.Controls.Add(_collectionListView);

			_bookView = templateBookViewFactory();
			_bookView.Dock = DockStyle.Fill;
			splitContainer1.Panel2.Controls.Add(_bookView);

			splitContainer1.SplitterDistance = _collectionListView.PreferredWidth;
			_makeBloomPackButton.Visible = model.IsShellProject;
			_sendReceiveButton.Visible = Settings.Default.ShowSendReceive;

			_leftToolStrip.Renderer  = new BorderlessToolStripRenderer();

			if (sendReceiveCommand != null)
			{
#if Chorus
				_sendReceiveButton.Click += (x, y) => sendReceiveCommand.Raise(this);
				_sendReceiveButton.Enabled = !SendReceiver.SendReceiveDisabled;
#endif
			}
			else
				_sendReceiveButton.Enabled = false;

			if (SIL.PlatformUtilities.Platform.IsMono)
			{
				BackgroundColorsForLinux();
			}

			selectedTabChangedEvent.Subscribe(c=>
												{
													if (c.To == this)
													{
														Logger.WriteEvent("Entered Collections Tab");
													}
												});
		}

		internal void ManageSettings(SettingsProtectionHelper settingsLauncherHelper)
		{
			//we have a couple of buttons which don't make sense for the remote (therefore vulnerable) low-end user
			settingsLauncherHelper.ManageComponent(_settingsButton);

			//NB: this isn't really a setting, but we're using that feature to simplify this menu down to what makes sense for the easily-confused user
			settingsLauncherHelper.ManageComponent(_openCreateCollectionButton);
		}

		private void BackgroundColorsForLinux() {

			// Set the background image for Mono because the background color does not paint,
			// and if we override the background paint handler, the default styling of the child
			// controls is changed.

			// We are getting an exception if none of the buttons are visible. The tabstrip is set
			// to Dock.Top which results in the height being zero if no buttons are visible.
			if ((_toolStrip.Height == 0) || (_toolStrip.Width == 0)) return;

			var bmp = new Bitmap(_toolStrip.Width, _toolStrip.Height);
			using (var g = Graphics.FromImage(bmp))
			{
				using (var b = new SolidBrush(_toolStrip.BackColor))
				{
					g.FillRectangle(b, 0, 0, bmp.Width, bmp.Height);
				}
			}
			_toolStrip.BackgroundImage = bmp;
		}

		public string CollectionTabLabel
		{
			get { return LocalizationManager.GetString("CollectionTab.CollectionTabLabel","Collections"); }//_model.IsShellProject ? "Shell Collection" : "Collection"; }

		}


		private void OnMakeBloomPackButton_Click(object sender, EventArgs e)
		{
			_collectionListView.MakeBloomPack(false);
		}

		public string HelpTopicUrl
		{
			get
			{
				if (_model.IsShellProject)
				{
					return "/Tasks/Source_Collection_tasks/Source_Collection_tasks_overview.htm";
				}
				else
				{
					return "/Tasks/Vernacular_Collection_tasks/Vernacular_Collection_tasks_overview.htm";
				}
			}
		}

		public Control TopBarControl
		{
			get { return _topBarControl; }
		}

		/// <summary>
		/// TopBarControl.Width is not right here, because (a) the Send/Receive button currently never shows, and
		/// (b) the Make Bloompack button only shows in source collections.
		/// </summary>
		public int WidthToReserveForTopBarControl
		{
			get
			{
				if (_makeBloomPackButton.Visible)
					// The should be the distance from the left of the TopBarControl to the right of the makeBloomPack button
					return _makeBloomPackButton.Bounds.Right + _toolStrip.Left;
				else
					return _leftToolStrip.Width;
			}
		}

		public Bitmap ToolStripBackground { get; set; }

		private WorkspaceView GetWorkspaceView()
		{
			Control ancestor = Parent;
			while (ancestor != null && !(ancestor is WorkspaceView))
				ancestor = ancestor.Parent;
			return ancestor as WorkspaceView;
		}

		private void _settingsButton_Click(object sender, EventArgs e)
		{
			GetWorkspaceView().OnSettingsButton_Click(sender, e);
		}

		private void _openCreateCollectionButton_Click(object sender, EventArgs e)
		{
			GetWorkspaceView().OpenCreateLibrary();
		}
	}

	// Without this we get a white border we don't want.
	// Thanks to https://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c
	internal class BorderlessToolStripRenderer : ToolStripSystemRenderer
	{
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			// we don't want a border!
		}
	}
}
