using GrassHopper.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace GrassHopper.Data
{
    public class FacebookReviews
    {

        //constructor

        //new static method called newFromFacebook()
        //the method returns a List of Review objects
        //it inputs the string from the facebook api
        public static Review NewFromFacebook(string r)
        {
            //Note: THIS NEEDS TESTED WITH MORE THAN 1 REVIEW
            Review review = new Review();
            string[] strings = r.Split(new char[] { '{', '}' });
            //string content = strings[1];
            strings[0] = "{";
            strings[2] = "}";
            string theString = strings[0] + strings[1] + strings[2];
            Newtonsoft.Json.Linq.JObject theObj = JObject.Parse(theString);
            var reviewText = theObj["review_text"];
            var createdTime = theObj["created_time"];
            var rating = theObj["rating"];
            var reviewer = theObj["reviewer"];

            if (reviewText != null)
            {
                review.ReviewBody = (string)reviewText;
            } else
            {
                review.ReviewBody = "No review given.";
            }
            if (createdTime != null) //Note: there has to be a time, there's no default
            {
                string str = createdTime.ToString();

                string yearStr = str.Substring(0, 4);
                int year = int.Parse(yearStr);

                string monthStr = str.Substring(5, 2);
                int month = int.Parse(monthStr);

                string dayStr = str.Substring(8, 2);
                int day = int.Parse(dayStr);

                review.ReviewDate = new DateTime(year, month, day);
            } 
            if (rating != null)
            {
                //normalize the rating format into the model
            } else
            {
                //no rating was given, handle this
            }
            if (reviewer != null)
            {
                //normalise the reviewer into the model
            } else
            {
                review.ReviewerName = "Anonymous reviewer from Facebook";
            }
            

            return review;
        }
    }
}
