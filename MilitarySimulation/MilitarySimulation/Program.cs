using System;

namespace MilitarySimulation
{
    internal class Program
    {
        static Random random = new Random();
        static int gold = 0; // 초기자금(현재 보유금액)
        static int classM = 0; // 군 계급(0=이병)
        static int hobong = 1; // 호봉(1=1호봉)
        static float deducted = 0; // 실패확률
        static float application = 0; // 적용확률
        static float demotion = 0;//강등
        static int promotion = 0; // 진급비
        static int discharge = 0; // 전역비
        static int Reinforcement = 100; // 강화비용
        static int Destruction = 0;//파괴 확률
        static int salary = 100; //월급
        static string input;

        static void Main(string[] args)
        {
            Console.Title = "행복한 군생활";
            //Console.SetWindowSize(100, 40); // 너비 100, 높이 40
            //DrawBorder();

            string text = "이 텍스트가 화면 정중앙에 나타납니다.";

            // 텍스트의 길이를 측정하여 중앙에 출력할 위치를 계산
            int left = (100 - text.Length) / 2;
            int top = 40 / 2;
            Console.SetCursorPosition(left, top);
            Console.WriteLine("게임을 시작하려면 아무키나 입력하세요");
            Console.ReadKey(); // 사용자가 아무 키나 누를 때까지 대기
            Console.Clear(); // 화면 지우기

            while (true)
            {
                Console.WriteLine($"현재 보유한 골드: {gold}G");
                Console.WriteLine($"강화 비용 : {Reinforcement}G");
                Console.WriteLine("1. 강화하기");
                Console.WriteLine("2. 팀원");
                Console.WriteLine("3. 나가기");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ReinforceItem();
                        break;
                    case "2":
                        Console.WriteLine(",,,,,");
                        break;
                    case "3":
                        Console.WriteLine("게임을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("\n올바른 옵션을 선택하세요.\n");
                        break;
                }
            }
        }
        static void ReinforceItem()
        {
            Console.Clear(); // 화면 지우기
            //DrawBorder();
            Console.Write("아이템을 강화합니다");

            string text = "...."; // 출력할 문자열

            foreach (char c in text)
            {
                Console.Write(c); // 한 문자씩 출력
                System.Threading.Thread.Sleep(200); // 0.2초 대기

                if (Console.KeyAvailable)
                {
                    // 사용자가 키를 누르면 입력을 처리하고 루프를 종료
                    Console.ReadKey(true);
                    break;
                }
            }

            // 50% 확률로 강화 성공 또는 실패
            bool success = random.Next(0, 2) == 0;

            if (success)
            {
                if (gold >= Reinforcement)
                {
                    gold -= Reinforcement;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n아이템을 성공적으로 강화했습니다!\n");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n골드가 부족합니다.\n");
                    Console.Beep(); // 경고음
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n아이템 강화에 실패했습니다...\n");
            }
            Console.ResetColor(); // 콘솔 텍스트 색상을 초기화
        }
        static void DrawBorder()
        {
            int width = 100;
            int height = 40;

            // 상단 테두리
            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            for (int i = 1; i < width - 1; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┐");

            // 중간 내용
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
                Console.SetCursorPosition(width - 1, i);
                Console.WriteLine("│");
            }

            // 하단 테두리
            Console.SetCursorPosition(0, height - 1);
            Console.Write("└");
            for (int i = 1; i < width - 1; i++)
            {
                Console.Write("─");
            }
            Console.WriteLine("┘");
        }
        static void Test()
        {
            if (classM == 0)//계급)(이병)
            {
                if (hobong == 1)//호봉
                {
                    salary = 0;//월급

                    Reinforcement = 0;//강화비용

                }
                if (hobong == 2)
                {
                    salary = 123;//월급
                    Reinforcement = 150;//강화비용
                }
                promotion = 0;//전급비
                discharge = 0;//전역비
                application = 100;//성공
                deducted = 0;//실패
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            if (classM == 1)
            {
                if (hobong == 1)
                {
                    salary = 150;//월급
                    promotion = 500;//전급비
                    Reinforcement = 188;//강화비용
                    application = 98;//성공
                    deducted = 2;//실패
                }
                if(hobong == 2)
                {
                    salary = 184;//월급
                    promotion = 0;//전급비
                    Reinforcement = 216;//강화비용
                    application = 96;//성공
                    deducted = 4;//실패
                }
                if (hobong == 3)
                {
                    salary = 255;//월급
                    promotion = 0;//전급비
                    Reinforcement = 248;//강화비용
                    application = 94;//성공
                    deducted = 6;//실패
                }
                if (hobong == 4)
                {
                    salary = 276;//월급
                    promotion = 0;//진급비
                    Reinforcement = 248;//강화비용
                    application = 92;//성공
                    deducted = 8;//실패
                }
                if (hobong == 5)
                {
                    salary = 338;//월급
                    promotion = 0;//진급비
                    Reinforcement = 328;//강화비용
                    application = 90;//성공
                    deducted = 10;//실패
                }
                if (hobong == 6)
                {
                    salary = 414;//월급
                    promotion = 0;//진급비
                    Reinforcement = 377;//강화비용
                    application = 88;//성공
                    deducted = 12;//실패
                }
                discharge = 0;//전역비
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            if (classM == 2)//상병
            {
                if (hobong == 1)
                {
                    salary = 507;//월급
                    promotion = 903;//전급비
                    Reinforcement = 471;//강화비용
                    application = 86;//성공
                    deducted = 14;//실패
                }
                if (hobong == 2)
                {
                    salary = 621;//월급
                    promotion = 0;//전급비
                    Reinforcement = 542;//강화비용
                    application = 84;//성공
                    deducted = 16;//실패
                }
                if (hobong == 3)
                {
                    salary = 761;//월급
                    promotion = 0;//전급비
                    Reinforcement = 623;//강화비용
                    application = 82;//성공
                    deducted = 18;//실패
                }
                if (hobong == 4)
                {
                    salary = 932;//월급
                    promotion = 0;//진급비
                    Reinforcement = 717;//강화비용
                    application = 80;//성공
                    deducted = 20;//실패
                }
                if (hobong == 5)
                {
                    salary = 1142;//월급
                    promotion = 0;//진급비
                    Reinforcement = 825;//강화비용
                    application = 78;//성공
                    deducted = 22;//실패
                }
                if (hobong == 6)
                {
                    salary = 1399;//월급
                    promotion = 0;//진급비
                    Reinforcement = 948;//강화비용
                    application = 76;//성공
                    deducted = 24;//실패
                }
                discharge = 0;//전역비
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            if (classM == 3)//병장
            {
                if (hobong == 1)
                {
                    salary = 1714;//월급
                    promotion = 3050;//전급비
                    Reinforcement = 1185;//강화비용
                    application = 74;//성공
                    deducted = 26;//실패
                }
                if (hobong == 2)
                {
                    salary = 2099;//월급
                    promotion = 0;//전급비
                    Reinforcement = 1363;//강화비용
                    application = 72;//성공
                    deducted = 28;//실패
                }
                if (hobong == 3)
                {
                    salary = 2571;//월급
                    promotion = 0;//전급비
                    Reinforcement = 1567;//강화비용
                    application = 70;//성공
                    deducted = 30;//실패
                }
                if (hobong == 4)
                {
                    salary = 3150;//월급
                    promotion = 0;//진급비
                    Reinforcement = 1803;//강화비용
                    application = 68;//성공
                    deducted = 32;//실패
                }
                discharge = 0;//전역비
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            if (classM == 4)//하사
            {
                if (hobong == 1)
                {
                    salary = 3859;//월급
                    promotion = 6869;//전급비
                    discharge = 19004;//전역비
                    Reinforcement = 2433;//강화비용
                    application = 65.8f;//성공
                    deducted = 33.2f;//실패
                    demotion = 1;//강등
                }
                if (hobong == 2)
                {
                    salary = 5727;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 2920;//강화비용
                    application = 63.8f;//성공
                    deducted = 34.1f;//실패
                    demotion = 2.2f;//강등
                }
                if (hobong == 3)
                {
                    salary = 5791;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 3504;//강화비용
                    application = 61.8f;//성공
                    deducted = 34.9f;//실패
                    demotion = 3.3f;//강등
                }
                Destruction = 0;//파괴
            }
            if (classM == 5)//중사
            {
                if (hobong == 1)
                {
                    salary = 7093;//월급
                    promotion = 12626;//전급비
                    discharge = 34934;//전역비
                    Reinforcement = 4731;//강화비용
                    application = 59.6f;//성공
                    deducted = 36f;//실패
                    demotion = 4.5f;//강등
                }
                if (hobong == 2)
                {
                    salary = 8689;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 5677;//강화비용
                    application = 57.6f;//성공
                    deducted = 36.8f;//실패
                    demotion = 5.6f;//강등
                }
                if (hobong == 3)
                {
                    salary =  10645;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 6812;//강화비용
                    application = 55.6f;//성공
                    deducted = 37.7f;//실패
                    demotion = 6.8f;//강등
                }
                Destruction = 0;//파괴
            }
            if (classM == 5)//상사
            {
                if (hobong == 1)
                {
                    salary = 13040;//월급
                    promotion = 23211;//전급비
                    discharge = 36250;//전역비
                    Reinforcement = 9196;//강화비용
                    application = 53.4f;//성공
                    deducted = 38.7f;//실패
                    demotion = 7.9f;//강등
                }
                if (hobong == 2)
                {
                    salary = 15974;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 11036;//강화비용
                    application = 51.4f;//성공
                    deducted = 39.6f;//실패
                    demotion = 9.1f;//강등
                }
                if (hobong == 3)
                {
                    salary = 19568;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 13243;//강화비용
                    application = 49.4f;//성공
                    deducted = 40.4f;//실패
                    demotion = 10.2f;//강등
                }
                Destruction = 0;//파괴
            }
        }
    }
}
