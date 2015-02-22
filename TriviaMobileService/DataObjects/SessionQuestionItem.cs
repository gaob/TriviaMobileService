using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaMobileService.DataObjects
{
    public class SessionQuestionItem : EntityData
    {
        public string GameSessionID { get; set; }
        public string QuestionID { get; set; }
        public string proposedAnswer { get; set; }
    }
}
