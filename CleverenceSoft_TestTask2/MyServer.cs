using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleverenceSoft_TestTask2
{
    public static class MyServer
    {
        private static int count = 0;
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public static int GetCount()
        {
            _lock.EnterReadLock();
            try
            {
                return count;
            }
            finally
            {
                //Console.WriteLine($"Reader {Thread.CurrentThread.ManagedThreadId}: Count = {_count}");
                _lock.ExitReadLock();
            }
        }

        public static void AddToCount(int value)
        {
            _lock.EnterWriteLock();
            try
            {
                count += value;
            }
            finally
            {
                //Console.WriteLine($"Writer {Thread.CurrentThread.ManagedThreadId}: Added {value}, Count = {_count}");
                _lock.ExitWriteLock();
            }
        }
    }
}