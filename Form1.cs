using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calcu
{
    public partial class Calculate : Form
    {
        bool isNewEntity = false;
        bool isRepeatLastOperation = false;   //too boolen var to block repete number and operation , and allow to clean screen to new var;
        double EndResult = 0, NumbTwo = 0, Memory = 0;  //number result and next number input and memoryStore;
        char ChPreviousOperator; //signs in calcu;
        //
        public Calculate()
        {
            InitializeComponent();
            ActiveControl = TextBoxScreen;
        }
 //=======================================================================group1===================================================//
        // method of numbers 1,2,3,4,5,6,7,8,9,0,. ;
        private void UpdateOPeration(object sender, EventArgs e)
        {
            if (isNewEntity)
            {
                TextBoxScreen.Text = "0";
                isNewEntity = false;
            }
            if (isRepeatLastOperation)
            {
                ChPreviousOperator = '\0';
                EndResult = 0;
            }
            //use single line if statement.

            if (!(TextBoxScreen.Text == "0" && (Button)sender == button0) && !(((Button)sender) == buttonDot && TextBoxScreen.Text.Contains(".")))
            {
                TextBoxScreen.Text = (TextBoxScreen.Text == "0" && ((Button)sender) == buttonDot) ? "0." : ((TextBoxScreen.Text == "0") ? ((Button)sender).Text : TextBoxScreen.Text + ((Button)sender).Text);
            }
        }
//=======================================================================group2===================================================//
        //method to perform operators +,-,*,/ ;
        void Operators(double FirstNumber, char Sign, double SecoundNumber)
        {
            switch (ChPreviousOperator)
            {
                case '+':
                    TextBoxScreen.Text = (EndResult = (FirstNumber + SecoundNumber)).ToString();
                    break;
                case '-':
                    TextBoxScreen.Text = (EndResult = (FirstNumber - SecoundNumber)).ToString();
                    break;
                case '*':
                    TextBoxScreen.Text = (EndResult = (FirstNumber * SecoundNumber)).ToString();
                    break;
                case '/':
                        TextBoxScreen.Text = (EndResult = (FirstNumber / SecoundNumber)).ToString();
                    break;
            }
        }

        //method for perfrm mathmatic operation through operators method;
        private void OperatorMathmatic(object sender, EventArgs e)
        {
          if (TextBoxScreen.Text.Length==0)
            {
                    TextBoxScreen.Text = null;
            }
            else
            {
            if (ChPreviousOperator == '\0')
                {
                    ChPreviousOperator = ((Button)sender).Text[0];
                    EndResult = double.Parse(TextBoxScreen.Text);
                }
                else if (isNewEntity)
                {
                    ChPreviousOperator = ((Button)sender).Text[0];
                }
                else
                {
                    ChPreviousOperator = ((Button)sender).Text[0];
                    Operators(EndResult, ChPreviousOperator, double.Parse(TextBoxScreen.Text));

                }
                isNewEntity = true;
                isRepeatLastOperation = false;
            }
        }
                        //===========; 
        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0)
            {
                TextBoxScreen.Text = "0";
            }
            else
            {
                if (!isRepeatLastOperation)
                {
                    NumbTwo = double.Parse(TextBoxScreen.Text);
                    isRepeatLastOperation = true;
                    isNewEntity = true;
                }
            
                Operators(EndResult, ChPreviousOperator, NumbTwo);
            }
        }
 //=======================================================================group3===================================================//
        // change miuns to positive sign - or +
        private void buttonChangeSign_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0)
            {
                TextBoxScreen.Text = "0";
            }
            else
            {
                TextBoxScreen.Text = (double.Parse(TextBoxScreen.Text) * (-1)).ToString();
            }
        }

        // inverse number 1/x or x^-1
        private void buttonInverse_Click(object sender, EventArgs e)
        {
          TextBoxScreen.Text = Math.Pow(double.Parse(TextBoxScreen.Text), -1).ToString();
        }

        // %  
        private void buttonPresent_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0)
            {
                TextBoxScreen.Text = "0";
            }
            else
            {
                TextBoxScreen.Text = (double.Parse(TextBoxScreen.Text) * 1 / 100).ToString();
            }
        }

        //squrt number
        private void buttonSqurt_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0)
            {
                TextBoxScreen.Text = "0";
            }
            else
            {
                TextBoxScreen.Text = Math.Sqrt(double.Parse(TextBoxScreen.Text)).ToString();
            }
        }
//=======================================================================group4===================================================//
        private void buttonMS_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0 || TextBoxScreen.Text=="0")
            {
                TextBoxScreen.Text =null;
            }
            else
            {
                buttonMC.Enabled = true;
                buttonMR.Enabled = true;
                Memory = double.Parse(TextBoxScreen.Text);
            }
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0 || TextBoxScreen.Text == "0")
            {
                TextBoxScreen.Text = null;
            }
            else
            {
                buttonMC.Enabled = true;
                buttonMR.Enabled = true;
                Memory += EndResult;
                TextBoxScreen.Text = Memory.ToString();
            }
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            Memory = 0;
            buttonMC.Enabled = false;
            buttonMR.Enabled = false;
        }

        private void Calculate_MouseHover(object sender, EventArgs e)
        {
            LblDeveloper.Visible = true;
        }

        private void LblDeveloper_MouseLeave(object sender, EventArgs e)
        {
            LblDeveloper.Visible = false;
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            TextBoxScreen.Text = Memory.ToString();
        }

        //=======================================================================group5===================================================//

        //button to delete backspace;
        private void buttonBackSpace_Click(object sender, EventArgs e)
        {
            if (TextBoxScreen.Text.Length == 0)
            {
                TextBoxScreen.Text = "0";
            }
            else
            {
                TextBoxScreen.Text = TextBoxScreen.Text.Remove(TextBoxScreen.Text.Length - 1, 1);
                isNewEntity = true;
            }
        }
        //button clear text ;
        private void buttonClear_Click(object sender, EventArgs e)
        {
            TextBoxScreen.Text = "0";
            NumbTwo = EndResult = 0;
            isNewEntity = true;
            ChPreviousOperator = '\0';
            buttonMC.Enabled = false;
            buttonMR.Enabled = false;
        }
    }
}
