using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsLinkerConnect
{
    //User class to create user objects
    public class User
    {
        public int user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int age { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string interests { get; set; } = "Entertainment";
        public string preferred_langauage { get; set; } = "English";
        public string newsletter_frequency { get; set; } = "Monthly";

        //ToString method to display User Details
        public override string ToString()
        {
            return $"Name: {first_name} {last_name}\nAge: {age}\nEmail: {email}\nAddress: {address}\n" +
                $"Interests: {interests}\nPreferred Language: {preferred_langauage}\nNewsletter Frequency: {newsletter_frequency}";
        }

        public string UserDetails()
        {
            return $"Name: {first_name} {last_name}\nAge: {age}\nEmail: {email}\nAddress: {address}";
        }
    }
}
