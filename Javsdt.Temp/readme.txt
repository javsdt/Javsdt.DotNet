
未来：
1、User.settings的归类的标准要把“\”换成系统分隔符。去除末尾的分隔符
2、片商 还是 发行商
3、优化python InitDict
4、优化python file两个类为静态类
5、优化python，rename_mp4重写到FIleUtil中
6、优化python， 重命名失败，终止后续操作（除非是大小写失败，使用新命名继续操作），C# 使用旧命名继续
7、优化python， FileLathe classify_files 移动字幕 os.rename(jav_file.Path_subtitle, path_subtitle_new)  Path_subtitle不是原字幕路径
8、视频在根目录下的重命名独立文件夹


当前：
1、重命名视频和字幕文件，合到一个方法里
2、保存json不管前面步骤失败与否，都得进行
3、WriteNfo的写genres改tag

与Python的不同
1、write_nfo 写死了很多tag
2、裁剪方式用枚举
3、多个导演

Add-Migration InitialCreate -Project Javsdt.Data
Update-Database -Project Javsdt.Data


Arzon:
1、访问图片需要带Refer
2、普通请求不需要设置headers

