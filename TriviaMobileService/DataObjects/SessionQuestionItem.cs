using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriviaMobileService.DataObjects
{
    /// <summary>
    /// The structure to store each question associated with each game session.
    /// </summary>
    public class SessionQuestionItem : EntityData
    {
        public string GameSessionID { get; set; }
        public string QuestionID { get; set; }
        // "1", "2", "3", "4" indicates the answer, "?" means this question hasn't been answered.
        public string proposedAnswer { get; set; }
    }
}
