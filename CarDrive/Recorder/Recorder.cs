using System;
using System.Collections.Generic;

namespace CarDrive.Recorder
{
    class Recorder
    {
        internal readonly List<Record> Records = new List<Record>();

        internal void Add(Record record)
        {
            Records.Add(record);
        }

        internal void Clear()
        {
            Records.Clear();
        }

        public override string ToString()
        {
            string result = String.Empty;

            foreach (Record record in Records)
            {
                result += record.ToString();
            }

            return result;
        }

        public string ToString4D()
        {
            string result = String.Empty;

            foreach (Record record in Records)
            {
                result += record.ToString4D();
            }

            return result;
        }
    }
}
