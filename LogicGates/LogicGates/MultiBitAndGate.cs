using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitAndGate : MultiBitGate
    {
        //your code here
        public WireSet Input { get; private set; }
        public Wire Output { get; private set; }

        private AndGate[] m_andArray;
        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            //your code here
            Input = new WireSet(iInputCount);
            Output = new Wire();
            m_andArray = new AndGate[iInputCount - 1];
            for (int k = 0; k < iInputCount - 1; k++)
            {
                m_andArray[k] = new AndGate();
            }
            m_andArray[0].ConnectInput1(Input[0]);
            m_andArray[0].ConnectInput2(Input[1]);
            for (int i = 1; i < iInputCount - 1; i++)
            {
                m_andArray[i].ConnectInput1(m_andArray[i - 1].Output);
                m_andArray[i].ConnectInput2(Input[i + 1]);
            }
            Output = m_andArray[iInputCount - 2].Output;
        }



        public override bool TestGate()
        {
            Input[0].Value = 1;
            Input[1].Value = 0;
            Input[2].Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
