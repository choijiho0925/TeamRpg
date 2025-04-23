using System;
using System.Runtime.InteropServices;

namespace TeamRpg
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int MAXIMIZE = 3;
    // Program 클래스 - 프로그램의 진입점 역할을 합니다.
    class Program
    {
        // Main 메서드 - 프로그램이 시작되는 지점입니다.
        static void Main(string[] args)
        {
            try
            {
                // 콘솔 윈도우 최대화
                IntPtr consoleWindow = GetConsoleWindow();
                ShowWindow(consoleWindow, MAXIMIZE);

                // 콘솔 제목 설정
                Console.Title = "Team RPG Adventure";

                // 게임 인스턴스 가져오기
                Game game = Game.Instance;

                // 게임 시작
                game.Start();
            }
            catch (Exception ex)
            {
                // 예외 발생 시 오류 메시지 출력
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("게임 실행 중 오류가 발생했습니다:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                // 사용자가 오류 메시지를 볼 수 있도록 잠시 대기
                Console.WriteLine("\n종료하려면 아무 키나 누르세요...");
                Console.ReadKey();
            }
        }
    }
}