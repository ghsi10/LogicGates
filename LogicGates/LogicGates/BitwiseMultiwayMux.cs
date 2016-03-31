using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseMultiwayMux : Gate
    {
        public int Size { get; private set; }
        public int ControlBits { get; private set; }
        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        //your code here
        private BitwiseMux[] m_MuxArray;
        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Inputs.Length; i++)
            {
                Inputs[i] = new WireSet(Size);

            }
            //your code here
            m_MuxArray = new BitwiseMux[Inputs.Length - 1];
            for (int i = 0; i < m_MuxArray.Length; i++)
                m_MuxArray[i] = new BitwiseMux(Size);

            int cont = 0;
            for (int i = 0, x1 = Inputs.Length, x2 = Inputs.Length; i < m_MuxArray.Length; i++)
            {
                if (i == x1 / 2)
                {
                    cont++;
                    x1 = x1 + x2 / 2;
                    x2 = x2 / 2;
                }
                m_MuxArray[i].ConnectControl(Control[cont]);
            }

            int place = m_MuxArray.Length, position = 0, j = 0;
            for (int i = 0; i < place / 2 + 1; i++)
            {
                m_MuxArray[position].ConnectInput1(Inputs[j]);
                m_MuxArray[position].ConnectInput2(Inputs[j + 1]);
                position++;
                j += 2;
            }
            position = place / 2 + 1;
            place = place / 2;
            j = 0;
            for (int n = j; position != m_MuxArray.Length && j != m_MuxArray.Length; j += place, place = place / 2 + 1)
                for (int i = j; i < j + place; i++, n = n + 2, position++)
                {
                    m_MuxArray[position].ConnectInput1(m_MuxArray[n].Output);
                    m_MuxArray[position].ConnectInput2(m_MuxArray[n + 1].Output);
                }
            Output.ConnectInput(m_MuxArray[m_MuxArray.Length - 1].Output);
        }


        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }



        public override bool TestGate()
        {
            Inputs[0][0].Value = 0;
            Inputs[0][1].Value = 0;
            Inputs[0][2].Value = 1;
            Inputs[1][0].Value = 0;
            Inputs[1][1].Value = 1;
            Inputs[1][2].Value = 1;
            Inputs[2][0].Value = 1;
            Inputs[2][1].Value = 0;
            Inputs[2][2].Value = 1;
            Inputs[3][0].Value = 1;
            Inputs[3][1].Value = 1;
            Inputs[3][2].Value = 1;
            Inputs[4][0].Value = 0;
            Inputs[4][1].Value = 1;
            Inputs[4][2].Value = 0;
            Inputs[5][0].Value = 1;
            Inputs[5][1].Value = 0;
            Inputs[5][2].Value = 0;
            Inputs[6][0].Value = 1;
            Inputs[6][1].Value = 1;
            Inputs[6][2].Value = 0;
            Inputs[7][0].Value = 0;
            Inputs[7][1].Value = 0;
            Inputs[7][2].Value = 0;

            Control[0].Value = 0;
            Control[1].Value = 1;
            Control[2].Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 0)
                return false;
            Control[0].Value = 0;
            Control[1].Value = 0;
            Control[2].Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 0 || Output[2].Value != 1)
                return false;
            Control[0].Value = 1;
            Control[1].Value = 1;
            Control[2].Value = 0;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 1)
                return false;
            Control[0].Value = 1;
            Control[1].Value = 0;
            Control[2].Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 1 || Output[2].Value != 1)
                return false;
            return true;

        }
    }
}
