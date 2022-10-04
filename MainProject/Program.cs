using System;
using System.IO;
using System.Text;

namespace MainProject {
    class Program {
        public static void Main(string[] args) {
            Console.WriteLine("Main Start... {0}", Thread.CurrentThread.ManagedThreadId); //메인 시작
            PrintHelloWorldByAsync("C:\\Users\\Guestes\\RiderProjects\\CSharpHelloWorld\\MainProject\\Hello.txt");
            for (int i = 0; i < 10; i++) {
                Console.WriteLine("{0} 번째 for문, 100ms마다 딜레이", i + 1);
                Thread.Sleep(100);
            }
        }

        private static async void PrintHelloWorldByAsync(string path) {
            Console.WriteLine("Start Reading... {0}", Thread.CurrentThread.ManagedThreadId); //읽기 시작
            byte[] readArray = new byte[15]; //버퍼 배열 선언
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate); //path -> 스트림
            int read = await stream.ReadAsync(readArray, 0, readArray.Length); //비동기 io -> 버퍼에 읽기 시작, await를 붙일경우 Task에 선언된 제네릭 값을 바로 리턴해줌, await를 붙였으므로, 메인쓰레드는 관여 X, 워커쓰레드가 대기
            await Delay();
            Console.WriteLine("Read Value {0}, Current Thread {1}", Encoding.Default.GetString(readArray).Trim(), Thread.CurrentThread.ManagedThreadId);
            //여기서 중요한점 = 만약 await로 실행한 작업이 mainThread가 금방 끝낼 수 있는 작업 => 바로 메인쓰레드가 작업, Task.Delay() 혹은 비동기 작업이 오래 걸리는 경우 => 워커 쓰레드가 작동 = ReadAsync이 특이하게 작동하는듯?
            
        }

        private static async Task Delay() {
            await Task.Delay(500); //0.5초 delay
        }
    }
}