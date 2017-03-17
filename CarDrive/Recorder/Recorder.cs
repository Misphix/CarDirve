using System.Collections.Generic;

namespace CarDrive.Recorder
{
    class Recorder
    {
        private readonly List<Record> _records = new List<Record>();

        public void Add(Record record)
        {
            _records.Add(record);
        }

        public void Clear()
        {
            _records.Clear();
        }
    }
}
