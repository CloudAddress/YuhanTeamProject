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
        static float Destruction = 0;//파괴 확률
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
         if (classM == 16) //대장
            {
                if (hobong == 1)//호봉
                {
                    deducted = 41.1f;//실패 
                    application = 3.4f;//성공
                    discharge = 8373767;//전역비
                    salary = 1700326;//월급
                    promotion = 3026581;//진급비
                    Reinforcement = 2374076;//강화비용
                    demotion = 35.5f;//강등
                    Destruction = 20;//파괴

                }
}


if (classM == 15) //중장
{
    if (hobong == 1)
    {
        deducted = 41.6f;//실패
        application = 5.6f;//성공
        discharge = 6835728;//전역비
        salary = 1388021;//월급
        promotion = 2470678;//진급비
        Reinforcement = 1758575;//강화비용
        demotion = 34.4f;//강등
        Destruction = 18.5f;//파괴

    }
}

if (classM == 14) //소장
{
    if (hobong == 1)
    {
        deducted = 42;//실패
        application = 7.8f;//성공
        discharge = 5580186;//전역비
        salary = 1133079;//월급
        promotion = 2016880;//진급비
        Reinforcement = 1302648;//강화비용
        demotion = 33.2f;//강등
        Destruction = 17;//파괴

    }
}

if (classM == 13) // 준장
{
    if (hobong == 1)
    {
        deducted = 42.5f;//실패
        application = 3.4f;//성공
        discharge = 4555254;//전역비
        salary = 924962;//월급
        promotion = 1646433;//진급비
        Reinforcement = 964925;//강화비용
        demotion = 32.1f;//강등
        Destruction = 15.5f;//파괴

    }
}

if (classM == 12) // 대령
{
    if (hobong == 3)
    {
        deducted = 42.9f;//실패
        application = 12.2f;//성공
        discharge = 0;//전역비
        salary = 0;//월급
        promotion = 0;//진급비
        Reinforcement = 714759;//강화비용
        demotion = 30.9f;//강등
        Destruction = 14;//파괴

    }

    else if (hobong == 2)
    {
        deducted = 43.6f;//실패
        application = 14.2f;//성공
        discharge = 0;//전역비
        salary = 616385;//월급
        promotion = 0;//진급비
        Reinforcement = 595632;//강화비용
        demotion = 29.8f;//강등
        Destruction = 12.5f;//파괴
    }

    else if (hobong == 1)
    {
        deducted = 44.2f;//실패
        application = 16.2f;//성공
        discharge = 2478017;//전역비
        salary = 503171;//월급
        promotion = 895645;//진급비
        Reinforcement = 496360;//강화비용
        demotion = 28.6f;//강등
        Destruction = 11;//파괴
    }
}

if (classM == 11) //중령
{
    if (hobong == 3)
    {
        deducted = 44.7f;//실패
        application = 18.4f;//성공
        discharge = 0;//전역비
        salary = 410752;//월급
        promotion = 0;//진급비
        Reinforcement = 367674;//강화비용
        demotion = 27.5f;//강등
        Destruction = 9.5f;//파괴

    }

    else if (hobong == 2)
    {
        deducted = 45.3f;//실패
        application = 20.4f;//성공
        discharge = 0;//전역비
        salary = 335308;//월급
        promotion = 0;//진급비
        Reinforcement = 306395;//강화비용
        demotion = 26.3f;//강등
        Destruction = 8;//파괴
    }

    else if (hobong == 1)
    {
        deducted = 46;//실패
        application = 22.4f;//성공
        discharge = 1348019;//전역비
        salary = 273721;//월급
        promotion = 487223;//진급비
        Reinforcement = 255329;//강화비용
        demotion = 25.2f;//강등
        Destruction = 6.5f;//파괴
    }
}

