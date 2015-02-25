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
            List<QuestionItem> questionItems = new List<QuestionItem>
            {
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Which pop duo was the first western band to play in The Peoples Republic of China?", 
                                   answerOne = "Wham", answerTwo = "Simon and Garfunkel", answerThree = "Chas and Dave", answerFour = "Right Said Fred", 
                                   identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Timber selected from how many fully grown oak trees were needed to build a large 3 decker Royal Navy battle ship in the 18th century?", 
                                   answerOne = "50", answerTwo = "500", answerThree = "1,500", answerFour = "3,500", 
                                   identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Speed skating originated in which country?", 
                                   answerOne = "Russia", answerTwo = "Netherlands", answerThree = "Canada", answerFour = "Norway", 
                                   identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Off the coast of which country did the Amoco Cadiz sink?", 
                                   answerOne = "South Africa", answerTwo = "France", answerThree = "USA", answerFour = "Spain", 
                                   identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "The song 'An Englishman in New York' was about which man?", 
                                   answerOne = "Quentin Crisp", answerTwo = "Sting", answerThree = "John Lennon", answerFour = "Gordon Sumner", 
                                   identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "In the book The Last Of The Mohicans what was the name of Chingachgook's only son?", 
                                   answerOne = "Mingo", answerTwo = "Lightfoot", answerThree = "Magua", answerFour = "Uncas", 
                                   identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "In which year did Louis Reard invent the bikini?", 
                                   answerOne = "1906", answerTwo = "1926", answerThree = "1946", answerFour = "1966", 
                                   identifier = "3"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "In which continent did the ostrich originate?", 
                                   answerOne = "Africa", answerTwo = "Australia", answerThree = "North America", answerFour = "Asia", 
                                   identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "What are the names of the two main characters in Diana Gabaldon's highland saga novels?", 
                                   answerOne = "Jamie and Claire", answerTwo = "Fergus and Fiona", answerThree = "Rab and Elizabeth", answerFour = "Hamish and Maggie", 
                                   identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Julius Caesar said \"The die is cast\" after crossing which river?", 
                                   answerOne = "Danube", answerTwo = "Rubicon", answerThree = "Thames", answerFour = "Tiber", 
                                   identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Who was the only English player to win the European Golden Boot award?", 
                                   answerOne = "Robbie Fowler", answerTwo = "Michael Owen", answerThree = "Gary Lineker", answerFour = "Kevin Phillips", 
                                   identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Doris Day had a sleepy hit with which song?", 
                                   answerOne = "Pillow talk", answerTwo = "Golden slumbers", answerThree = "The beds too big without you", answerFour = "Tears on my pillow", 
                                   identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Which singer once took a bite out of a Beach Boys record during a press conference?", 
                                   answerOne = "Robbie Williams", answerTwo = "Engelbert Humperdinck", answerThree = "Shane McGowan", answerFour = "James Blunt", 
                                   identifier = "3"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Who was the first band to play at the Cavern Club?", 
                                   answerOne = "Swinging Blue Jeans", answerTwo = "The Merseysippi", answerThree = "Beatles", answerFour = "Merseybeats", 
                                   identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "What are Lanthanides?", 
                                   answerOne = "Elements in the periodic table", answerTwo = "Mountains on Earth", answerThree = "Mountains on the Moon", answerFour = "Bacterium", 
                                   identifier = "1"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Which band was not from Liverpool?", 
                                   answerOne = "Searchers", answerTwo = "A Flock of Seagulls", answerThree = "Stone the crows", answerFour = "OMD", 
                                   identifier = "3"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "Which boxer twice defeated Nigel Benn in 1996?", 
                                   answerOne = "Thulani Malinga", answerTwo = "Steve Collins", answerThree = "Roy Jones", answerFour = "Gerald McClellan", 
                                   identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "On what make of motorcycle did Barry Sheene win the 500cc-world title in 1977?", 
                                   answerOne = "Kawasaki", answerTwo = "Suzuki", answerThree = "Mitsubishi", answerFour = "Yamaha", 
                                   identifier = "2"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "What part of the body does a turtle use to breathe?", 
                                   answerOne = "Belly", answerTwo = "Shell", answerThree = "Mouth", answerFour = "Anus", 
                                   identifier = "4"},
                new QuestionItem { Id = Guid.NewGuid().ToString(), questionText = "For how long is a dog pregnant?", 
                                   answerOne = "9 Weeks", answerTwo = "9 Days", answerThree = "9 Months", answerFour = "9 Fortnights", 
                                   identifier = "1"}
            };

            foreach (QuestionItem questionItem in questionItems)
            {
                context.Set<QuestionItem>().Add(questionItem);
            }

            base.Seed(context);
        }
    }
}

