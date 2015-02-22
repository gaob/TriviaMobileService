using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaMobileService.DataObjects
{
    public class SessionItem : EntityData
    {
        public string playerid { get; set; }
    }
}
