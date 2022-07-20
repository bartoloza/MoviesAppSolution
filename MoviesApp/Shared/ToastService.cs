using Blazored.Toast.Services;

namespace MoviesApp.Shared
{
    public class ToastService
    {
        private ToastLevel toastLevel;

        private void setAndShowNotification(ToastLevel tLevel)
        {
            toastLevel = ToastLevel.Info;

            switch (tLevel)
            {
                case ToastLevel.Success:
                    toastLevel = ToastLevel.Success;
                    break;
                case ToastLevel.Warning:
                    toastLevel = ToastLevel.Warning;
                    break;
                case ToastLevel.Info:
                    toastLevel = ToastLevel.Info;
                    break;
                case ToastLevel.Error:
                    toastLevel = ToastLevel.Error;
                    break;
            }
        }
    }
}
