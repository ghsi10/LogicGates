using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        //your code here
        private NotGate[] m_gNot;

        public BitwiseNotGate(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            m_gNot = new NotGate[Size];
            //your code here
            for (int i = 0; i < iSize; i++)
            {
                m_gNot[i] = new NotGate();
                m_gNot[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(m_gNot[i].Output);
            }
        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }


        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int j = 0; j < Math.Pow(2, Size); j++)
            {
                BitwiseNotGate notGate = new BitwiseNotGate(Size);
                WireSet ws = new WireSet(Size);
                ws = ws.toBinary(j);
                notGate.ConnectInput(ws);
                for (int k = 0; k < ws.Size; k++)
                {
                    if (notGate.Output[k].Value == (ws[k].Value))
                        return false;
                }
            }
            return true;
        }

    }
}
