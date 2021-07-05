using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReflectionLessone
{
    public class Invoice : IEnumerator<InvoiceRecord>, IEnumerable<InvoiceRecord>
    {
        public DateTime OrderTime { get; set; }

        public string ClientName { get; set; }

        public List<InvoiceRecord> InvoiceRecords { get; set; }

        public InvoiceRecord Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public InvoiceRecord this[int index]
        {
            get
            {
                return InvoiceRecords[index];
            }
            set
            {
                InvoiceRecords[index] = value;
            }
        }

        public InvoiceRecord this[string name, int price]
        {
            get
            {
                return InvoiceRecords.FirstOrDefault(x => x.Name == name);
            }
            set
            {
                InvoiceRecords.FirstOrDefault(x => x.Name == name);
                //= value;
            }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<InvoiceRecord> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
