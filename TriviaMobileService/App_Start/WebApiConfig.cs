using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Web.Http;
using TriviaMobileService.DataObjects;
using TriviaMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;

namespace TriviaMobileService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();

            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            Database.SetInitializer(new MobileServiceInitializer());
        }
    }

    public class MobileServiceInitializer : DropCreateDatabaseIfModelChanges<MobileServiceContext>
    {
        protected override void Seed(MobileServiceContext context)
        {
            // Initialize 20 questions.
            List<QuestionItem> questionItems = new List<QuestionItem>
            {
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Which pop duo was the first western band to play in The Peoples Republic of China?", 
                                   AnswerOne = "Wham", AnswerTwo = "Simon and Garfunkel", AnswerThree = "Chas and Dave", AnswerFour = "Right Said Fred", 
                                   Identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Timber selected from how many fully grown oak trees were needed to build a large 3 decker Royal Navy battle ship in the 18th century?", 
                                   AnswerOne = "50", AnswerTwo = "500", AnswerThree = "1,500", AnswerFour = "3,500", 
                                   Identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Speed skating originated in which country?", 
                                   AnswerOne = "Russia", AnswerTwo = "Netherlands", AnswerThree = "Canada", AnswerFour = "Norway", 
                                   Identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Off the coast of which country did the Amoco Cadiz sink?", 
                                   AnswerOne = "South Africa", AnswerTwo = "France", AnswerThree = "USA", AnswerFour = "Spain", 
                                   Identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "The song 'An Englishman in New York' was about which man?", 
                                   AnswerOne = "Quentin Crisp", AnswerTwo = "Sting", AnswerThree = "John Lennon", AnswerFour = "Gordon Sumner", 
                                   Identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "In the book The Last Of The Mohicans what was the name of Chingachgook's only son?", 
                                   AnswerOne = "Mingo", AnswerTwo = "Lightfoot", AnswerThree = "Magua", AnswerFour = "Uncas", 
                                   Identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "In which year did Louis Reard invent the bikini?", 
                                   AnswerOne = "1906", AnswerTwo = "1926", AnswerThree = "1946", AnswerFour = "1966", 
                                   Identifier = "3"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "In which continent did the ostrich originate?", 
                                   AnswerOne = "Africa", AnswerTwo = "Australia", AnswerThree = "North America", AnswerFour = "Asia", 
                                   Identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "What are the names of the two main characters in Diana Gabaldon's highland saga novels?", 
                                   AnswerOne = "Jamie and Claire", AnswerTwo = "Fergus and Fiona", AnswerThree = "Rab and Elizabeth", AnswerFour = "Hamish and Maggie", 
                                   Identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Julius Caesar said \"The die is cast\" after crossing which river?", 
                                   AnswerOne = "Danube", AnswerTwo = "Rubicon", AnswerThree = "Thames", AnswerFour = "Tiber", 
                                   Identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Who was the only English player to win the European Golden Boot award?", 
                                   AnswerOne = "Robbie Fowler", AnswerTwo = "Michael Owen", AnswerThree = "Gary Lineker", AnswerFour = "Kevin Phillips", 
                                   Identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Doris Day had a sleepy hit with which song?", 
                                   AnswerOne = "Pillow talk", AnswerTwo = "Golden slumbers", AnswerThree = "The beds too big without you", AnswerFour = "Tears on my pillow", 
                                   Identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Which singer once took a bite out of a Beach Boys record during a press conference?", 
                                   AnswerOne = "Robbie Williams", AnswerTwo = "Engelbert Humperdinck", AnswerThree = "Shane McGowan", AnswerFour = "James Blunt", 
                                   Identifier = "3"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Who was the first band to play at the Cavern Club?", 
                                   AnswerOne = "Swinging Blue Jeans", AnswerTwo = "The Merseysippi", AnswerThree = "Beatles", AnswerFour = "Merseybeats", 
                                   Identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "What are Lanthanides?", 
                                   AnswerOne = "Elements in the periodic table", AnswerTwo = "Mountains on Earth", AnswerThree = "Mountains on the Moon", AnswerFour = "Bacterium", 
                                   Identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Which band was not from Liverpool?", 
                                   AnswerOne = "Searchers", AnswerTwo = "A Flock of Seagulls", AnswerThree = "Stone the crows", AnswerFour = "OMD", 
                                   Identifier = "3"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "Which boxer twice defeated Nigel Benn in 1996?", 
                                   AnswerOne = "Thulani Malinga", AnswerTwo = "Steve Collins", AnswerThree = "Roy Jones", AnswerFour = "Gerald McClellan", 
                                   Identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "On what make of motorcycle did Barry Sheene win the 500cc-world title in 1977?", 
                                   AnswerOne = "Kawasaki", AnswerTwo = "Suzuki", AnswerThree = "Mitsubishi", AnswerFour = "Yamaha", 
                                   Identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "What part of the body does a turtle use to breathe?", 
                                   AnswerOne = "Belly", AnswerTwo = "Shell", AnswerThree = "Mouth", AnswerFour = "Anus", 
                                   Identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), QuestionText = "For how long is a dog pregnant?", 
                                   AnswerOne = "9 Weeks", AnswerTwo = "9 Days", AnswerThree = "9 Months", AnswerFour = "9 Fortnights", 
                                   Identifier = "1"}
            };

            foreach (QuestionItem questionItem in questionItems)
            {
                context.Set<QuestionItem>().Add(questionItem);
            }

            base.Seed(context);
        }
    }
}

