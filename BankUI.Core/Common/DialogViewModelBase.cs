using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankUI.Core.Common
{
    /// <summary>
    /// ViewModel для диалоговых окон
    /// </summary>
    public class DialogViewModelBase : BindableBase,IDialogAware
    {
        public DialogViewModelBase() { }

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public virtual string Title => "";

        /// <summary>
        /// Возникает при закрытии диалогового окна
        /// </summary>
        public event Action<IDialogResult> RequestClose;


        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        /// <summary>
        /// true когда диалоговое окно может быть закрыто, false иначе
        /// </summary>
        /// <returns></returns>
        public virtual bool CanCloseDialog() =>
            true;


        /// <summary>
        /// Вызывается при закрытии диалогового окна
        /// </summary>
        public virtual void OnDialogClosed() { }

        /// <summary>
        /// Вызывается при открытии диалогового окна
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnDialogOpened(IDialogParameters parameters) { }
    }
}
