using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project_2
{
    public class MultiCellBuffer
    {
        private int n = 3;
        private Semaphore semaphore = new Semaphore(0, 3);
        private String[] buffer = new String[3];

        public MultiCellBuffer()
        {
            for (int i = 0; i < n; i++)
            {
                buffer[i] = "";
            }
            semaphore.Release(3);   // set semaphore to max value (3)
        }

        public String getOneCell()
        {
            lock (buffer)
            {
                for (int i = 0; i < n; i++)
                {
                    if (buffer[i] != "")    // buffer cell filled
                    {
                        return buffer[i];
                    }
                }
            }

            return "-1";
        }

        public void setOneCell(String value)
        {
            semaphore.WaitOne();
            lock (buffer)
            {
                // check if buffer cell is available
                for (int i = 0; i < n; i++)
                {
                    if (buffer[i] == "")    // buffer cell available
                    {
                        buffer[i] = value;
                        break;
                    }
                }
            }
        }

        public void clearCell(string value)
        {
            for (int i = 0; i < n; i++)
            {
                if (buffer[i] == value)
                {
                    semaphore.Release();
                    buffer[i] = "";
                    break;
                }
            }
        }
    }
}
