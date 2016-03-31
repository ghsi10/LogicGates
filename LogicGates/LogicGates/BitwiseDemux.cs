using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }

        //your code here
        private Demux[] m_demuxArr;
        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Input = new WireSet(Size);
            //your code here
            m_demuxArr = new Demux[Size];
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);
            for (int i = 0; i < iSize; i++)
            {
                m_demuxArr[i] = new Demux();
                m_demuxArr[i].ConnectInput(Input[i]);
                m_demuxArr[i].ConnectControl(Control);
                Output1[i].ConnectInput(m_demuxArr[i].Output1);
                Output2[i].ConnectInput(m_demuxArr[i].Output2);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            Control.Value = 0;
            for (int i = 0; i < Math.Pow(2, Size); i++)
            {
                BitwiseDemux demuxGate = new BitwiseDemux(Size);
                WireSet ws = new WireSet(Size);
                ws = ws.toBinary(i);
                demuxGate.ConnectInput(ws);
                demuxGate.ConnectControl(Control);
                for (int k = 0; k < Size; k++)
                    if ((demuxGate.Output1[k].Value != ws[k].Value) || (demuxGate.Output2[k].Value != 0))
                        return false;
            }

            Control.Value = 1;
            for (int i = 0; i < Math.Pow(2, Size); i++)
            {
                BitwiseDemux demuxGate = new BitwiseDemux(Size);
                WireSet ws = new WireSet(Size);
                ws = ws.toBinary(i);
                demuxGate.ConnectInput(ws);
                demuxGate.ConnectControl(Control);
                for (int k = 0; k < Size; k++)
                    if ((demuxGate.Output2[k].Value != ws[k].Value) || (demuxGate.Output1[k].Value != 0))
                        return false;
            }
            return true;
        }

    }
}
