﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IpgwCore {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        public static string RootPath = AppDomain.CurrentDomain.BaseDirectory;
        public const string WebConfig = @"\Configs\Web-config.xml";
        public const string FluxLog = @"\Configs\FluxLog.xml";
        public const string CourseData = @"\Configs\CourseData.xml";

        #region Methods
        private void Application_Startup(object sender, StartupEventArgs e) {
            App.Current.SessionEnding += Current_SessionEnding;
            MainWindow window = new MainWindow();
            window.Show();
            if (!e.Args.Length.Equals(0))
                window.Hide();
        }

        protected override void OnExit(ExitEventArgs e) {
            IpgwCore.Properties.Settings.Default.Save();
            base.OnExit(e);
        }

        private void Current_SessionEnding(object sender, SessionEndingCancelEventArgs e) {
            IpgwCore.Properties.Settings.Default.Save();
        }

        #endregion

        #region Contrustor
        public App() {
            Startup += Application_Startup;
        }
        #endregion
    }

    public class ConstTable {
        public const string IPGW = "NEUIpgw";
        public const string ZJW = "NEUZhjw";
        public const string PagePath = @"View\Pages\";
    }
}
