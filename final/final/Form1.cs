using System.Text;
using System.Diagnostics;

namespace final
{

    public partial class Form1 : Form
    {

        Password password = new Password();
        string file = "passwords.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = password.GeneratePassword();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Password password1 = new Password();
            string username = textBox1.Text;
            string password = password1.EncryptPassword(textBox2.Text, "7N8tK7y4p8zR1/3f6+9V1g==\r\n");


            using (StreamWriter outputFile = new StreamWriter(file, true))
            {
                outputFile.WriteLine(username + ":" + password);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", file);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Password password2 = new Password();
            string password = password2.DecryptPassword(textBox3.Text, "7N8tK7y4p8zR1/3f6+9V1g==\r\n");
            textBox4.Text = password;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


    public class Password
    {
        int passSize;

        public Password()
        {
            Random rnd = new Random();
            passSize = rnd.Next(8, 14);
        }

        public string GeneratePassword()
        {
            string lower = "abcdefghijklmnopqrstuvwxyz";
            string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            string punctuation = "!@#$%^&*_-+=:;,.?/|~";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            password.Append(lower[random.Next(lower.Length)]);
            password.Append(upper[random.Next(upper.Length)]);
            password.Append(digits[random.Next(digits.Length)]);
            password.Append(punctuation[random.Next(punctuation.Length)]);

            for (int i = passSize - 4; i >= 0; i--)
            {
                string ratio = lower + upper + digits + punctuation;
                int index = random.Next(ratio.Length);
                password.Append(ratio[index]);
            }

            for (int i = 0; i < password.Length; i++)
            {
                int swap = random.Next(password.Length);
                char temp = password[i];
                password[i] = password[swap];
                password[swap] = temp;
            }

            return password.ToString();
        }

        public string EncryptPassword(string password, string secretKey)
        {
            StringBuilder encryptedPassword = new StringBuilder();
            for (int i = 0; i < password.Length; i++)
            {
                encryptedPassword.Append((char)(password[i] ^ secretKey[i % secretKey.Length]));
            }
            return encryptedPassword.ToString();
        }

        public string DecryptPassword(string encryptedPassword, string secretKey)
        {
            StringBuilder decryptedPassword = new StringBuilder();
            for (int i = 0; i < encryptedPassword.Length; i++)
            {
                decryptedPassword.Append((char)(encryptedPassword[i] ^ secretKey[i % secretKey.Length]));
            }
            return decryptedPassword.ToString();
        }


    }

}



