using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaMobileService.DataObjects
{
    /// <summary>
    /// The Session structure stored in the database.
    /// The auto-generated id is the SessionID.
    /// </summary>
    public class SessionItem : EntityData
    {
        public string playerid { get; set; }
    }
}
