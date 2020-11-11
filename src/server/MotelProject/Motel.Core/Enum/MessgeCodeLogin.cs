using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Motel.Core.Enum
{
    public enum MessgeCodeRegistration
    {
        [Description("Thực hiện thành công")]
        Suscess = 1,
        [Description("Đã validate")]
        IsValidate = 5,
        [Description("Tên tài khoản đã tồn tại")]
        ExistName = 10,
        [Description("Email tài khoản đã tồn tại")]
        ExistEmail = 11,
       
        [Description("Tên tài khoản không hợp lệ")]
        AccountNameWrong = -1,

        [Description("Password không hợp lệ")]
        PasswordWrong = -2,

        [Description("Email tài khoản không hợp lệ")]
        AccountEmailWrong = -3,

        [Description("Không tìm được tên tài khoản")]
        NotFoundName = -4,
        [Description("Đã bị khóa")]
        IsDeleted = -77,

        [Description("Đã bị khóa")]
        IsLockout = -88,
        
        [Description("Thực hiện thất bại")]
        Error = -99,
    }
}
