using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Web.Framework.Models
{
    public class BasePagedListModel<T>: BaseMotelModel, IPagedModel<T> where T : BaseMotelModel
    {
    }
}
