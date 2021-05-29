using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        float val1, val2 = 0.0f;
        bool hasFunction = false;

        CalcMath cm = new CalcMath();
        CalcMath.CALCTASK ct;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void AddToAbove(object sender, RoutedEventArgs e)
        {
            float outF = 0.0f;
            txtCalc.Text += Convert.ToString((sender as Button).Content);
            if (float.TryParse(txtCalc.Text, out outF))
            {//success, valid float
                if(!hasFunction)//first number is added, we can enable functionality.
                {
                    EnableFunctionality();
                }
                else
                {//adding second number. disable functionality because I only allow for 2 calculations at a time.
                    DisableFunctionality();
                    btnCalculation.IsEnabled = true;
                }
                
            }
            else {//invalid float, keep functionality disabled.
               DisableFunctionality();
            }
        }
        
        private void ClearCalc_Click(object sender, RoutedEventArgs e)
        {
            ClearCalc();
        }

        private void ClearCalc()
        {
            txtCalc.Text = "";
            txtResult.Text = "Result:";
            hasFunction = false;
            DisableFunctionality();
            btnCalculation.IsEnabled = false;
            EnableNumerics();
        }

        #region functionality, + , - , * , /
        internal void DisableFunctionality()
        {
            btnAddition.IsEnabled = false;
            btnSubtraction.IsEnabled = false;
            btnMultiplication.IsEnabled = false;
            btnDivision.IsEnabled = false;
        }

        internal void EnableFunctionality()
        {
            btnAddition.IsEnabled = true;
            btnSubtraction.IsEnabled = true;
            btnMultiplication.IsEnabled = true;
            btnDivision.IsEnabled = true;
        }
        #endregion

        #region numerics [0..9] and .
        internal void DisableNumerics()
        {
            btnDot.IsEnabled = false;
            btn0.IsEnabled = false;
            btn9.IsEnabled = false;
            btn8.IsEnabled = false;
            btn7.IsEnabled = false;
            btn6.IsEnabled = false;
            btn5.IsEnabled = false;
            btn4.IsEnabled = false;
            btn3.IsEnabled = false;
            btn2.IsEnabled = false;
            btn1.IsEnabled = false;
        }

        internal void EnableNumerics()
        {
            btnDot.IsEnabled = true;
            btn0.IsEnabled = true;
            btn9.IsEnabled = true;
            btn8.IsEnabled = true;
            btn7.IsEnabled = true;
            btn6.IsEnabled = true;
            btn5.IsEnabled = true;
            btn4.IsEnabled = true;
            btn3.IsEnabled = true;
            btn2.IsEnabled = true;
            btn1.IsEnabled = true;
        }
        #endregion

        private void CalcValue(object sender, RoutedEventArgs e)
        {
            try
            {
                float outF = 0.0f;
                if (float.TryParse(txtCalc.Text, out outF))
                {
                    val2 = outF;
                }
                
                cm.DoTask(val1.ToString(), val2.ToString(), out float valToReturn, ct);
                
                txtCalc.Text = "";
                txtResult.Text += val2.ToString() + "=" + valToReturn.ToString();
                DisableNumerics();
                btnCalculation.IsEnabled = false;
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message);
                ClearCalc();
            }
            catch (NotFiniteNumberException ex)
            {
                MessageBox.Show(ex.Message);
                ClearCalc();
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message);
                ClearCalc();
            }
        }
        
        private void SplitToFloat(object sender, RoutedEventArgs e)
        {
            ct = (CalcMath.CALCTASK) Enum.Parse(typeof(CalcMath.CALCTASK), (sender as Button).Tag.ToString());
            string sToDo = "";
            float outF = 0.0f;
            if (float.TryParse(txtCalc.Text, out outF)){
                //success. add to val
                if (txtResult.Text == "Result:") {//add to val1
                    val1 = outF;
                    hasFunction = true;
                    switch (ct)
                    {
                        case CalcMath.CALCTASK.ADDITION:
                            sToDo = "+";
                            break;
                        case CalcMath.CALCTASK.SUBTRACTION:
                            sToDo = "-";
                            break;
                        case CalcMath.CALCTASK.MULTIPLICATION:
                            sToDo = "*";
                            break;
                        case CalcMath.CALCTASK.DIVISION:
                            sToDo = "/";
                            break;
                    }

                    txtResult.Text += "\r\n" + val1.ToString() + sToDo;
                    txtCalc.Text = "";
                }
            }

        }
    }
}
