using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Memory : SequentialGate
    {
        public int AddressSize { get; private set; }
        public int WordSize { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public WireSet Address { get; private set; }
        public Wire Load { get; private set; }

        //your code here
        private BitwiseMultiwayDemux m_bitwiseMultiwayDemux;
        private BitwiseMultiwayMux m_bitwiseMultiwayMux;
        private MultiBitRegister[] m_registers;

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();
            //your code here
            m_bitwiseMultiwayDemux = new BitwiseMultiwayDemux(1, AddressSize);
            m_bitwiseMultiwayMux = new BitwiseMultiwayMux(WordSize, AddressSize);
            m_registers = new MultiBitRegister[(int)Math.Pow(2, AddressSize)];

            m_bitwiseMultiwayDemux.Input[0].ConnectInput(Load);
            m_bitwiseMultiwayDemux.ConnectControl(Address);
            m_bitwiseMultiwayMux.ConnectControl(Address);
            //create all the other connections between the registers,mux,demux
            for (int i = 0; i < Math.Pow(2, iAddressSize); i++)
            {
                m_registers[i] = new MultiBitRegister(iWordSize);
                m_registers[i].ConnectInput(Input);
                m_registers[i].Load.ConnectInput(m_bitwiseMultiwayDemux.Outputs[i][0]);
                m_bitwiseMultiwayMux.ConnectInput(i, m_registers[i].Output);
            }
            Output.ConnectInput(m_bitwiseMultiwayMux.Output);
        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {
            Load.Value = 1;
            Input.SetValue(3);
            Address.SetValue(0);
            Clock.ClockDown();
            Clock.ClockUp();
            Input.SetValue(2);
            Address.SetValue(1);
            Clock.ClockDown();
            Clock.ClockUp();
            Input.SetValue(1);
            Address.SetValue(3);
            Clock.ClockDown();
            Clock.ClockUp();
            Input.SetValue(0);
            Address.SetValue(2);
            Clock.ClockDown();
            Clock.ClockUp();

            Load.Value = 0;
            Address.SetValue(0);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 3)
                return false;
            Address.SetValue(1);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 2)
                return false;
            Address.SetValue(2);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 0)
                return false;
            Address.SetValue(3);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 1)
                return false;

            Clock.ClockDown();
            Clock.ClockUp();
            Clock.ClockDown();
            Clock.ClockUp();

            Load.Value = 1;
            Input.SetValue(1);
            Address.SetValue(2);
            Clock.ClockDown();
            Clock.ClockUp();

            Input.SetValue(0);
            Load.Value = 0;
            Address.SetValue(2);
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.GetValue() != 1)
                return false;

            return true;
        }

    }
}
