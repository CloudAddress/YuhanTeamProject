using System;
using System.IO;

namespace MilitarySimulation
{
    internal class Program
    {
        static Random random = new Random();
        static int gold = 100; // 초기자금(현재 보유금액)
        static int classM = 0; // 군 계급(0=이병)
        static int hobong = 1; // 호봉(1=1호봉)
        static float deducted = 0; // 실패확률
        static float application = 0; // 성공확률
        static float demotion = 0;//강등
        static int promotion = 0; // 진급비
        static int discharge = 0; // 전역비
        static int Reinforcement = 100; // 강화비용
        static float Destruction = 0;//파괴 확률
        static int salary = 100; //월급
        static string? input;
        static string? classA;//계급 표시

        static int successfulReinforcements = 0; // 강화 성공 횟수를 추적하는 변수
        static int failedReinforcements = 0;     // 강화 실패 횟수를 추적하는 변수
        static int dishonorableDischarges = 0;    // 불명예 전역 횟수를 추적하는 변수
        static int demotions = 0;                // 강등 횟수를 추적하는 변수
        static int promotionMisses = 0;          // 진급 누락 횟수를 추적하는 변수

        static void Main(string[] args)
        {
            Console.Title = "행복한 군생활";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //Console.SetWindowSize(100, 40); // 너비 100, 높이 40
            //DrawBorder();

            Console.ReadKey(); // 사용자가 아무 키나 누를 때까지 대기
            Console.Clear(); // 화면 지우기
            while (true)
            {
                ArmyClass();
                Console.WriteLine($"현재 계급 : {classA}/{hobong}호봉");
                
                Console.Write($"■ 현재 소지금 : ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{gold.ToString("n0")}G");
                Console.ResetColor();
                Console.Write($"  ■ 강화 비용 : ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"{Reinforcement.ToString("n0")}G\n");
                Console.ResetColor();
                Console.Write($"■ 성공 확률   : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{application}%");
                Console.ResetColor();
                Console.Write($"  ■ 실패 확률 : ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{deducted}%\n\n");
                Console.ResetColor();
                Console.Write($"● 월급 : ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"{salary.ToString("n0")}G");
                Console.ResetColor();
                Console.Write($"  ● 진급비 : ");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write($"{promotion.ToString("n0")}G");
                Console.ResetColor();
                Console.Write($"  ● 전역비 : ");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write($"{discharge.ToString("n0")}G\n");
                Console.ResetColor();
                Console.Write($"▼ 진급누락 : ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{demotion}%");
                Console.ResetColor();
                Console.Write($"  ▼ 불명예 전역 : ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{Destruction}%\n\n");
                Console.ResetColor();
                Console.WriteLine("1. 강화 하기");
                Console.WriteLine("2. 게임 종료");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ReinforceItem();
                        break;
                    case "2":
                        Console.WriteLine("게임을 종료합니다.");
                        return;
                    default:
                        Console.WriteLine("\n올바른 옵션을 선택하세요.\n");
                        break;
                }
            SaveGameDataToCSV();
            }
        }
        static void ReinforceItem()//강화시
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
                    // 사용자가 키를 누르면 입력을 처리하고 루프 종료
                    Console.ReadKey(true);
                    break;
                }
            }

            bool failed = Reinforce(deducted);

