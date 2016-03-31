using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public Wire Load { get; private set; }
        public int Size { get; private set; }

        private SingleBitRegister[] m_singleRegisterArray;
        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            //your code here
            m_singleRegisterArray = new SingleBitRegister[Size];
            for (int i = 0; i < Size; i++)
            {
                m_singleRegisterArray[i] = new SingleBitRegister();
                m_singleRegisterArray[i].ConnectLoad(Load);
                m_singleRegisterArray[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(m_singleRegisterArray[i].Output);
            }
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }


        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            Input[0].Value = 1;
            Input[1].Value = 1;
            Input[2].Value = 1;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Load.Value = 0;
            Input[0].Value = 0;
            Input[1].Value = 1;
            Input[2].Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 1)
                return false;
            Clock.ClockDown();
            Clock.ClockUp();
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input[0].Value = 1;
            Input[1].Value = 1;
            Input[2].Value = 1;
            if (Output[0].Value != 0 || Output[1].Value != 1 || Output[2].Value != 1)
                return false;
            return true;
        }
    }
}
