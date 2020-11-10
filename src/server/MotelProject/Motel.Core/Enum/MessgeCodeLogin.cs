using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Motel.Core.Enum
{
    public enum MessgeCodeRegistration
    {
        [Description("Tên tài khoản đã tồn tại")]
        ExistName = 10,
        [Description("Email tài khoản đã tồn tại")]
        ExistEmail = 10,
        [Description("Password không hợp lệ")]
        PasswordWrong = -2,
        [Description("Tên tài khoản không hợp lệ")]
        AccountNameWrong = -1,
        [Description("Email tài khoản không hợp lệ")]
        AccountEmailWrong = -2,
        [Description("Thực hiện thành công")]
        Suscess = 1,
        [Description("Thực hiện thất bại")]
        Error = -99,
        [Description("Đã validate")]
        IsValidate = 5,
    }
}
