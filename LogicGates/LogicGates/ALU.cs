using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class ALU : Gate
    {
        public WireSet InputX { get; private set; }
        public WireSet InputY { get; private set; }
        public WireSet Output { get; private set; }

        public Wire ZeroX { get; private set; }
        public Wire ZeroY { get; private set; }
        public Wire NotX { get; private set; }
        public Wire NotY { get; private set; }
        public Wire F { get; private set; }
        public Wire NotOutput { get; private set; }

        public Wire Zero { get; private set; }
        public Wire Negative { get; private set; }

        public int Size { get; private set; }

        //your code here
        private BitwiseMux m_zx;
        private BitwiseNotGate m_notX;
        private BitwiseMux m_muxNX;
        private BitwiseMux m_zy;
        private BitwiseNotGate m_notY;
        private BitwiseMux m_muxNY;
        private BitwiseMux m_muxF;
        private BitwiseAndGate m_andXY;
        private MultiBitAdder m_addXY;
        private BitwiseMux m_muxNO;
        private BitwiseNotGate m_notOUT;
        private BitwiseNotGate m_notZERO;
        private MultiBitAndGate m_multiAndZERO;

        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            ZeroX = new Wire();
            ZeroY = new Wire();
            NotX = new Wire();
            NotY = new Wire();
            F = new Wire();
            NotOutput = new Wire();
            Negative = new Wire();
            Zero = new Wire();

            Output = new WireSet(Size);
            m_zx = new BitwiseMux(Size);
            m_notX = new BitwiseNotGate(Size);
            m_muxNX = new BitwiseMux(Size);
            m_zy = new BitwiseMux(Size);
            m_notY = new BitwiseNotGate(Size);
            m_muxNY = new BitwiseMux(Size);
            m_muxF = new BitwiseMux(Size);
            m_andXY = new BitwiseAndGate(Size);
            m_addXY = new MultiBitAdder(Size);
            m_muxNO = new BitwiseMux(Size);
            m_notOUT = new BitwiseNotGate(Size);
            m_notZERO = new BitwiseNotGate(Size);
            m_multiAndZERO = new MultiBitAndGate(Size);

            //zero x
            m_zx.ConnectInput1(InputX);
            m_zx.ConnectControl(ZeroX);
            //not x
            m_notX.ConnectInput(m_zx.Output);
            m_muxNX.ConnectInput1(m_zx.Output);
            m_muxNX.ConnectInput2(m_notX.Output);
            m_muxNX.ConnectControl(NotX);
            //zero y
            m_zy.ConnectInput1(InputY);
            m_zy.ConnectControl(ZeroY);
            //not y
            m_notY.ConnectInput(m_zy.Output);
            m_muxNY.ConnectInput1(m_zy.Output);
            m_muxNY.ConnectInput2(m_notY.Output);
            m_muxNY.ConnectControl(NotY);
            //f
            m_andXY.ConnectInput1(m_muxNX.Output);
            m_andXY.ConnectInput2(m_muxNY.Output);
            m_addXY.ConnectInput1(m_muxNX.Output);
            m_addXY.ConnectInput2(m_muxNY.Output);
            m_muxF.ConnectInput1(m_andXY.Output);
            m_muxF.ConnectInput2(m_addXY.Output);
            m_muxF.ConnectControl(F);
            //not output
            m_notOUT.ConnectInput(m_muxF.Output);
            m_muxNO.ConnectInput1(m_muxF.Output);
            m_muxNO.ConnectInput2(m_notOUT.Output);
            m_muxNO.ConnectControl(NotOutput);
            Output.ConnectInput(m_muxNO.Output);
            //zero
            m_notZERO.ConnectInput(Output);
            m_multiAndZERO.ConnectInput(m_notZERO.Output);
            Zero.ConnectInput(m_multiAndZERO.Output);
            //negative
            Negative.ConnectInput(Output[Size - 1]);
        }

        public override bool TestGate()
        {

            InputX[0].Value = 0;
            InputX[1].Value = 1;
            InputX[2].Value = 0;
            InputX[3].Value = 1;
            InputX[4].Value = 1;
            InputX[5].Value = 0;
            InputX[6].Value = 1;
            InputX[7].Value = 0;

            InputY[0].Value = 1;
            InputY[1].Value = 1;
            InputY[2].Value = 0;
            InputY[3].Value = 1;
            InputY[4].Value = 1;
            InputY[5].Value = 0;
            InputY[6].Value = 1;
            InputY[7].Value = 1;

            ZeroX.Value = 1;
            ZeroY.Value = 1;
            F.Value = 1;
            if (Output[0].Value != 0 || Output[1].Value != 0 || Output[2].Value != 0 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 0 || Output[6].Value != 0 || Output[7].Value != 0)
                return false;
            NotX.Value = 1;
            NotY.Value = 1;
            NotOutput.Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 0 || Output[2].Value != 0 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 0 || Output[6].Value != 0 || Output[7].Value != 0)
                return false;
            NotY.Value = 0;
            NotOutput.Value = 0;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 1 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 1 || Output[6].Value != 1 || Output[7].Value != 1)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            NotY.Value = 1;
            F.Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 1 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 0)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 1)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            NotOutput.Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 0 || Output[2].Value != 1 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 1 || Output[6].Value != 0 || Output[7].Value != 1)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 0 || Output[2].Value != 1 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 1 || Output[6].Value != 0 || Output[7].Value != 0)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 1;
            if (Output[0].Value != 0 || Output[1].Value != 1 || Output[2].Value != 1 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 1 || Output[6].Value != 0 || Output[7].Value != 1)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            if (Output[0].Value != 1 || Output[1].Value != 0 || Output[2].Value != 1 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 1 || Output[6].Value != 0 || Output[7].Value != 0)
                return false;
            ZeroX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 0)
                return false;
            ZeroX.Value = 1;
            ZeroY.Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 0 || Output[2].Value != 1 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 1)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotOutput.Value = 0;
            if (Output[0].Value != 1 || Output[1].Value != 0 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 0)
                return false;
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 1 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 1)
                return false;
            ZeroX.Value = 0;
            NotX.Value = 0;
            if (Output[0].Value != 1 || Output[1].Value != 0 || Output[2].Value != 1 || Output[3].Value != 0
                || Output[4].Value != 1 || Output[5].Value != 1 || Output[6].Value != 0 || Output[7].Value != 0)
                return false;
            NotX.Value = 1;
            NotOutput.Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 1 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 1 || Output[6].Value != 1 || Output[7].Value != 0)
                return false;
            NotX.Value = 0;
            NotY.Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 0 || Output[2].Value != 0 || Output[3].Value != 0
                || Output[4].Value != 0 || Output[5].Value != 0 || Output[6].Value != 0 || Output[7].Value != 1)
                return false;
            NotY.Value = 0;
            F.Value = 0;
            NotOutput.Value = 0;
            if (Output[0].Value != 0 || Output[1].Value != 1 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 0)
                return false;
            NotX.Value = 1;
            NotY.Value = 1;
            NotOutput.Value = 1;
            if (Output[0].Value != 1 || Output[1].Value != 1 || Output[2].Value != 0 || Output[3].Value != 1
                || Output[4].Value != 1 || Output[5].Value != 0 || Output[6].Value != 1 || Output[7].Value != 1)
                return false;
            return true;

        }
    }
}
