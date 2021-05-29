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
        float val1 = 0.0f;
        float val2 = 0.0f;
        float val3 = 0.0f;
        string sToDo = "";
        bool hasFunction = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void AddToAbove(object sender, RoutedEventArgs e)
        {
            float outF = 0.0f;
            txtCalc.Text += Convert.ToString((sender as Button).Content);
            if (float.TryParse(txtCalc.Text, out outF))
            {
                if(!hasFunction)
                {
                    EnableFunctionality();
                }
                else
                {
                    DisableFunctionality();
                    btnCalculation.IsEnabled = true;
                }
                
            }
            else
            {
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
            txtResult.Text = "";
            hasFunction = false;
            DisableFunctionality();
            btnCalculation.IsEnabled = false;
            EnableNumerics();
        }

        private void DisableFunctionality()
        {
            btnAddition.IsEnabled = false;
            btnSubtraction.IsEnabled = false;
            btnMultiplication.IsEnabled = false;
            btnDivision.IsEnabled = false;
        }

        private void EnableFunctionality()
        {
            btnAddition.IsEnabled = true;
            btnSubtraction.IsEnabled = true;
            btnMultiplication.IsEnabled = true;
            btnDivision.IsEnabled = true;
        }

        private void DisableNumerics()
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

        public void EnableNumerics()
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

        private void CalcValue(object sender, RoutedEventArgs e)
        {
            try
            {
                float outF = 0.0f;
                if (float.TryParse(txtCalc.Text, out outF))
                {
                    val2 = outF;
                }

                switch (sToDo)
                {
                    case "+":
                        val3 = val1 + val2;
                        break;

                    case "-":
                        val3 = val1 - val2;
                        break;

                    case "/":
                        val3 = val1 / val2;
                        break;

                    case "*":
                        val3 = val1 * val2;
                        break;

                    default:
                        break;
                }
                txtCalc.Text = "";
                txtResult.Text += " " + val2.ToString() + " = " + val3.ToString();
                DisableNumerics();
                btnCalculation.IsEnabled = false;
                histListView.Items.Add(txtResult.Text);
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
            sToDo = (sender as Button).Content.ToString();
            float outF = 0.0f;
            if (float.TryParse(txtCalc.Text, out outF)){
                //success. add to val
                if (txtResult.Text == "") {//add to val1
                    val1 = outF;
                    hasFunction = true;
                    txtResult.Text += val1.ToString() + " " + sToDo;
                    txtCalc.Text = "";
                }
            }

        }

        //added functionality to the app.
        double dMemory = 0;
        private void MemoryFunctions(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Tag)
            {
                case "1"://clear
                    dMemory = 0;
                    break;
                case "2"://recall
                    txtCalc.Text =  dMemory.ToString();
                    break;
                case "3"://add
                    dMemory += Convert.ToDouble((txtCalc.Text.ToString() != "" ? txtCalc.Text.ToString() : "0") );
                    break;
                case "4"://subtract
                    dMemory = dMemory - Convert.ToDouble((txtCalc.Text.ToString() != "" ? txtCalc.Text.ToString() : "0"));
                    break;
                case "5"://save
                    dMemory = Convert.ToDouble((txtCalc.Text.ToString() != "" ? txtCalc.Text.ToString() : "0"));
                    break;
                default:
                    break;
            }
            txtMemoryValn.Text = dMemory.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            histListView.Items.Clear();
        }
    }
}
