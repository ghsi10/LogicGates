﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //this class represents a set of wires (a cable)
    class WireSet
    {
        private Wire[] m_aWires;
        public int Size { get; private set; }
        public bool InputConected { get; private set; }
        public Wire this[int i]
        {
            get
            {
                return m_aWires[i];
            }
        }

        public WireSet(int iSize)
        {
            Size = iSize;
            InputConected = false;
            m_aWires = new Wire[iSize];
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i] = new Wire();
        }
        public override string ToString()
        {
            string s = "[";
            for (int i = m_aWires.Length - 1; i >= 0; i--)
                s += m_aWires[i].Value;
            s += "]";
            return s;
        }

        //transform a positive integer value into binary and set the wires accordingly, with 0 being the LSB
        public void SetValue(int iValue)
        {
            for (int i = 0; i < Size; i++)
            {
                m_aWires[i].Value = iValue % 2;
                iValue /= 2;
            }
        }

        //transform the binary code into a positive integer
        public int GetValue()
        {
            int ans = 0;
            for (int i = 0; i < Size; i++)
                ans += m_aWires[i].Value * (int)Math.Pow(2, i);
            return ans;
        }

        //transform an integer value into binary using 2`s complement and set the wires accordingly, with 0 being the LSB
        public void Set2sComplement(int iValue)
        {
            if (iValue < 0)
                SetValue((int)Math.Pow(2, Size) + iValue);
            else
                SetValue(iValue);
        }

        //transform the binary code in 2`s complement into an integer
        public int Get2sComplement()
        {
            if (m_aWires[Size - 1].Value == 1)
                return ((int)Math.Pow(2, Size) - GetValue()) * -1;
            else
                return GetValue();
        }

        public void ConnectInput(WireSet wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            if (wIn.Size != Size)
                throw new InvalidOperationException("Cannot connect two wiresets of different sizes.");
            for (int i = 0; i < m_aWires.Length; i++)
                m_aWires[i].ConnectInput(wIn[i]);

            InputConected = true;

        }

        public WireSet toBinary(int iVal)
        {
            WireSet ws = new WireSet(Size);
            SetValue(iVal);
            for (int k = 0; k < Size; k++)
                ws[k].Value = m_aWires[k].Value;
            return ws;
        }


    }
}