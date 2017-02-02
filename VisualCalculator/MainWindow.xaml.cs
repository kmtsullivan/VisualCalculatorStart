using System;
using System.Windows;
using System.Windows.Controls;

namespace VisualCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Implements a simple calculator.
    /// Author: Katherine Sullivan
    /// 
    /// Created from a skeleton class provided by instructor of
    /// "C# Essential Training" Lynda.com
    /// </summary>
    public partial class MainWindow : Window
    {

        double currentValue = 0;
        double newValue = 0;
        double cumulativeValue = 0;
        double currentDecimalPlace;
        int currentDigit = 0;
        enum Operation { Add, Subtract, Multiply, Divide, Equals, Start, Initialized };
        enum State { GetWholePart, GetFractionPart, CalcAnswer};
        State currentState;
        Operation currentOperation;
        

        public MainWindow()
        {
            InitializeComponent();
            txtOut.Text = currentValue.ToString();
            currentState = State.GetWholePart;
            currentOperation = Operation.Initialized;
        }

        private void BtnEntry_Click(object sender, RoutedEventArgs e)
        {
            Button myButton = (Button)sender;

            string label = myButton.Content.ToString();
           
            if (Int32.TryParse(label, out currentDigit))
            {
                switch (currentState)
                {
                    case State.GetWholePart:
                        newValue = (newValue * 10) + currentDigit;                      
                        break;

                    case State.GetFractionPart:
                        newValue = newValue + (currentDigit * currentDecimalPlace);                        
                        currentDecimalPlace = currentDecimalPlace / 10;
                        break;
                    default:
                        break;
                }

                txtOut.Text = newValue.ToString();
            }
            else
            {
                // handle non numerical button - decimal point only
                if (label == ".")
                {
                    if (currentState == State.GetWholePart)
                    {
                        currentState = State.GetFractionPart;
                        currentDecimalPlace = 0.1;
                    }
                    else //assume entering first number starts with decimal
                    {
                        currentState = State.GetFractionPart;
                        currentDecimalPlace = 0.1;
                    }

                }
            }
           
        }

        private void Calculate(Operation op)
        {
            switch (op)
            {
                case Operation.Add:
                    newValue = currentValue + newValue;
                    currentValue = newValue;
                    break;
                case Operation.Subtract:
                    newValue = currentValue - newValue;
                    currentValue = newValue;
                    break;
                case Operation.Multiply:
                    newValue = currentValue * newValue;
                    currentValue = newValue;
                    break;
                case Operation.Divide:
                    if ((currentValue == 0) || (newValue == 0))
                    {
                        newValue = 0;
                    }
                    else
                    {
                        newValue = currentValue / newValue;
                    }
                    currentValue = newValue;
                    break;
                case Operation.Start:
                    // no operation to perform
                    
                    break;
                case Operation.Initialized:
                    currentValue = newValue;
                    break;
            }
        }

        // 4 event handlers for operations:
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            // do old calculation
            Calculate(currentOperation);

            //currentValue = newValue;
            // get ready to accept new value          
            txtOut.Text = currentValue.ToString();
            currentOperation = Operation.Add;
            currentState = State.GetWholePart;
            newValue = 0;
        }

        private void BtnSubtract_Click(object sender, RoutedEventArgs e)
        {
            // do old calculation
            Calculate(currentOperation);

            //currentValue = newValue;
            txtOut.Text = currentValue.ToString();
            currentOperation = Operation.Subtract;
            currentState = State.GetWholePart;
            newValue = 0;
        }

        private void BtnMultiply_Click(object sender, RoutedEventArgs e)
        {
            // do old calculation
            Calculate(currentOperation);
           
            //currentValue = newValue;
            txtOut.Text = currentValue.ToString();
            currentOperation = Operation.Multiply;
            currentState = State.GetWholePart;
            newValue = 0;
        }

        private void BtnDivide_Click(object sender, RoutedEventArgs e)
        {
            // do old calculation
            Calculate(currentOperation);

            //currentValue = newValue;
            txtOut.Text = currentValue.ToString();
            currentOperation = Operation.Divide;
            currentState = State.GetWholePart;
            newValue = 0;
        }

        //Clear the current results
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            currentValue = 0;
            newValue = 0;
            cumulativeValue = 0;
            currentState = State.GetWholePart;
            currentOperation = Operation.Initialized;
            txtOut.Text = currentValue.ToString();

        }

        //Handle the Equals button
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            currentState = State.CalcAnswer;
            Calculate(currentOperation);
            //currentValue = newValue;
            txtOut.Text = newValue.ToString();

            // reset state
            currentState = State.GetWholePart;
            currentOperation = Operation.Start;           
            newValue = 0;
        }

    }
}
