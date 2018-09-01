﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IpgwCore.MVVMBase;
using IpgwCore.ViewModel;
using IpgwCore.Controls.AreaWindow;
using IpgwCore.Services.MessageServices;
using IpgwCore.Services.HttpServices;
using IpgwCore.Controls.Dialogs;
using IpgwCore.View;
using IpgwCore.Controls.FlowControls;
using IpgwCore.Controls;

namespace IpgwCore {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YT_Window {
        private MainPageViewModel _mpvm;
        private YT_TitleBar _titlebar;
        bool _oa, _ob;

        public MainWindow() {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e) {
            _mpvm = DataContext as MainPageViewModel;
            _mpvm.CommandOperation += _mpvm_CommandOperation;
            _titlebar = (YT_TitleBar)GetTemplateChild("TitleBar");
            _titlebar.CloseCommand.Execution += CloseCommand_Execution;
        }

        private void CloseCommand_Execution(object para = null) {
            if (!Properties.Settings.Default.ExitAsk)
            {
                _oa = Properties.Settings.Default.ExitAsk;
                _ob = Properties.Settings.Default.ExitArea;
                _mpvm.ExitTip = Properties.Settings.Default.ExitArea ? "是否以托盘状态运行?" : "是否直接退出?";
                YT_FormDialog dialog = new YT_FormDialog { Style = App.Current.Resources["ExitDialog"] as Style };
                dialog.CancelAction += Dialog_NoAction;
                dialog.YesAction += Dialog_YesAction;
                dialog.NoAction += Dialog_NoAction;
                dialog.ShowDialog(this);
            }
            else
            {
                if (Properties.Settings.Default.ExitArea)
                    Hide();
                else
                {
                    Properties.Settings.Default.Save();
                    App.Current.Shutdown();
                }

            }
        }

        private void Dialog_NoAction(object para = null) {
            Properties.Settings.Default.ExitAsk = _oa;
            Properties.Settings.Default.ExitArea = _ob;
        }

        private void Dialog_YesAction(object para = null) {
            if (Properties.Settings.Default.ExitArea)
                Hide();
            else
            {
                Properties.Settings.Default.Save();
                Application.Current.Shutdown();
            }
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            int a = wParam.ToInt32();
            int b = lParam.ToInt32();
            switch (b)
            {
                case 1708:
                    PopupMessageServices.Instence.MainWindowVisibility = Visibility.Hidden;
                    break;
                case 6800:
                    PopupMessageServices.Instence.MainWindowVisibility = Visibility.Visible;
                    break;
            }
            //switch (a)
            //{
            //    case Win32Funcs.W_HIDE:
            //        PopupMessageServices.Instence.MainWindowVisibility = App.Current.MainWindow.Visibility;
            //        break;
            //}
            return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
        }

        private void _mpvm_CommandOperation(object sender, CommandArgs args) {

            switch (args.Command)
            {
                case "CancelCommand":
                    break;
            }
        }
    }
}
