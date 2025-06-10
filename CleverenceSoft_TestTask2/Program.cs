using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleverenceSoft_TestTask2
{
    public class Programm
    {
        public static void Main(string[] args)
        {
            // Запускаем несколько потоков для чтения и записи
            Task[] tasks = new Task[10];

            Random random = new Random();

            int i = 0;
            for (; i < 5; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 100; j++)
                    {
                        Console.WriteLine($"Reader {Thread.CurrentThread.ManagedThreadId}: Count = {MyServer.GetCount()}");
                        Thread.Sleep(random.Next(100)); // Имитация некоторой работы
                    }
                });
            }

            for (; i < 10; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        MyServer.AddToCount(j);

                        // Хотя, добавление к MyServer.count работает корректно и писатели работают последовательно, 
                        // но вывод именно здесь, не в MyServer.AddToCount (для), не будет корректным
                        // и за то время, после выхода из MyServer.AddToCount
                        // до вывода, другой "писатель" успеет вызвать MyServer.AddToCount

                        Console.WriteLine($"Writer {Thread.CurrentThread.ManagedThreadId}: Added {j}, Count = {MyServer.GetCount()}");
                        Thread.Sleep(random.Next(1000)); // Имитация некоторой работы
                    }
                });
            }

            Task.WaitAll(tasks);

            Console.WriteLine($"Final Count: {MyServer.GetCount()}");
        }
    }
}