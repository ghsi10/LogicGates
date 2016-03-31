using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        //private field
        private AndGate[] m_AndArray;
        private int m_size;

        public BitwiseAndGate(int iSize)
            : base(iSize)
        {
            //array of and gates which we connect by the position of the bit
            m_AndArray = new AndGate[iSize];
            m_size = iSize;
            for (int i = 0; i < iSize; i++)
                m_AndArray[i] = new AndGate();
            for (int i = 0; i < iSize; i++)
            {
                m_AndArray[i].ConnectInput1(Input1[i]);
                m_AndArray[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_AndArray[i].Output);
            }
        }

        // toString method
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        //testGate method
        public override bool TestGate()
        {
            for (int i = 0; i < Math.Pow(2, Size); i++)
            {
                WireSet ws1 = new WireSet(Size);
                ws1 = ws1.toBinary(i);
                for (int j = 0; j < Math.Pow(2, Size); j++)
                {
                    BitwiseAndGate andGate = new BitwiseAndGate(Size);
                    WireSet ws2 = new WireSet(Size);
                    ws2 = ws2.toBinary(j);
                    andGate.ConnectInput1(ws1);
                    andGate.ConnectInput2(ws2);
                    for (int k = 0; k < ws1.Size; k++)
                        if (andGate.Output[k].Value != (ws1[k].Value & ws2[k].Value))
                            return false;
                }
            }
            return true;
        }
    }



}
