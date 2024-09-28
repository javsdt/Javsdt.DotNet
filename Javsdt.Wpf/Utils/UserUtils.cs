using System;
using System.IO;
using System.Windows.Forms;

namespace Javsdt.Utility
{
    public static class UserUtils
    {
        public static string ChooseDirectory()
        {
            // 请用户选择要整理的文件夹
            // 如果当前系统不支持对话框，则让用户输入
            // 返回值:
            // 文件夹完整路径
            Console.Write("请选择要整理的文件夹: ");
            for (int i = 0; i < 2; i++)
            {
                try
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    DialogResult dr = fbd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        string dirChoose = fbd.SelectedPath;
                        Console.WriteLine(dirChoose);
                        return dirChoose;
                    }
                    else
                    {
                        Console.WriteLine("你没有选择目录! 请重新选: ");
                        System.Threading.Thread.Sleep(2000);
                        continue;
                    }
                }
                catch (Exception) // 来自@BlueSkyBot
                {
                    try
                    {
                        Console.Write("请输入你需要整理的文件夹路径: ");
                        string dirChoose = Console.ReadLine();
                        if (!Directory.Exists(dirChoose))
                        {
                            Console.WriteLine($"不存在{dirChoose}当前目录或者输入错误，请重新输入！");
                            System.Threading.Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine(dirChoose);
                            return dirChoose;
                        }
                    }
                    catch (Exception)
                    {
                        Environment.Exit(0);
                    }
                }
            }
            Console.WriteLine("你可能不需要我了，请关闭我吧！");
            return null;
        }
    }
}
