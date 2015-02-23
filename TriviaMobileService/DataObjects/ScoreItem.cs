﻿using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaMobileService.DataObjects
{
    public class ScoreItem : EntityData
    {
        public string playerid { get; set; }
        public int score { get; set; }
        public DateTime occurred { get; set; }
    }
}
