using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewsLinkerConnect
{
    public partial class TabForm : Form
    {

        private User currentUser;

        //Constructor that accepts a User object
        public TabForm(User user)
        {
            InitializeComponent();
            currentUser = user;

            // Initializing Combo box options (custom event)
            comboBoxOptionsArrays();

            //User Details for Udating
            viewUserDetail();
        }

        // For Reading Text Files
        private void openTextFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Show the dialog to select the text file to load
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Text Files (*.txt) | *.txt";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected path
                    string filePath = openFileDialog1.FileName;

                    //Read the text file and add its lines to the listBox
                    string[] lines = File.ReadAllLines(filePath);
                    statusPageListBox.Items.Clear(); // Clear existing items
                    statusPageListBox.Items.AddRange(lines);

                    MessageBox.Show("Text File content loaded successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Opening file: {ex.Message}");
            }
        }

        private void subscribeButton_Click(object sender, EventArgs e)
        {
            // Validating User Choices
            if(interestsCheckedListBox.CheckedItems.Count > 0 && preferredLangComboBox.SelectedIndex != -1 && newsletterFrequencyComboBox.SelectedIndex != -1)
            {
                string selectedItems = ""; // Saving selected interest as a string

                for (int i = 0; i < interestsCheckedListBox.CheckedItems.Count; i++)
                {
                    if (i < interestsCheckedListBox.CheckedItems.Count - 1)
                    {
                        selectedItems += interestsCheckedListBox.CheckedItems[i].ToString() + ", ";
                    }
                    else
                    {
                        selectedItems += interestsCheckedListBox.CheckedItems[i].ToString();
                    }
                }
                currentUser.interests = selectedItems;
                currentUser.preferred_langauage = preferredLangComboBox.Text;
                currentUser.newsletter_frequency = newsletterFrequencyComboBox.Text;

                subscriptionConfirmationPanel.Visible = true;
                subscribePageListBox.Items.Clear();

                // For moving using input text in multiline in List Box
                string[] lines = currentUser.ToString().Split(new[] { '\n' });
                subscribePageListBox.Items.Add("SUBSCRIPTION CONFIRMED\n");
                subscribePageListBox.Items.Add("");

                foreach (var line in lines)
                {
                    subscribePageListBox.Items.Add(line);
                    subscribePageListBox.Items.Add("");
                }

                // Unselecting options on submit
                for (int i = 0; i < interestsCheckedListBox.Items.Count; i++)
                {
                    interestsCheckedListBox.SetItemChecked(i, false);
                }

                // Unselecting options on submit
                preferredLangComboBox.SelectedIndex = -1;
                newsletterFrequencyComboBox.SelectedIndex = -1;
                
                MessageBox.Show("Updated successfully!");
            }
            else
            {
                MessageBox.Show("Please update atleast 1 Interest, Preferred language and Newsletter Frequency to your subscription!");
            }
        }
        private bool IsValidEmail(string email)
        {
            // Basic email validation
            return email.Contains("@") && email.Contains(".");
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            // Validate User Input for updating User
            if (updateFirstnameTextbox.Text.Length >= 1 && updateLastnameTextbox.Text.Length >= 1 && int.TryParse(updateAgeTextbox.Text, out int result) &&
                IsValidEmail(updateEmailTextbox.Text) && updateAddressTextbox.Text.Length >= 5)
            {
                currentUser.first_name = updateFirstnameTextbox.Text;
                currentUser.last_name = updateLastnameTextbox.Text;
                currentUser.age = int.Parse(updateAgeTextbox.Text);
                currentUser.email = updateEmailTextbox.Text;
                currentUser.address = updateAddressTextbox.Text;

                personalInfoPanel.Visible = true;
                updatedPersonalInfoListbox.Items.Clear();



                // For moving text in multiline in List Box
                string[] lines = currentUser.UserDetails().Split(new[] { '\n' });
                updatedPersonalInfoListbox.Items.Add("UPDATED PERSONAL DETAILS\n");
                updatedPersonalInfoListbox.Items.Add("");

                foreach (var line in lines)
                {
                    updatedPersonalInfoListbox.Items.Add(line);
                    updatedPersonalInfoListbox.Items.Add("");
                }
                MessageBox.Show("Updated successfully!");
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

        private void viewUserDetail()
        {
            updateFirstnameTextbox.Text = currentUser.first_name;
            updateLastnameTextbox.Text = currentUser.last_name;
            updateAgeTextbox.Text = currentUser.age.ToString();
            updateEmailTextbox.Text = currentUser.email;
            updateAddressTextbox.Text = currentUser.address;
        }

        //Save to Text Files
        private void saveToTextFileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                // Show a dialog to select the destination file
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Text Files(*.txt)|*.txt";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string filePath = saveFile.FileName;


                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var item in subscribePageListBox.Items)
                        {
                            writer.WriteLine(item.ToString());
                        }
                    }
                    MessageBox.Show("File saved successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error ocurred: {ex.Message}");
            }
        }

        private void comboBoxOptionsArrays()
        {
            // Arrays to store options for ComboBox
            string[] newsletterFrequencyArray = { "Monthly", "Weekly", "Daily" }; // Array (reference)
            for (int i = 0; i < newsletterFrequencyArray.Length; i++)
            {
                newsletterFrequencyComboBox.Items.Add(newsletterFrequencyArray[i]);
            }

            string[] preferredLangArray = { "English", "French", "Spanish" }; // Array (reference)
            for (int i = 0; i < preferredLangArray.Length; i++)
            {
                preferredLangComboBox.Items.Add(preferredLangArray[i]);
            }

            //Hiding List Box Panels
            subscriptionConfirmationPanel.Visible = false;
            personalInfoPanel.Visible = false;


            // Check if there are no items in the ListView
            if (statusPageListBox.Items.Count == 0)
            {
                // Add the empty item to the ListView
                statusPageListBox.Items.Add("Please select the text file!");
            }
        }

        private void subscribePageExitButton_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        private void personalInfoExitButton_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        private void statusPageExitButton_Click(object sender, EventArgs e)
        {
            closeApplication();
        }

        private void closeApplication()
        {
            Application.Exit(); // Closing the Application
        }

    }
}
