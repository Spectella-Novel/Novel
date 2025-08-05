using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assembly_CSharp
{
    internal class UnityTextWriter : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
        public override void Write(object value)=>Debug.Log(value);
        public override void WriteLine(object value)=>Debug.Log(value);
        public override void WriteLine(string value)=>Debug.Log(value);

        public override void Write(ulong value)=>Debug.Log(value);
        [CLSCompliant(false)]
        public override void Write(uint value)=>Debug.Log(value);

        public override void Write(string value)=>Debug.Log(value);
        public override void Write(float value)=>Debug.Log(value);

        public override void Write(long value)=>Debug.Log(value);
        public override void Write(int value)=>Debug.Log(value);
        public override void Write(double value)=>Debug.Log(value);
        public override void Write(decimal value)=>Debug.Log(value);

        public override void Write(char value)=>Debug.Log(value);
        public override void Write(bool value)=>Debug.Log(value);

        public override void WriteLine(ulong value)=>Debug.Log(value);

        public override void WriteLine(uint value)=>Debug.Log(value);
        public override void WriteLine(float value)=>Debug.Log(value);

        public override void WriteLine(long value)=>Debug.Log(value);

        public override void WriteLine(bool value)=>Debug.Log(value);
        public override void WriteLine(char value)=>Debug.Log(value);

        public override void WriteLine(decimal value)=>Debug.Log(value);
        public override void WriteLine(double value)=>Debug.Log(value);
        public override void WriteLine(int value)=>Debug.Log(value);


    }
}
