using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitAdder : Gate
    {
        public int Size { get; private set; }
        public WireSet Input1 { get; private set; }
        public WireSet Input2 { get; private set; }
        public WireSet Output { get; private set; }
        public Wire Overflow { get; private set; }

        private FullAdder[] m_fulAdderArray;

        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Overflow = new Wire();
            Input1 = new WireSet(iSize);
            Input2 = new WireSet(iSize);
            Output = new WireSet(iSize);
            m_fulAdderArray = new FullAdder[iSize];

            for (int i = 0; i < iSize; i++)
                m_fulAdderArray[i] = new FullAdder();

            m_fulAdderArray[0].CarryInput.Value = 0;
            m_fulAdderArray[0].ConnectInput1(Input1[0]);
            m_fulAdderArray[0].ConnectInput2(Input2[0]);
            Output[0].ConnectInput(m_fulAdderArray[0].Output);
            //your code here
            for (int i = 1; i < iSize; i++)
            {
                m_fulAdderArray[i].ConnectInput1(Input1[i]);
                m_fulAdderArray[i].ConnectInput2(Input2[i]);
                m_fulAdderArray[i].CarryInput.ConnectInput(m_fulAdderArray[i - 1].CarryOutput);
                Output[i].ConnectInput(m_fulAdderArray[i].Output);
            }
            Overflow.ConnectInput(m_fulAdderArray[iSize - 1].CarryOutput);
        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            Input2[0].Value = 0;
            Input2[1].Value = 1;
            Input2[2].Value = 1;
            Input2[3].Value = 1;

            Input1[0].Value = 0;
            Input1[1].Value = 1;
            Input1[2].Value = 0;
            Input1[3].Value = 1;
            if ((Output[0].Value != 0) || (Output[1].Value != 0) || (Output[2].Value != 0) || (Output[3].Value != 1) || (Overflow.Value != 1))
                return false;
            Input1.Set2sComplement(-2);
            Input2.Set2sComplement(-1);
            if (Output.Get2sComplement() != -3)
                return false;
            return true;
        }
    }
}
