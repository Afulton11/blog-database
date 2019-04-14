using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;

namespace Mocker
{
    public class FakeDataReader : DbDataReader
    {
        public int CurrentRow = -1;
        public readonly IList<IDictionary<string, object>> data;
        public FakeDataReader(IList<IDictionary<string, object>> memoryData)
        {
            this.data = memoryData;
        }

        public override bool GetBoolean(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override byte GetByte(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override char GetChar(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedException();
        }

        public override string GetDataTypeName(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetDateTime(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override decimal GetDecimal(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override double GetDouble(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public override Type GetFieldType(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override float GetFloat(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override Guid GetGuid(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override short GetInt16(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override int GetInt32(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override long GetInt64(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override string GetName(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public override string GetString(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override object GetValue(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public override bool IsDBNull(int ordinal)
        {
            throw new NotImplementedException();
        }

        public override bool NextResult() => false;
        public override bool Read()
        {
            CurrentRow++;
            return CurrentRow < this.data.Count;
        }

        public override int Depth { get; }
        public override int FieldCount { get; }
        public override bool HasRows { get; }
        public override bool IsClosed { get; }

        public override object this[int ordinal] { get => throw new NotImplementedException(); }
        public override object this[string name]
        {
            get {
                if (this.data[CurrentRow].ContainsKey(name))
                {
                    return data[CurrentRow][name];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

        }

        public override int RecordsAffected { get; }
    }
}
