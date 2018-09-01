﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using IpgwCore.MVVMBase;
using IpgwCore.Services.MessageServices;
using PMS = IpgwCore.Services.MessageServices.PopupMessageServices;

namespace IpgwCore.ViewModel {
    internal class MainPageViewModel : ViewModelBase, IMessagePoster {
        #region Properties
        public object _message { get => Msg; set => Msg = value.ToString(); }

        private string _msg;
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg {
            get => _msg;
            set => SetValue(out _msg, value, Msg);
        }

        private string _exittip;
        /// <summary>
        /// 退出提示
        /// </summary>
        public string ExitTip {
            get => _exittip;
            set => SetValue(out _exittip, value, ExitTip);
        }

        public CommandBase CancelCommand { get; set; }

        public event CommandActionEventHandler CommandOperation;

        #endregion

        #region Methods
        private void InitCommand() {
            CancelCommand = new CommandBase(obj => CommandOperation?.Invoke(this, new CommandArgs(obj, "CancelCommand")));
        }

        #endregion

        #region Constructors
        public MainPageViewModel() {
            PMS.Instence.MessageHolder = this;
            InitCommand();
        }
        #endregion
    }

}
