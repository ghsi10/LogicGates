using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class BitwiseMultiwayDemux : Gate
    {
        public int Size { get; private set; }
        public int ControlBits { get; private set; }
        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        //your code here
        private BitwiseDemux[] m_DemuxArray;
        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            //your code here
            m_DemuxArray = new BitwiseDemux[Outputs.Length - 1];
            for (int i = 0; i < m_DemuxArray.Length; i++)
                m_DemuxArray[i] = new BitwiseDemux(Size);

            int x1 = 1, x2 = 1, control = cControlBits - 1;
            for (int i = 0; i < m_DemuxArray.Length; i++)
            {
                if (i == x1)
                {
                    control--;
                    x1 = x1 + x2 * 2;
                    x2 = x2 * 2;
                }
                m_DemuxArray[i].ConnectControl(Control[control]);
            }
            int place = m_DemuxArray.Length, position = m_DemuxArray.Length / 2, j = 0;
            for (int i = 0; i < place / 2 + 1; i++, j += 2, position++)
            {
                Outputs[j].ConnectInput(m_DemuxArray[position].Output1);
                Outputs[j + 1].ConnectInput(m_DemuxArray[position].Output2);
            }

            position = m_DemuxArray.Length - 1;
            j = m_DemuxArray.Length / 2 - 1;
            for (int i = 0; i < m_DemuxArray.Length / 2; i++, j--, position -= 2)
            {
                m_DemuxArray[position].ConnectInput(m_DemuxArray[j].Output2);
                m_DemuxArray[position - 1].ConnectInput(m_DemuxArray[j].Output1);
            }
            m_DemuxArray[0].ConnectInput(Input);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        public override bool TestGate()
        {
            Input.SetValue(2);
            Control.SetValue(0);
            for (int i = 0; i < Outputs.Length; i++)
            {
                if (i == 0)
                {
                    if (Outputs[i].GetValue() != 2)
                        return false;
                }
                else if (Outputs[i].GetValue() != 0)
                    return false;
            }
            Input.SetValue(3);
            Control.SetValue(2);
            for (int i = 0; i < Outputs.Length; i++)
            {
                if (i == 2)
                {
                    if (Outputs[i].GetValue() != 3)
                        return false;
                }
                else if (Outputs[i].GetValue() != 0)
                    return false;
            }
            Input.SetValue(1);
            Control.SetValue(3);
            for (int i = 0; i < Outputs.Length; i++)
            {
                if (i == 3)
                {
                    if (Outputs[i].GetValue() != 1)
                        return false;
                }
                else if (Outputs[i].GetValue() != 0)
                    return false;
            }
            Input.SetValue(0);
            Control.SetValue(0);
            for (int i = 0; i < Outputs.Length; i++)
            {
                if (i == 0)
                {
                    if (Outputs[i].GetValue() != 0)
                        return false;
                }
                else if (Outputs[i].GetValue() != 0)
                    return false;
            }
            return true;
        }
    }
}
