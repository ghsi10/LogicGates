using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; set; }
        public Wire CarryOutput { get; private set; }

        //your code here
        private HalfAdder m_halfAdd1;
        private HalfAdder m_halfAdd2;
        private OrGate m_gOr;

        public FullAdder()
        {
            CarryInput = new Wire();
            CarryOutput = new Wire();
            //your code here
            m_halfAdd1 = new HalfAdder();
            m_halfAdd2 = new HalfAdder();
            m_gOr = new OrGate();
            m_halfAdd1.ConnectInput1(Input1);
            m_halfAdd1.ConnectInput2(Input2);
            m_halfAdd2.ConnectInput1(m_halfAdd1.Output);
            m_halfAdd2.ConnectInput2(CarryInput);
            m_gOr.ConnectInput1(m_halfAdd1.CarryOutput);
            m_gOr.ConnectInput2(m_halfAdd2.CarryOutput);
            Output.ConnectInput(m_halfAdd2.Output);
            CarryOutput.ConnectInput(m_gOr.Output);
        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            CarryInput.Value = 0;
            Input1.Value = 0;
            Input2.Value = 0;
            if ((Output.Value != 0) || (CarryOutput.Value != 0))
                return false;
            CarryInput.Value = 1;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            CarryInput.Value = 0;
            Input1.Value = 0;
            Input2.Value = 1;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            CarryInput.Value = 1;
            if ((Output.Value != 0) || (CarryOutput.Value != 1))
                return false;
            CarryInput.Value = 0;
            Input1.Value = 1;
            Input2.Value = 0;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            CarryInput.Value = 1;
            Input1.Value = 1;
            Input2.Value = 0;
            if ((Output.Value != 0) || (CarryOutput.Value != 1))
                return false;
            CarryInput.Value = 0;
            Input1.Value = 1;
            Input2.Value = 1;
            if ((Output.Value != 0) || (CarryOutput.Value != 1))
                return false;
            CarryInput.Value = 1;
            Input1.Value = 1;
            Input2.Value = 1;
            if ((Output.Value != 1) || (CarryOutput.Value != 1))
                return false;
            return true;

        }
    }
}
