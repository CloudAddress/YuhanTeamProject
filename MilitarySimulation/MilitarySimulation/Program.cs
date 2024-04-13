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
        static string classA = "⚊ 이병";//계급 표시
        static bool isDischarge=false;
        static string t="";
        static bool isD=false;//전역을 눌렀는지 확인후
        static bool isDead=false;//소지금 부족으로 죽었을 경우
        static bool isa=false;

        static int successfulReinforcements = 0; // 강화 성공 횟수를 추적하는 변수
        static int failedReinforcements = 0;     // 강화 실패 횟수를 추적하는 변수
        static int dishonorableDischarges = 0;    // 불명예 전역 횟수를 추적하는 변수
        static int demotions = 0;                // 강등 횟수를 추적하는 변수
        static int promotionMisses = 0;          // 진급 누락 횟수를 추적하는 변수
        static string DeadClass= "⚊ 이병"; //죽었을 때 계급을 추적하는 변수
        static int DeadHobong=1; //죽었을 때 호봉을 추적하는 변수
        static int homedischarges = 0; //전역 횟수 추적변수
        static int fool; //소지금이 없어 진행이 안되는 상태에서 얼마나 연타를 계속 하는지 저장하는 변수
        static DateTime startTime; // 시작 시간을 저장할 변수
        static int re;//다시 시작을 몇번 했는지 추적하는 변수
        static int maxClass = 0; // 최고 계급을 저장할 변수
        static string maxClassK = "⚊ 이병";
        static int maxHobong = 0; // 최고 호봉을 저장할 변수
        static void Main(string[] args)
        {
            Console.Title = "행복한 군생활";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            startTime = DateTime.Now;// 현재 시간을 시작 시간으로 기록
            string asciiArt = @"
..................~.~....+:.:.........:.....................
.................~...,:....,......=.....,..,................
..............:....,......................:..?:.............
............,...............................................
.........................:=+????+=:.........................
..................,IIIIIIIIIIIIIII???????...................
...............,IIIIIIIIIIIIIIIIIIIIII?????I...~............
...........=..IIIIIIIIIIIIIIIIIIIIIIIIII?????,..............
.............IIIIIIIIIII,:IIIIIII==IIIIII?????..............
.............IIIIIIIIIIIII,IIIII.IIIIIIIII????,.............
..........:.IIIIIIIIIIII,,IIIIIII++IIIIIIII????.............
............IIIIIIIIIII:7~.IIIII.?7.IIIIIIII???.:...........
...........IIIIIIIIIIIII7..IIIII..77IIIIIIIII???............
...........IIIIIIIIIIIII~:+IIIIII.?IIIIIIIIIII??............
..........IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII??............
..........IIIIIIIIIIIIIIIIIII=...~IIIIIIIIIIIIIII+.?:+......
.....III,IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII~IIIII??.I???I.....
...,IIII=IIIIIII.~IIIIIIIIIIIIIIIIIIIIIII.IIIIII?~???II?....
...I=,=I?IIIIIII+?777=.:?IIIIIIIIII=,.:7+.=IIIII????~I?I,...
...+II:IIIIIIIIII.=:7 77777777 77777777.~IIIIIII???,???~....
....IIII?IIIIIIII:+?+??~..:+?I77I+,.~++?.IIIIIII????I??.....
.....???.IIIIIIIII~++++++++++++++++++++=IIIIIIII??:?I.......
.......:.IIIIIIIII.++++++++++++++++++++.IIIIIII??:..........
.......~,,,IIIIIIII,++++++++++++++++++,IIIIIIII?I...........
.....,.IIII:.IIIIII?+++++++++++++++++??IIIIIII??............
....,+IIIIII~:IIIIII?++++.~?????=.++?=IIIIIII??.............
....~,IIIIIII,IIIIIIII.??????????++=?IIIIIII?.,.............
.......IIIIII,..IIIIIII?,???????++.IIIIIII?+................
......=,IIII=,+II:,IIIIIII:.,,..=IIIIIII?~..................
.......,IIIIIIIIIII,.~IIIIIIIIIIIIIII?,:....................
.......,IIIIIIIIII?,:::=..~?IIII?::=~==~.=..................
......,IIIIIIII,IIIII.=:~===~~==~=,+====~:.~................
.....,,IIIIIIIIIIIIIII,,============.,==~~:,=...............
.....,IIIIIIIIIIIIIIII.,==I=======II?.====::~...............
.....,IIIIIIIIIIIII:..,,=??I+==IIIII?=,=+~~.................
.....~~IIIIIIIIII,IIIII?:~=++++===II???,+,,:................
......,IIIIIIIIIIIIIIIII,,,I??I,:,,,,~?+II~=................
.......,IIIIIIIIIIIIIII,~:,,..,,=~~,,,::,I?,................
........,?IIIIIIIIII~,+==~~~=====~~:==~~,:,~................
.........~,,IIIII:.:+====~~~+=====~~,~=~=.,=................
.................I=======~~~~~~~~=~~=~=~~:~.................
...............:,==~~~~~:::::::~~===+=~???,.................
...............,:=~~~~~,~::~:::~==,+=+++=,,.................
...............,==~~~~~~~::,,.,:~=======~~,.................
...............,,,:,,,:~~=~:..,~~+==I+~,,:,=................
.............:,,,,,:::::::,,:..,,:::::::::,,,,=.............
.........=,,:,,:,::::::::,,...,,,::::::::.:,,::,,:..........
........,,,:::::::::::::,,,,..:.,,,:::::::::::::,,:.........";

            Console.WriteLine(asciiArt);
            Console.WriteLine("아무키나 입력하여 게임을 시작하세요.");
            int index = 0;
            Console.ReadKey(); // 사용자가 아무 키나 누를 때까지 대기
            Console.Clear(); // 화면 지우기
            while (true)
            {
                ArmyClass();
                Discharge();
                UpdateMaxValues(classM, hobong);
                string[] choices = { "심사하기", t, "게임종료" };

                Console.WriteLine($"현재 계급 : {classA} / {hobong}호봉");
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
                Console.WriteLine("방향키로 이동하고 엔터로 선택하세요.");
                DeadClass = classA;//끝났을때 계급 데이터(또는 강제로 껐을때도)
                DeadHobong = hobong;//끝났을때 계급 데이터(또는 강제로 껐을때도)

                for (int i = 0; i < choices.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;//선택된 텍스트 색
                    }
                    Console.WriteLine(choices[i]);
                    Console.ResetColor();
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        index = (index - 1 + choices.Length) % choices.Length;
                        Console.Clear(); // 화면 지우기
                        break;
                    case ConsoleKey.DownArrow:
                        index = (index + 1) % choices.Length;
                        Console.Clear(); // 화면 지우기
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear(); // 화면 지우기
                        if (index == 1&& isD)
                        {
                            isa = true;
                        }
                        ExecuteOption(index);
                        break;
                    default:
                        Console.Clear(); // 화면 지우기
                        break ;
                }
            }
        }
        static void ExecuteOption(int optionIndex)
        {
            switch (optionIndex)
            {
                case 0: // "아이템 강화" 선택
                    ReinforceItem();
                    isD = false;
                    break;
                case 1:
                    if (isDischarge)
                    {
                        int SUM = (gold / 2) + discharge; 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"전역하시겠습니까 \n지금 전역하시면 : {SUM.ToString("n0")}G 만큼 받을 수 있습니다.\n");
                        Console.ResetColor();
                        isD = true;
                        if (isa)//전역하기
                        {
                            gold /= 2;
                            gold += discharge;
                            classM = 0;
                            hobong = 1;
                            isD = false;
                            isDischarge = false;
                            isa = false;
                            homedischarges++;//전역횟수 데이터
                        }
                    }
                    else
                    {
                        if (isDead)//다시하기
                        {
                            isD = false;
                            isDischarge = false;
                            isDead = false;
                            gold = 100;
                            classM = 0;
                            hobong = 1;
                            re++;//다시하기 횟수 데이터
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("아직 전역할수 없습니다 ^^\n(전역은 하사 이상 계급에서 1호봉일때만 가능합니다)");
                            Console.ResetColor();
                        }
                    }
                     break;
                case 2: // "게임 종료" 선택
                    Console.WriteLine("게임을 종료합니다.");
                    Console.ReadKey(); //잠시 대기
                    Environment.Exit(0); //종료
                    break;
                default:
                    Console.WriteLine("\n올바른 옵션을 선택하세요.\n");
                    isD = false;
                    Console.ReadKey(); // 사용자의 키 입력 대기
                    break;
            }
        }
        static void Discharge()//전역 선택
        {
            if ((classM >= 4 && classM <= 15) && hobong == 1)
            {
                isDischarge= true;
                isDead = false;
                if (isD)
                {
                    t = "다시 누르면 전역입니다. 신중하게 선택하세요";
                }
                else
                {
                    t = "전역하시겠습니까?";
                }
            }
            else if (isDead)
            {
                isDischarge = false;
                t = "다시하기";
            }
            else 
            {
                isDischarge = false;
                t = "---";
            }
        }
        static void ReinforceItem()//강화시
        {
            Console.Clear(); // 화면 지우기
            //DrawBorder();
            Console.Write("시간이 흐르고 있습니다");

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
                    Console.WriteLine("\n진급을 실패했습니다...\n");
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
                        bool demote = random.Next(100) < demotion;
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (demote)
                        {
                            demotions++;//강등 데이터
                            if((classM >= 4 && classM <= 15) && hobong == 1)
                            {
                                Console.WriteLine("\n계급이 강등되었습니다...\n");
                                classM -= 1;
                                if (classM == 13 || classM == 14 || classM == 15)
                                {
                                    hobong = 1;
                                }
                                else 
                                { 
                                    hobong = 2;
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
                    if ((classM >= 0 && classM <= 12) && hobong == 2 || (classM >= 13 && classM <= 16) && hobong == 1)
                    {
                        classM += 1;
                        hobong = 1;
                        gold += promotion + salary;
                        Console.WriteLine("\n진급을 성공했습니다!\n");
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
                isDead = true;
                fool++;//소지금 부족한 상태에서 연타한 데이터
            }
            Console.ResetColor(); // 콘솔 텍스트 색상을 초기화
        }
        static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {
           SaveGameDataToCSV(sender);
            Console.WriteLine("게임 데이터가 CSV 파일에 저장되었습니다.");
        }
        static void SaveGameDataToCSV(object? sender)
        {
            DateTime endTime = DateTime.Now;//종료시간 저장
            TimeSpan playTime = endTime - startTime; //플레이 시간 계산
            int playTimeM = (int)playTime.TotalSeconds; // 초 단위

            bool appendHeader = !File.Exists(@"game_data.csv");
            // CSV 파일 생성 및 헤더 작성
            using (StreamWriter writer = new StreamWriter(@"game_data.csv", true, System.Text.Encoding.GetEncoding("utf-8")))
            {
                if (appendHeader)
                {
                    writer.Write("성공 횟수,");
                    writer.Write("실패 횟수,");
                    writer.Write("불명예 횟수,");
                    writer.Write("강등 횟수,");
                    writer.Write("진급 누락 횟수,");
                    writer.Write("끝났을 때 계급,");
                    writer.Write("끝났을 때 호봉,");
                    writer.Write("전역 횟수,");
                    writer.Write("실제 시작 시간,");
                    writer.Write("실제 종료 시간,");
                    writer.Write("플레이 시간(초),");
                    writer.Write("소지금이 부족한 상태에서 연타한 횟수,");
                    writer.Write("다시한 횟수,");
                    writer.Write("최고 계급,");
                    writer.Write("최고 호봉,");

                    writer.WriteLine(); // 헤더 줄 바꿈
                }

                // 데이터를 가로 방향으로 추가
                writer.Write(successfulReinforcements);
                writer.Write(",");

                writer.Write(failedReinforcements);
                writer.Write(",");

                writer.Write(dishonorableDischarges);
                writer.Write(",");

                writer.Write(demotions);
                writer.Write(",");

                writer.Write(promotionMisses);
                writer.Write(",");

                writer.Write(DeadClass);
                writer.Write(",");

                writer.Write(DeadHobong);
                writer.Write(",");

                
                writer.Write(homedischarges);
                writer.Write(",");

                writer.Write(startTime);
                writer.Write(",");

                writer.Write(endTime);
                writer.Write(",");

                writer.Write(playTimeM);
                writer.Write(",");

                writer.Write(fool);
                writer.Write(",");

                writer.Write(re);
                writer.Write(",");

                writer.Write(maxClassK);
                writer.Write(",");

                writer.Write(maxHobong);
                writer.WriteLine(); // 데이터 줄 바꿈
            }
        }
        static void UpdateMaxValues(int currentClass, int currentHobong)
        {
            // 현재 계급이 최고 계급보다 높을 경우에만 업데이트
            if (currentClass > maxClass)
            {
                maxClassK = classA;
                maxHobong = currentHobong;
            }
            else if (currentClass == maxClass && currentHobong >= maxHobong)
            {
                maxHobong = currentHobong;
            }
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
                    salary = 100;//월급
                    promotion = 0;//전급비
                    Reinforcement = 0;//강화비용
                    
                }
                else if (hobong == 2)
                {
                    salary = 122;//월급
                    Reinforcement = 100;//강화비용
                    promotion = 200;//진급비
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
                    salary = 149;//월급
                    promotion = 0;//진급비
                    Reinforcement = 115;//강화비용
                    application = 96.4f;//성공
                    deducted = 3.7f;//실패
                    
                }
                else if (hobong == 2)
                {
                    salary = 182;//월급
                    promotion = 334;//진급비
                    Reinforcement = 138;//강화비용
                    application = 92.7f;//성공
                    deducted = 7.3f;//실패
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
                    salary = 223;//월급
                    promotion = 0;//진급비
                    Reinforcement = 187;//강화비용
                    application = 89.1f;//성공
                    deducted = 11;//실패
                }
                else if (hobong == 2)
                {
                    salary = 272;//월급
                    promotion = 499;//진급비
                    Reinforcement = 225;//강화비용
                    application = 85.4f;//성공
                    deducted = 14.6f;//실패
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
                    salary = 333;//월급
                    promotion = 0;//진급비
                    Reinforcement = 270;//강화비용
                    application = 81.8f;//성공
                    deducted = 18.3f;//실패
                }
                else if (hobong == 2)
                {
                    salary = 407;//월급
                    promotion = 746;//전급비
                    Reinforcement = 323;//강화비용
                    application = 78.1f;//성공
                    deducted = 21.9f;//실패
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
                    salary = 497;//월급
                    promotion = 0;//진급비
                    discharge = 2148;//전역비
                    Reinforcement = 388;//강화비용
                    application = 74.5f;//성공
                    deducted = 24.6f;//실패
                    demotion = 1;//강등
                }
                else if (hobong == 2)
                {
                    salary = 608;//월급
                    promotion = 1114;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 466;//강화비용
                    application = 70.8f;//성공
                    deducted = 27.1f;//실패
                    demotion = 2.2f;//강등
                }
                
                Destruction = 0;//파괴
            }
            else if (classM == 5)//중사
            {
                classA = "vv 중사";
                if (hobong == 1)
                {
                    salary = 743;//월급
                    promotion = 0;//진급비
                    discharge = 3208;//전역비
                    Reinforcement = 559;//강화비용
                    application = 67.2f;//성공
                    deducted = 29.6f;//실패
                    demotion = 3.3f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 907;//월급
                    promotion = 1663;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 671;//강화비용
                    application = 63.5f;//성공
                    deducted = 32.1f;//실패
                    demotion = 4.5f;//강등
                }
                
                Destruction = 0;//파괴
            }
            else if (classM == 6)//상사
            {
                classA = "vvv 상사";
                if (hobong == 1)
                {
                    salary = 1109;//월급
                    promotion = 0;//전급비
                    discharge = 4790;//전역비
                    Reinforcement = 805;//강화비용
                    application = 59.9f;//성공
                    deducted = 34.6f;//실패
                    demotion = 5.6f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 1355;//월급
                    promotion = 2484;//전급비
                    discharge = 0;//전역비
                    Reinforcement = 966;//강화비용
                    application = 56.2f;//성공
                    deducted = 37.1f;//실패
                    demotion = 6.8f;//강등
                }
                
                Destruction = 0;//파괴
            }
            else if (classM == 7) //소위
            {
                classA = "♦ 소위";
                if (hobong == 1)
                {
                    salary = 1656;//월급
                    promotion = 0;//진급비
                    discharge = 7153;//전역비
                    Reinforcement = 1159;//강화비용
                    application = 52.6f;//성공
                    deducted = 39.6f;//실패
                    demotion = 7.9f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 2023;//월급
                    promotion = 3709;//진급비
                    deducted = 42.1f;//실패
                    application = 48.9f;//성공
                    discharge = 0;//전역비
                    Reinforcement = 1391;//강화비용
                    demotion = 9.1f;//강등
                }
                
                Destruction = 0;//파괴
            }
            else if (classM == 8) // 중위
            {
                classA = "♦♦ 중위";
                if (hobong == 1)
                {
                    salary = 2473;//월급
                    promotion = 0;//진급비
                    discharge = 7153;//전역비
                    Reinforcement = 1669;//강화비용
                    application = 45.3f;//성공
                    deducted = 44.6f;//실패
                    demotion = 10.2f;//강등
                }
                else if (hobong == 2)
                {
                    salary = 3021;//월급
                    promotion = 5538;//진급비
                    discharge = 0;//전역비
                    Reinforcement = 2003;//강화비용
                    application = 41.6f;//성공
                    deducted = 47.1f;//실패
                    demotion = 11.4f;//강등
                }
                
                Destruction = 0;//파괴
            }
            else if (classM == 9) //대위
            {
                classA = "♦♦♦ 대위";
                if (hobong == 1)
                {
                    salary = 3692;//월급
                    promotion = 0;//진급비
                    discharge = 15950;//전역비
                    Reinforcement = 2403;//강화비용
                    application = 38;//성공
                    deducted = 49.6f;//실패
                    demotion = 12.5f;//강등
                    Destruction = 0;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 4512;//월급
                    promotion = 8270;//진급비
                    discharge = 0;//전역비
                    Reinforcement = 2884;//강화비용
                    application = 34.3f;//성공
                    deducted = 13.7f;//실패
                    demotion = 14.8f;//강등
                    Destruction = 0;//파괴
                }
                
            }
            else if (classM == 10) //소령
            {
                classA = "✷ 소령";
                if (hobong == 1)
                {
                    salary = 5513;//월급
                    promotion = 0;//진급비
                    discharge = 23818;//전역비
                    Reinforcement = 3461;//강화비용
                    application = 30.7f;//성공
                    deducted = 53.1f;//실패
                    demotion = 14.8f;//강등
                    Destruction = 1.5f;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 6737;//월급
                    promotion = 12350;//진급비
                    discharge = 0;//전역비
                    Reinforcement = 4153;//강화비용
                    application = 27;//성공
                    deducted = 54.1f;//실패
                    demotion = 16f;//강등
                    Destruction = 3f;//파괴
                }
                
            }
            else if (classM == 11) //중령
            {
                classA = "✷✷ 중령";
                if (hobong == 1)
                {
                    salary = 8233;//월급
                    promotion = 0;//진급비
                    discharge = 35567;//전역비
                    Reinforcement = 4984;//강화비용
                    application = 23.4f;//성공
                    deducted = 55.1f;//실패
                    demotion = 17.1f;//강등
                    Destruction = 4.5f;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 10061;//월급
                    promotion = 18442;//진급비
                    discharge = 0;//전역비
                    Reinforcement = 5981;//강화비용
                    application = 19.7f;//성공
                    deducted = 56.1f;//실패
                    demotion = 18.3f;//강등
                    Destruction = 6;//파괴
                }
                
            }
            else if (classM == 12) // 대령
            {
                classA = "✷✷✷ 대령";
                if (hobong == 1)
                {
                    salary = 12294;//월급
                    promotion = 0;//진급비
                    discharge = 53112;//전역비
                    Reinforcement = 7177;//강화비용
                    application = 16.1f;//성공
                    deducted = 57.1f;//실패
                    demotion = 19.4f;//강등
                    Destruction = 7.5f;//파괴
                }
                else if (hobong == 2)
                {
                    salary = 15024;//월급
                    promotion = 27539;//진급비
                    discharge = 0;//전역비
                    Reinforcement = 8612;//강화비용
                    application = 12.4f;//성공
                    deducted = 58.1f;//실패
                    demotion = 20.6f;//강등
                    Destruction = 9f;//파괴
                }
                
            }
            else if (classM == 13) // 준장
            {
                classA = "☆ 준장 ☆";
                if (hobong == 1)
                {
                    salary = 18359;//월급
                    promotion = 33652;//진급비
                    discharge = 79311;//전역비
                    Reinforcement = 10335;//강화비용
                    application = 8.8f;//성공
                    deducted = 59.1f;//실패
                    demotion = 21.7f;//강등
                    Destruction = 10.5f;//파괴
                }
            }
            else if (classM == 14) //소장
            {
                classA = "☆☆ 소장 ☆☆";
                if (hobong == 1)
                {
                    salary = 22435;//월급
                    promotion = 41123;//진급비
                    discharge = 96918;//전역비
                    Reinforcement = 12402;//강화비용
                    application = 5.1f;//성공
                    deducted = 60f;//실패
                    demotion = 22.9f;//강등
                    Destruction = 12;//파괴
                }
            }
            else if (classM == 15) //중장
            {
                classA = "☆☆☆ 중장 ☆☆☆";
                if (hobong == 1)
                {
                    salary = 27415;//월급
                    promotion = 50252;//진급비
                    discharge = 118434;//전역비
                    Reinforcement = 14882;//강화비용
                    application = 0.1f;//성공
                    deducted = 62.4f;//실패
                    demotion = 24f;//강등
                    Destruction = 13.5f;//파괴
                }
            }
            else if (classM == 16) //대장
            {
                classA = "☆☆☆☆ 대장 ☆☆☆☆";
                if (hobong == 1)//호봉
                {
                    salary = 33502;//월급
                    promotion = 0;//진급비
                    discharge = 144727;//전역비
                    Reinforcement = 1000;//강화비용
                    application = 0.0077f;//성공
                    deducted = 99.9922f;//실패
                    demotion = 0f;//강등
                    Destruction = 0.0001f;//파괴
                }
            }
            else if (classM == 17) //대통령
            {
                classA = "☆☆☆☆☆ 한국 초대 대통령 ☆☆☆☆☆";
                if (hobong == 1)//호봉
                {
                    Console.WriteLine("이은석");
                }
            }

        }
    }
}
        
