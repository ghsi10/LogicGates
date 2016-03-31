using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; private set; }

        //your code here
        private XorGate m_gXor;
        private AndGate m_gAnd;

        public HalfAdder()
        {
            //your code here
            CarryOutput = new Wire();
            m_gXor = new XorGate();
            m_gAnd = new AndGate();
            m_gXor.ConnectInput1(Input1);
            m_gXor.ConnectInput2(Input2);
            m_gAnd.ConnectInput1(Input1);
            m_gAnd.ConnectInput2(Input2);
            CarryOutput.ConnectInput(m_gAnd.Output);
            Output.ConnectInput(m_gXor.Output);
        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if ((Output.Value != 0) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if ((Output.Value != 0) || (CarryOutput.Value != 1))
                return false;
            return true;
        }
    }
}
