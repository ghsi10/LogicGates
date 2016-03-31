using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        //private field
        private OrGate[] m_OrArray;

        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            //array of or gates which we connect by or by the position of the bit
            m_OrArray = new OrGate[iSize];
            for (int i = 0; i < iSize; i++)
                m_OrArray[i] = new OrGate();
            for (int i = 0; i < iSize; i++)
            {
                m_OrArray[i].ConnectInput1(Input1[i]);
                m_OrArray[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(m_OrArray[i].Output);
            }
        }

        //toString method
        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
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
                    BitwiseOrGate orGate = new BitwiseOrGate(Size);
                    WireSet ws2 = new WireSet(Size);
                    ws2 = ws2.toBinary(j);
                    orGate.ConnectInput1(ws1);
                    orGate.ConnectInput2(ws2);
                    for (int k = 0; k < ws1.Size; k++)
                        if (orGate.Output[k].Value != (ws1[k].Value | ws2[k].Value))
                            return false;
                }
            }
            return true;
        }

    }
}
