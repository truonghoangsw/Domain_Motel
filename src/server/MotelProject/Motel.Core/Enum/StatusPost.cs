using System.ComponentModel;

namespace Motel.Core.Enum
{
    public enum StatusPost
    {
        [Description("Chờ duyệt")]
        Pending = 0,
        [Description("Đang chạy")]
        Approved = 1,
        [Description("Hủy")]
        Cancel = 2,
        [Description("Khóa")]
        Block = 3,
        [Description("Xóa")]
        Delete = 99
    }
}
