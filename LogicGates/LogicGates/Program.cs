using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {
            //test and gate
            AndGate and = new AndGate();
            if (!and.TestGate())
                Console.WriteLine("bugbug and");

            //test or gate
            OrGate or = new OrGate();
            if (!or.TestGate())
                Console.WriteLine("bugbug or");

            //test xor gate
            XorGate xor = new XorGate();
            if (!xor.TestGate())
                Console.WriteLine("bugbug xor");

            //test mux gate
            MuxGate mux = new MuxGate();
            if (!mux.TestGate())
                Console.WriteLine("bugbug mux");

            //test demux gate
            Demux demux = new Demux();
            if (!demux.TestGate())
                Console.WriteLine("bugbug demux");

            //test bitwiseAnd gate
            BitwiseAndGate bitWiseAnd = new BitwiseAndGate(4);
            if (!bitWiseAnd.TestGate())
                Console.WriteLine("bugbug bitWiseAnd");

            //test bitwiseOr gate
            BitwiseOrGate bitWiseOr = new BitwiseOrGate(4);
            if (!bitWiseOr.TestGate())
                Console.WriteLine("bugbug bitWiseOr");

            //test bitwiseNot gate
            BitwiseNotGate bitWiseNot = new BitwiseNotGate(4);
            if (!bitWiseNot.TestGate())
                Console.WriteLine("bugbug bitWiseNot");

            //test halfAdder 
            HalfAdder halfAdd = new HalfAdder();
            if (!halfAdd.TestGate())
                Console.WriteLine("bugbug halfAdder");

            //test fullAdder
            FullAdder fullAdd = new FullAdder();
            if (!fullAdd.TestGate())
                Console.WriteLine("bugbug fullAdder");

            //test bitwiseMux gate
            BitwiseMux bitWiseMux = new BitwiseMux(4);
            if (!bitWiseMux.TestGate())
                Console.WriteLine("bugbug bitWiseMux");

            // test bitwiseDemux gate
            BitwiseDemux bitWiseDemux = new BitwiseDemux(3);
            if (!bitWiseDemux.TestGate())
                Console.WriteLine("bugbug bitwiseDemux");

            // test multibitAnd gate
            MultiBitAndGate multiAnd = new MultiBitAndGate(3);
            if (!multiAnd.TestGate())
                Console.WriteLine("bugbug multiAnd");

            //test bitwiseMultiwayMux
            BitwiseMultiwayMux multiwayMux = new BitwiseMultiwayMux(3, 3);
            if (!multiwayMux.TestGate())
                Console.WriteLine("bugbug multiwayMux");

            MultiBitAdder bitAdder = new MultiBitAdder(4);
            if (!bitAdder.TestGate())
                Console.WriteLine("bugbug multiAdder");

            //test bitwiseMultiwayDemux
            BitwiseMultiwayDemux multiwayDemux = new BitwiseMultiwayDemux(3, 3);
            if (!multiwayDemux.TestGate())
                Console.WriteLine("bugbug multiwayDemux");

            //test ALU
            ALU alu = new ALU(8);
            if (!alu.TestGate())
                Console.WriteLine("bugbug ALU");

            //test singelBitRegister
            SingleBitRegister singleBitReg = new SingleBitRegister();
            if (!singleBitReg.TestGate())
                Console.WriteLine("bugbug singelBitRegister");

            //test multiBitRegister
            MultiBitRegister multiBitReg = new MultiBitRegister(3);
            if (!multiBitReg.TestGate())
                Console.WriteLine("bugbug multiBitRegister");

            //test Memory
            Memory memory = new Memory(2, 8);
            if (!memory.TestGate())
                Console.WriteLine("bugbug memory");

            //test setValue, getValue
            WireSet wires = new WireSet(8);
            wires.SetValue(5);
            if (wires.GetValue() != 5)
                Console.WriteLine("bugbug");
            //test Set2sComplement, Get2sComplement
            wires.Set2sComplement(7);
            if (wires.Get2sComplement() != 7)
                Console.WriteLine("bugbug");
            wires.Set2sComplement(-54);
            if (wires.Get2sComplement() != -54)
                Console.WriteLine("bugbug");
            wires.Set2sComplement(-3);
            if (wires.Get2sComplement() != -3)
                Console.WriteLine("bugbug");

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
