using Motel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Domain.Domain.UtilitiesRoom
{
    public class UtilitiesRoom:BaseEntity
    {
        public byte Status { get;set;}
	    public string UtilitiesName {get;set;}
	    public int UtilitiesValue {get;set;}
	    public string UtilitiesDescription {get;set;}

    }
}
