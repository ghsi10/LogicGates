using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseMux : BitwiseTwoInputGate
    {
        public Wire ControlInput { get; private set; }
        //your code here
        private MuxGate[] m_MuxArray;
        public BitwiseMux(int iSize)
            : base(iSize)
        {
            ControlInput = new Wire();
            //your code here
            m_MuxArray = new MuxGate[iSize];
            for (int i = 0; i < m_MuxArray.Length; i++)
            {
                m_MuxArray[i] = new MuxGate();
                m_MuxArray[i].ConnectInput1(Input1[i]);
                m_MuxArray[i].ConnectInput2(Input2[i]);
                m_MuxArray[i].ConnectControl(ControlInput);
                Output[i].ConnectInput(m_MuxArray[i].Output);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }

        public override bool TestGate()
        {
            ControlInput.Value = 0;
            for (int i = 0; i < Math.Pow(2, Size); i++)
            {
                WireSet ws1 = new WireSet(Size);
                ws1 = ws1.toBinary(i);
                for (int j = 0; j < Math.Pow(2, Size); j++)
                {
                    BitwiseMux muxGate = new BitwiseMux(Size);
                    WireSet ws2 = new WireSet(Size);
                    ws2 = ws2.toBinary(j);
                    muxGate.ConnectInput1(ws1);
                    muxGate.ConnectInput2(ws2);
                    for (int k = 0; k < ws1.Size; k++)
                        if (muxGate.Output[k].Value != (ws1[k].Value))
                            return false;
                }
            }

            ControlInput.Value = 1;
            for (int i = 0; i < Math.Pow(2, Size); i++)
            {
                WireSet ws1 = new WireSet(Size);
                ws1 = ws1.toBinary(i);
                for (int j = 0; j < Math.Pow(2, Size); j++)
                {
                    BitwiseMux muxGate = new BitwiseMux(Size);
                    WireSet ws2 = new WireSet(Size);
                    ws2 = ws2.toBinary(j);
                    muxGate.ConnectInput1(ws1);
                    muxGate.ConnectInput2(ws2);
                    muxGate.ConnectControl(ControlInput);
                    for (int k = 0; k < ws1.Size; k++)
                        if (muxGate.Output[k].Value != (ws2[k].Value))
                            return false;
                }
            }
            return true;
        }


    }
}
