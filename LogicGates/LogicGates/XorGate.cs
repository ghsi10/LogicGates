using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class XorGate : TwoInputGate
    {
        private NotGate m_gNot1;
        private NotGate m_gNot2;
        private AndGate m_gAnd1;
        private AndGate m_gAnd2;
        private OrGate m_gOr;


        public XorGate()
        {
            //init the gates
            m_gNot1 = new NotGate();
            m_gNot2 = new NotGate();
            m_gAnd1 = new AndGate();
            m_gAnd2 = new AndGate();
            m_gOr = new OrGate();
            //wire 
            m_gAnd1.ConnectInput1(m_gNot1.Output);
            m_gAnd1.ConnectInput2(m_gNot2.Input);
            m_gAnd2.ConnectInput1(m_gNot1.Input);
            m_gAnd2.ConnectInput2(m_gNot2.Output);
            m_gOr.ConnectInput1(m_gAnd1.Output);
            m_gOr.ConnectInput2(m_gAnd2.Output);
            //set the inputs and the output of the xor gate
            Output = m_gOr.Output;
            Input1 = m_gNot1.Input;
            Input2 = m_gNot2.Input;
        }

        public override string ToString()
        {
            return "Xor " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
