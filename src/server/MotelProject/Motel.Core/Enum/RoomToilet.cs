using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Motel.Core.Enum
{
    public enum RoomToilet
    {
        [Description("Phòng vệ sinh chung")]
        SharedToilet = 0,
        [Description("Đang chạy")]
        ToiletPrivate = 1
    }
}