if (classM == 10) //소령
{
    if (hobong == 3)
    {
        deducted = 46.4f;//실패
        application = 24.6f;//성공
        discharge = 0;//전역비
        salary = 223445;//월급
        promotion = 0;//진급비
        Reinforcement = 189133;//강화비용
        demotion = 24;//강등
        Destruction = 5;//파괴

    }

    else if (hobong == 2)
    {
        deducted = 47.1f;//실패
        application = 26.6f;//성공
        discharge = 0;//전역비
        salary = 182404;//월급
        promotion = 0;//진급비
        Reinforcement = 157611;//강화비용
        demotion = 22.9f;//강등
        Destruction = 3.5f;//파괴

    }

    else if (hobong == 1)
    {
        deducted = 47.7f;//실패
        application = 28.6f;//성공
        discharge = 733310;//전역비
        salary = 148902;//월급
        promotion = 265045;//진급비
        Reinforcement = 131342;//강화비용
        demotion = 21.7f;//강등
        Destruction = 2;//파괴
    }

    if (classM == 9) //대위
    {
        if (hobong == 3)
        {
            deducted = 48.2f;//실패
            application = 30.8f;//성공
            discharge = 0;//전역비
            salary = 121552;//월급
            promotion = 121552;//진급비
            Reinforcement = 189133;//강화비용
            demotion = 20.6f;//강등
            Destruction = 0.5f;//파괴

        }

        else if (hobong == 2)
        {
            deducted = 47.8f;//실패
            application = 32.8f;//성공
            discharge = 0;//전역비
            salary = 99226;//월급
            promotion = 0;//진급비
            Reinforcement = 81075;//강화비용
            demotion = 19.4f;//강등
            Destruction = 0;//파괴
        }

        else if (hobong == 1)
        {
            deducted = 47;//실패
            application = 34.8f;//성공
            discharge = 398914;//전역비
            salary = 81001;//월급
            promotion = 144182;//진급비
            Reinforcement = 67563;//강화비용
            demotion = 18.3f;//강등
            Destruction = 0;//파괴
        }

    }

    if (classM == 8) // 중위
    {
        if (hobong == 3)
        {

            deducted = 45.9f;//실패
            application = 37;//성공
            discharge = 0;//전역비
            salary = 66123;//월급
            promotion = 0;//진급비
            Reinforcement = 50047;//강화비용
            demotion = 20.6f;//강등
            Destruction = 0;//파괴
        }

        else if (hobong == 2)
        {
            deducted = 45.1f;//실패
            application = 39;//성공
            discharge = 0;//전역비
            salary = 53978;//월급
            promotion = 0;//진급비
            Reinforcement = 41705;//강화비용
            demotion = 16;//강등
            Destruction = 0;//파괴

        }

        else if (hobong == 1)
        {
            deducted = 44.2f;//실패
            application = 41;//성공
            discharge = 217006;//전역비
            salary = 44, 064;//월급
            promotion = 78434;//진급비
            Reinforcement = 34755;//강화비용
            demotion = 14.8f;//강등
            Destruction = 0;//파괴

        }
    }

    if (classM == 7) //소위
    {
        if (hobong == 3)
        {
            deducted = 43.2f;//실패
            application = 43.2f;//성공
            discharge = 0;//전역비
            salary = 35971;//월급
            promotion = 0;//진급비
            Reinforcement = 25744;//강화비용
            demotion = 13.7f;//강등
            Destruction = 0;//파괴

        }

        else if (hobong == 2)
        {
            deducted = 42.3f;//실패
            application = 45.2f;//성공
            discharge = 0;//전역비
            salary = 29364;//월급
            promotion = 0;//진급비
            Reinforcement = 21453;//강화비용
            demotion = 12.5f;//강등
            Destruction = 0;//파괴
        }

        else if (hobong == 1)
        {
            deducted = 41.5f;//실패
            application = 47.2f;//성공
            discharge = 11049;//전역비
            salary = 23970;//월급
            promotion = 42667;//진급비
            Reinforcement = 17878;//강화비용
            demotion = 11.4f;//강등
            Destruction = 0;//파괴

        }
    }
}
