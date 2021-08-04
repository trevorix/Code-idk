using System;
using System.Windows.Forms;

namespace DnD_Roll_Chances
{
    public partial class DnDRoll : Form
    {
        public DnDRoll(){ InitializeComponent(); }
        private void Form1_Load(object sender, EventArgs e){ }
        public int dc = 0;
        public int mod = 0;
        public int adv = 0; // 0 for no adv || 1 for adv || -1 for dis 
        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            try { dc = Convert.ToInt32(textBox1.Text); }
            catch { dc = 11; textBox1.Text = "";}
        }
        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            try { mod = Convert.ToInt32(textBox2.Text); }
            catch { mod = 0; textBox2.Text = ""; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double gaming;
            if (adv == 0)
            {
                if (mod >= dc || dc == 1) //chance of getting nat 1 [modifier higher than dc] || [dc is 1]
                    gaming = 95;
                else if (mod <= 0 & dc >= 20) //chance nat 20 [modifier is 0 or less] & dc >= 20
                    gaming = 5;
                else
                    gaming = (1 - (((dc - mod) - 1) / 20.0)) * 100; //1 - miss chance
                textBox4.Text = Convert.ToString(gaming);
            }
            else if (adv == 1)
            {
                if (mod >= dc || dc == 1) //chance of getting nat 1 [modifier higher than dc] || [dc is 1]
                    gaming = 99.75;
                else if (mod <= 0 & dc >= 20) //chance nat 20 [modifier is 0 or less] & dc >= 20
                    gaming = 2.5;
                else
                    gaming = (1 - Math.Pow((dc - mod - 1) / 20.0, 2)) * 100; // 1 - (miss chance)^2
                textBox4.Text = Convert.ToString(gaming);
            }
            else if (adv == -1)
            {
                if (mod >= dc || dc == 1) //chance of getting nat 1 [modifier higher than dc] || [dc is 1]
                    gaming = 90.25;
                else if (mod <= 0 & dc >= 20) //chance of getting nat 20 [modifier is 0 or less] & dc is 20 or higher
                    gaming = 0.0025;
                else
                    gaming = Math.Pow(1 - (dc - mod - 1) / 20.0, 2) * 100;//(1 - miss chance)^2
                textBox4.Text = Convert.ToString(gaming);
            }
            decimal count = 0;
            int LOG = 7;//EXPONENTIAL--NO HIGHER THAN 9
            Random rand = new Random();
            for (int test = 0; test < Math.Pow(10, LOG); test++)
            {
                int ran = rand.Next(1, 21);
                if (ran == 1 || ran + mod < dc)
                {
                    if (adv == 1)
                    {
                        ran = rand.Next(1, 21);
                        if (ran == 20 || ran + mod >= dc && ran != 1)
                            count++;
                    }
                }
                else if (ran == 20 || ran + mod >= dc)
                {
                    if (adv == -1)
                    {
                        ran = rand.Next(1, 21);
                        if (ran == 20 || ran + mod >= dc && ran != 1)
                            count++;
                    }
                    else
                        count++;
                }
            }
            textBox6.Text = Convert.ToString(Decimal.Round(count / (decimal)Math.Pow(10, LOG) * 100, 2));
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            adv = 1; 
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            adv = 0;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            adv = -1;
        }
    }
}