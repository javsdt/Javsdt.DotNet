
namespace Javsdt.Wpf.Utils
{
    class FileDialogHelper
    {

        // https://learn.microsoft.com/zh-cn/dotnet/api/system.windows.forms.openfiledialog?view=windowsdesktop-8.0
        public static string ShowFolderBrowserDialog()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            // 设置对话框的属性，如根文件夹、显示描述等
            dialog.RootFolder = Environment.SpecialFolder.Desktop;
            dialog.Description = "请选择文件夹";

            // 打开文件夹选择对话框
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            // 处理对话框的结果
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            else
            {
                return null; // 用户取消了选择
            }
        }

    }
}
