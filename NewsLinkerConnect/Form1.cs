using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsLinkerConnect
{
    public partial class Form1 : Form
    {
        // List to add new Users
        private List<User> users = new List<User>();
        
        private int count = 1; // (Integer Primitive) - count to auto increment for user_id

        public Form1()
        {
            InitializeComponent();
        }

        private void registerPageExitButton_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        // Exit and close Application Function
        private void closeApplication()
        {
            Application.Exit();
        }

        private bool IsValidEmail(string email)
        {
            // Basic email validation
            return email.Contains("@") && email.Contains(".");
        }
        private void registerButton_Click(object sender, EventArgs e)
        {
            // Validate User Inputs
            if (firstnameTextbox.Text.Length >= 1 && lastnameTextbox.Text.Length >=1 && int.TryParse(ageTextbox.Text, out int result) &&  
                IsValidEmail(emailTextbox.Text) && addressTextbox.Text.Length >= 5)
            {            
                // Create a new User Object
                User user = new User
                {
                    user_id = count++,
                    first_name = firstnameTextbox.Text,
                    last_name = lastnameTextbox.Text,
                    age = int.Parse(ageTextbox.Text),
                    email = emailTextbox.Text,
                    address = addressTextbox.Text
                };

                users.Add(user);

                TabForm objFrm = new TabForm(users[0]); // passing a user object to the tabForm
                objFrm.Show(); // Show Tab form
                this.Hide(); // Hide current form
            }
            else
            {
                MessageBox.Show("Please check your details!\n" +
                    "Firstname and Lastname must be more than 1 character.\n" +
                    "Age must be a number.\n" +
                    "Email should be a valid email.\n" +
                    "Address should be atleast more than 5 characters.");
            }
        }
    }
}
