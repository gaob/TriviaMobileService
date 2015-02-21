using Microsoft.WindowsAzure.Mobile.Service;

namespace TriviaMobileService.DataObjects
{
    public class QuestionItem : EntityData
    {
        public string questionText { get; set; }

        public string answerOne { get; set; }
        public string answerTwo { get; set; }
        public string answerThree { get; set; }
        public string answerFour { get; set; }

        public int identifier { get; set; }
    }

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
