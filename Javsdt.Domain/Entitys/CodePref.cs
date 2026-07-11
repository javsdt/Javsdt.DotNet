using Javsdt.Shared.Enums;

namespace Javsdt.Domain.Entitys;

public class CodePref
{
    public required string Name { get; set; }

    // 状态
    public JavType Type { get; set; } = JavType.有码;

    /// <summary>
    /// 分隔符
    /// </summary>
    public string Separator { get; set; } = "-";

    public override string ToString()
    {
        return Name;
    }
}