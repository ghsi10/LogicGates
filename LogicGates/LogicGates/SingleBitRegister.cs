using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class SingleBitRegister : Gate
    {

        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        public Wire Load { get; private set; }

        private MuxGate m_gMux;

        private DFlipFlopGate m_gFlipFlop;

        public SingleBitRegister()
        {

            Input = new Wire();
            Load = new Wire();
            //your code here 
            m_gMux = new MuxGate();
            m_gFlipFlop = new DFlipFlopGate();
            Output = new Wire();
            m_gMux.ConnectInput2(Input);
            m_gMux.ConnectControl(Load);
            m_gMux.ConnectInput1(m_gFlipFlop.Output);
            m_gFlipFlop.ConnectInput(m_gMux.Output);
            Output.ConnectInput(m_gFlipFlop.Output);
        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }



        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {
            Input.Value = 1;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 0;
            Load.Value = 0;
            if (Output.Value != 1)
                return false;
            Clock.ClockDown();
            Clock.ClockUp();
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
