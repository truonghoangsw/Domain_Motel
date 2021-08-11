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
        [Description("Bước 4")]
        Setp4 = 14,
        [Description("Xóa")]
        Delete = 99,
        [Description("Có lỗi xảy ra")]
        Error = -99,
        [Description("Thực hiện thành công")]
        Susscess = 10
    }
}
