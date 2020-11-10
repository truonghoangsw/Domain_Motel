using Motel.Core.Enum;
using Motel.Domain;
using Motel.Domain.Domain.Lester;
using Motel.Domain.Domain.Sercurity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Motel.Services.Lester
{
    public class RegistrationLeterReults
    {
        public RegistrationLeterReults()
        {
            customPrincipal  =new CustomPrincipal();
        }
        public CustomPrincipal customPrincipal { get;set;}
        public Lesters Lesters { get; set;}
        public MessgeCodeRegistration MessageCode { get;set;}
    }
}
