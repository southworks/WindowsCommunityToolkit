using System;
using System.Threading.Tasks;
using Microsoft.Toolkit.Services.Core;

namespace Microsoft.Toolkit.Services.PlatformSpecific.NetFramework
{
    internal class NetFrameworkAuthenticationBroker : IAuthenticationBroker
    {
        private PopupWPF popupForm;

        public Task<AuthenticationResult> Authenticate(Uri requestUri, Uri callbackUri)
        {
            var taskCompletionSource = new TaskCompletionSource<AuthenticationResult>();
            popupForm = new PopupWPF(callbackUri);
            popupForm.FormClosed += (sender, e) =>
            {
                var result = new AuthenticationResult();
                if (popupForm.actualUrl != null)
                {
                    var query = System.Web.HttpUtility.ParseQueryString(popupForm.actualUrl.Query);

                    result.ResponseData = query.ToString();
                    result.ResponseStatus = AuthenticationResultStatus.Success;
                }
                else
                {
                    result.ResponseStatus = AuthenticationResultStatus.ErrorHttp;
                }

                taskCompletionSource.SetResult(result);
                return false;
            };

            popupForm.Show();
            popupForm.navigateTo(requestUri.AbsoluteUri);

            return taskCompletionSource.Task;
        }
    }
}