            if (gold >= Reinforcement)
            {
                gold -= Reinforcement;//소지금 차감
                if (failed)//실패
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n진급의 실패했습니다...\n");
                    failedReinforcements++;//실패 데이터
                    bool destroy = random.Next(100) < Destruction;
                    if (destroy) // 파괴(전역)
                    {
                        dishonorableDischarges++; // 불명예 전역 데이터
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n불명예 전역이...\n");
                        classM = 0;
                        hobong = 1;
                        gold = 100;
                    }
                    else //강등
                    {
                        demotions++;//강등 데이터
                        bool demote = random.Next(100) < demotion;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (demote)
                        {
                            if (classM == 4 || classM == 5 || classM == 6
                                || classM == 7 || classM == 8 || classM == 9
                                 || classM == 10 || classM == 11 || classM == 12
                                  || classM == 13 || classM == 14 || classM == 15 && hobong == 1)
                            {
                                Console.WriteLine("\n계급이 강등되었습니다...\n");
                                classM -= 1;
                                if (classM == 13 || classM == 14 || classM == 15)
                                {
                                    hobong = 1;
                                }
                                else if (classM == 4)
                                {
                                    hobong = 4;
                                }
                                else
                                {
                                    hobong = 3;
                                }
                            }
                            else
                            {
                                promotionMisses++; //진급 누락 데이터 
                                Console.WriteLine("\n진급 누락되었습니다...\n");
                                hobong -= 1;
                            }
                        }
                    }
                }
                else // 성공
                {
                    successfulReinforcements++;//성공 데이터
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    if (classM == 0 && hobong == 2 || classM == 1 && hobong == 6 || classM == 2 && hobong == 6 || classM == 3 && hobong == 4
                        || classM == 4 && hobong == 3 || classM == 5 && hobong == 3 || classM == 6 && hobong == 3
                         || classM == 7 && hobong == 3 || classM == 8 && hobong == 3 || classM == 9 && hobong == 3
                          || classM == 10 && hobong == 3 || classM == 11 && hobong == 3 || classM == 12 && hobong == 3
                           || classM == 13 && hobong == 1 || classM == 14 && hobong == 1 || classM == 15 && hobong == 1)
                    {
                        classM += 1;
                        hobong = 1;
                        gold += promotion + salary;
                        Console.WriteLine("\n진급의 성공했습니다!\n");
                    }
                    else
                    {
                        gold += salary;
                        hobong += 1;
                        Console.WriteLine("\n호봉이 올랐습니다!\n");
                    }

                }
            }
            else
            {
                Console.WriteLine("\n소지금이 부족하셔서 더 이상 게임을 하실수 없습니다ㅠㅠ\n");
            }
            Console.ResetColor(); // 콘솔 텍스트 색상을 초기화
        }
        static void SaveGameDataToCSV()
        {
            // CSV 파일 생성 및 헤더 작성
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(@"game_data.csv", false, System.Text.Encoding.GetEncoding("utf-8")))
            {
                writer.WriteLine("Event,횟수"); // CSV 파일 헤더 작성

                // 각 이벤트의 횟수를 CSV에 작성
                writer.WriteLine($"성공 횟수,{successfulReinforcements}");
                writer.WriteLine($"실패 횟수,{failedReinforcements}");
                writer.WriteLine($"불명예 횟수,{dishonorableDischarges}");
                writer.WriteLine($"강등 횟수,{demotions}");
                writer.WriteLine($"진급 누락 횟수,{promotionMisses}");
            }
            Console.WriteLine("게임 데이터가 CSV 파일로 저장되었습니다.");
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
        static bool Reinforce(float successRate)
        {
            // 랜덤한 확률을 생성하여 성공 또는 실패를 반환
            return random.Next(100) < successRate;
        }
        static void ArmyClass() // 이병~대장 
        {
            if (classM == 0)//계급)(이병)
            {
                classA = "⚊ 이병";
                if (hobong == 1)//호봉
                {
                    salary = 200;//월급
                    promotion = 0;//전급비
                    Reinforcement = 0;//강화비용

                }
                else if (hobong == 2)
                {
                    salary = 245;//월급
                    Reinforcement = 150;//강화비용
                    promotion = 500;//진급비
                }
                discharge = 0;//전역비
                application = 100;//성공
                deducted = 0;//실패
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            else if (classM == 1)//일병
            {
                classA = "⚌ 일병";
                if (hobong == 1)
                {
                    salary = 300;//월급
                    promotion = 0;//전급비
                    Reinforcement = 188;//강화비용
                    application = 98;//성공
                    deducted = 2;//실패
                }
                else if (hobong == 2)
                {
                    salary = 368;//월급
                    promotion = 0;//전급비
                    Reinforcement = 216;//강화비용
                    application = 96;//성공
                    deducted = 4;//실패
                }
                else if (hobong == 3)
                {
                    salary = 450;//월급
                    promotion = 0;//전급비
                    Reinforcement = 248;//강화비용
                    application = 94;//성공
                    deducted = 6;//실패
                }
                else if (hobong == 4)
                {
                    salary = 552;//월급
                    promotion = 0;//진급비
                    Reinforcement = 248;//강화비용
                    application = 92;//성공
                    deducted = 8;//실패
                }
                else if (hobong == 5)
                {
                    salary = 676;//월급
                    promotion = 0;//진급비
                    Reinforcement = 328;//강화비용
                    application = 90;//성공
                    deducted = 10;//실패
                }
                else if (hobong == 6)
                {
                    salary = 828;//월급
                    promotion = 903;//진급비
                    Reinforcement = 377;//강화비용
                    application = 88;//성공
                    deducted = 12;//실패
                }
                discharge = 0;//전역비
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            else if (classM == 2)//상병
            {
                classA = "☰ 상병";
                if (hobong == 1)
                {
                    salary = 1014;//월급
                    promotion = 0;//진급비
                    Reinforcement = 471;//강화비용
                    application = 86;//성공
                    deducted = 14;//실패
                }
                else if (hobong == 2)
                {
                    salary = 1242;//월급
                    promotion = 0;//전급비
                    Reinforcement = 542;//강화비용
                    application = 84;//성공
                    deducted = 16;//실패
                }
                else if (hobong == 3)
                {
                    salary = 1522;//월급
                    promotion = 0;//전급비
                    Reinforcement = 623;//강화비용
                    application = 82;//성공
                    deducted = 18;//실패
                }
                else if (hobong == 4)
                {
                    salary = 1864;//월급
                    promotion = 0;//진급비
                    Reinforcement = 717;//강화비용
                    application = 80;//성공
                    deducted = 20;//실패
                }
                else if (hobong == 5)
                {
                    salary = 2284;//월급
                    promotion = 0;//진급비
                    Reinforcement = 825;//강화비용
                    application = 78;//성공
                    deducted = 22;//실패
                }
                else if (hobong == 6)
                {
                    salary = 2798;//월급
                    promotion = 3050;//전급비
                    Reinforcement = 948;//강화비용
                    application = 76;//성공
                    deducted = 24;//실패
                }
                discharge = 0;//전역비
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            else if (classM == 3)//병장
            {
                classA = "𝌆 병장";
                if (hobong == 1)
                {
                    salary = 3427;//월급
                    promotion = 0;//진급비
                    Reinforcement = 1185;//강화비용
                    application = 74;//성공
                    deducted = 26;//실패
                }
                else if (hobong == 2)
                {
                    salary = 4198;//월급
                    promotion = 0;//전급비
                    Reinforcement = 1363;//강화비용
                    application = 72;//성공
                    deducted = 28;//실패
                }
                else if (hobong == 3)
                {
                    salary = 5143;//월급
                    promotion = 0;//전급비
                    Reinforcement = 1567;//강화비용
                    application = 70;//성공
                    deducted = 30;//실패
                }
                else if (hobong == 4)
                {
                    salary = 6300;//월급
                    promotion = 6869;//전급비
                    Reinforcement = 1803;//강화비용
                    application = 68;//성공
                    deducted = 32;//실패
                }
                discharge = 0;//전역비
                demotion = 0;//강등
                Destruction = 0;//파괴
            }
            else if (classM == 4)//하사
            {
                classA = "v 하사";
                if (hobong == 1)
                {
                    salary = 7718;//월급
                    promotion = 0;//진급비
                    discharge = 19004;//전역비
                    Reinforcement = 2433;//강화비용
                    application = 65.8f;//성공
                    deducted = 33.2f;//실패
                    demotion = 1;//강등
                }
                else if (hobong == 2)
                {
                    salary = 9454;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 2920;//강화비용
                    application = 63.8f;//성공
                    deducted = 34.1f;//실패
                    demotion = 2.2f;//강등
                }
                else if (hobong == 3)
                {
                    salary = 11581;//월급
                    promotion = 12626;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 3504;//강화비용
                    application = 61.8f;//성공
                    deducted = 34.9f;//실패
                    demotion = 3.3f;//강등
                }
                Destruction = 0;//파괴
            }
            else if (classM == 5)//중사
            {
                classA = "vv 중사";
                if (hobong == 1)
                {
                    salary = 14187;//월급
                    promotion = 0;//전급비
                    discharge = 34934;//전역비
                    Reinforcement = 4731;//강화비용
                    application = 59.6f;//성공
                    deducted = 36f;//실패
                    demotion = 4.5f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 17379;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 5677;//강화비용
                    application = 57.6f;//성공
                    deducted = 36.8f;//실패
                    demotion = 5.6f;//강등
                }
                else if (hobong == 3)
                {
                    salary = 21289;//월급
                    promotion = 23211;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 6812;//강화비용
                    application = 55.6f;//성공
                    deducted = 37.7f;//실패
                    demotion = 6.8f;//강등
                }
                Destruction = 0;//파괴
            }
            else if (classM == 6)//상사
            {
                classA = "vvv 상사";
                if (hobong == 1)
                {
                    salary = 26079;//월급
                    promotion = 0;//전급비
                    discharge = 36250;//전역비
                    Reinforcement = 9196;//강화비용
                    application = 53.4f;//성공
                    deducted = 38.7f;//실패
                    demotion = 7.9f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 31947;//월급
                    promotion = 0;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 11036;//강화비용
                    application = 51.4f;//성공
                    deducted = 39.6f;//실패
                    demotion = 9.1f;//강등
                }
                else if (hobong == 3)
                {
                    salary = 39135;//월급
                    promotion = 42667;//진급비
                    discharge = 0;//전역비
                    Reinforcement = 13243;//강화비용
                    application = 49.4f;//성공
                    deducted = 40.4f;//실패
                    demotion = 10.2f;//강등
                }
                Destruction = 0;//파괴
            }
            else if (classM == 7) //소위
            {
                classA = "♦ 소위";
                if (hobong == 1)
                {
                    salary = 47941;//월급
                    promotion = 0;//전급비
                    deducted = 41.5f;//실패
                    application = 47.2f;//성공
                    discharge = 118049;//전역비
                    Reinforcement = 17878;//강화비용
                    demotion = 11.4f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 58727;//월급
                    promotion = 0;//진급비
                    deducted = 42.3f;//실패
                    application = 45.2f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 21453;//강화비용
                    demotion = 12.5f;//강등
                }
                else if(hobong == 3)
                {
                    salary = 71941;//월급
                    promotion = 78434;//진급비
                    deducted = 43.2f;//실패
                    application = 43.2f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 25744;//강화비용
                    demotion = 13.7f;//강등
                }
                Destruction = 0;//파괴
            }
            else if (classM == 8) // 중위
            {
                classA = "♦♦ 중위";
                if (hobong == 1)
                {
                    salary = 88128;//월급
                    promotion = 0;//진급비
                    deducted = 44.2f;//실패
                    application = 41;//성공
                    discharge = 217006;//전역비
                    Reinforcement = 34755;//강화비용
                    demotion = 14.8f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 107956;//월급
                    promotion = 0;//진급비
                    deducted = 45.1f;//실패
                    application = 39;//성공
                    discharge = 0;//전역비
                    Reinforcement = 41705;//강화비용
                    demotion = 16;//강등
                }
                else if(hobong == 3)
                {
                    salary = 132247;//월급
                    promotion = 0;//진급비
                    deducted = 45.9f;//실패
                    application = 37;//성공
                    discharge = 0;//전역비
                    Reinforcement = 50047;//강화비용
                    demotion = 17.1f;//강등
                }
                Destruction = 0;//파괴
            }
            else if (classM == 9) //대위
            {
                classA = "♦♦♦ 대위";
                if (hobong == 1)
                {
                    salary = 162002;//월급
                    promotion = 0;//진급비
                    deducted = 47;//실패
                    application = 34.8f;//성공
                    discharge = 398914;//전역비
                    Reinforcement = 67563;//강화비용
                    demotion = 18.3f;//강등
                    Destruction = 0;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 198453;//월급
                    promotion = 0;//진급비
                    deducted = 47.8f;//실패
                    application = 32.8f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 81075;//강화비용
                    demotion = 19.4f;//강등
                    Destruction = 0;//파괴
                }
                else if(hobong == 3)
                {
                    salary = 243105;//월급
                    promotion = 265045;//진급비
                    deducted = 48.2f;//실패
                    application = 30.8f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 97971;//강화비용
                    demotion = 20.6f;//강등
                    Destruction = 0.5f;//파괴
                }
            }
            else if (classM == 10) //소령
            {
                classA = "✷ 소령";
                if (hobong == 1)
                {
                    salary = 297803;//월급
                    promotion = 0;//진급비
                    deducted = 47.7f;//실패
                    application = 28.6f;//성공
                    discharge = 733310;//전역비
                    Reinforcement = 131342;//강화비용
                    demotion = 21.7f;//강등
                    Destruction = 2;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 364809;//월급
                    promotion = 0;//진급비
                    deducted = 47.1f;//실패
                    application = 26.6f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 157611;//강화비용
                    demotion = 22.9f;//강등
                    Destruction = 3.5f;//파괴
                }
                else if (hobong == 3)
                {
                    salary = 446891;//월급
                    promotion = 487223;//진급비
                    deducted = 46.4f;//실패
                    application = 24.6f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 189133;//강화비용
                    demotion = 24;//강등
                    Destruction = 5;//파괴
                }
            }
            else if (classM == 11) //중령
            {
                classA = "✷✷ 중령";
                if (hobong == 1)
                {
                    salary = 547441;//월급
                    promotion = 0;//진급비
                    deducted = 46;//실패
                    application = 22.4f;//성공
                    discharge = 1348019;//전역비
                    Reinforcement = 255329;//강화비용
                    demotion = 25.2f;//강등
                    Destruction = 6.5f;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 670615;//월급
                    promotion = 0;//진급비
                    deducted = 45.3f;//실패
                    application = 20.4f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 306395;//강화비용
                    demotion = 26.3f;//강등
                    Destruction = 8;//파괴
                }
                else if(hobong == 3)
                {
                    salary = 821504;//월급
                    promotion = 895645;//진급비
                    deducted = 44.7f;//실패
                    application = 18.4f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 367674;//강화비용
                    demotion = 27.5f;//강등
                    Destruction = 9.5f;//파괴
                }
            }
            else if (classM == 12) // 대령
            {
                classA = "✷✷✷ 대령";
                if (hobong == 1)
                {
                    salary = 1006342;//월급
                    promotion = 0;//진급비
                    deducted = 44.2f;//실패
                    application = 16.2f;//성공
                    discharge = 2478017;//전역비
                    Reinforcement = 496360;//강화비용
                    demotion = 28.6f;//강등
                    Destruction = 11;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 1232769;//월급
                    promotion = 0;//진급비
                    deducted = 43.6f;//실패
                    application = 14.2f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 595632;//강화비용
                    demotion = 29.8f;//강등
                    Destruction = 12.5f;//파괴
                }
                else if (hobong == 3)
                {
                    salary = 1510142;//월급
                    promotion = 1646433;//진급비
                    deducted = 42.9f;//실패
                    application = 12.2f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 714759;//강화비용
                    demotion = 30.9f;//강등
                    Destruction = 14;//파괴
                }
            }
            else if (classM == 13) // 준장
            {
                classA = "☆ 준장 ☆";
                if (hobong == 1)
                {
                    salary = 1849924;//월급
                    promotion = 2016880;//진급비
                    discharge = 4555254;//전역비
                    deducted = 42.5f;//실패
                    application = 10f;//성공
                    Reinforcement = 964925;//강화비용
                    demotion = 32.1f;//강등
                    Destruction = 15.5f;//파괴
                }
            }
            else if (classM == 14) //소장
            {
                classA = "☆☆ 소장 ☆☆";
                if (hobong == 1)
                {
                    salary = 2266157;//월급
                    promotion = 2470678;//진급비
                    deducted = 42;//실패
                    application = 7.8f;//성공
                    discharge = 5580186;//전역비
                    Reinforcement = 1302648;//강화비용
                    demotion = 33.2f;//강등
                    Destruction = 17;//파괴
                }
            }
            else if (classM == 15) //중장
            {
                classA = "☆☆☆ 중장 ☆☆☆";
                if (hobong == 1)
                {
                    salary = 2776043;//월급
                    promotion = 3026581;//진급비
                    deducted = 41.6f;//실패
                    application = 5.6f;//성공
                    discharge = 6835728;//전역비
                    Reinforcement = 1758575;//강화비용
                    demotion = 34.4f;//강등
                    Destruction = 18.5f;//파괴
                }
            }
            else if (classM == 16) //대장
            {
                classA = "☆☆☆☆ 대장 ☆☆☆☆";
                if (hobong == 1)//호봉
                {
                    salary = 3400652;//월급
                    deducted = 41.1f;//실패 
                    application = 3.4f;//성공
                    discharge = 8373767;//전역비
                    Reinforcement = 2374076;//강화비용
                    demotion = 35.5f;//강등
                    Destruction = 20;//파괴
                }
            }
        }
    }
}
        
