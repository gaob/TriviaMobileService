using Microsoft.WindowsAzure.Mobile.Service;

namespace TriviaMobileService.DataObjects
{
    /// <summary>
    /// The question structure in the database.
    /// Fields need to begin with capital case, otherwise JSONization will fail.
    /// </summary>
    public class QuestionItem : EntityData
    {
        public string QuestionText { get; set; }

        public string AnswerOne { get; set; }
        public string AnswerTwo { get; set; }
        public string AnswerThree { get; set; }
        public string AnswerFour { get; set; }

        public string Identifier { get; set; }
    }

    /// <summary>
    /// The question structure sent to client.
    /// </summary>
    public class QuestionToClient
    {
        public string Id { get; set; }
        public string questionText { get; set; }
        public string answerOne { get; set; }
        public string answerTwo { get; set; }
        public string answerThree { get; set; }
        public string answerFour { get; set; }
    }
}
