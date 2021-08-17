using System;

namespace SajuApp
{
    class Program
    {
        public static int birthSex = 0;
        public static int birthYear = 0;
        public static int birthMonth = 0;
        public static int birthDay = 0;
        public static int birthHour = 0;
        public static int birthMin = 0;
        public static int originYear = 0;
        public static int originMonth = 0;
        public static int originDay = 0;
        public static int originTime = 0;

        //성별 열거형
        public enum Sex
        {
            乾 = 1,
            坤 = 2
        }

        //음양 열거형
        public enum YinYang
        {
            陰,
            陽
        }

        //오행
        public enum Five
        {
            木,
            火,
            土,
            金,
            水
        }


        //천간 열거형
        public enum Stem
        {
            甲 = 1,
            乙 = 2,
            丙 = 3,
            丁 = 4,
            戊 = 5,
            己 = 6,
            庚 = 7,
            辛 = 8,
            壬 = 9,
            癸 = 10
        }

        // 지지 열거형
        public enum Branch
        {
            子 = 1,
            丑 = 2,
            寅 = 3,
            卯 = 4,
            辰 = 5,
            巳 = 6,
            午 = 7,
            未 = 8,
            申 = 9,
            酉 = 10,
            戌 = 11,
            亥 = 12
        }

        // 십성 열거형
        /*
         * 공통 = 1) 비견, 3) 식신, 5) 편재 7) 칠살 9) 효신
         * 음천간 = 2) 상관[4] 4) 정재[6] 6) 정관[8] 8) 정인[10] 10) 겁재[2]
         * 양천간 = 2) 겁재 4) 상관 6) 정재 8) 정관 10) 정인
         * 
         */
        public enum Stars
        {
            我神 = 0,
            比肩 = 1,
            劫財 = 2,
            食神 = 3,
            傷官 = 4,
            偏財 = 5,
            正財 = 6,
            七殺 = 7,
            正官 = 8,
            梟神 = 9,
            正印 = 10
        }

        public static int AskSex()
        {
            while (true)
            {
                Console.WriteLine("Choose your sex: (1) Male, (2) Female");
                int x = Convert.ToInt32(Console.ReadLine());

                if (x == 1 || x == 2)
                {
                    return x;

                }
                Console.WriteLine("**Not Appropriate. Try Again: ");
            }
            
        }

        public static int AskYear()
        {
            while (true)
            {
                Console.WriteLine("Born Year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                if (year >= 0)
                {
                    return year;
                }
                Console.WriteLine("Not Appropriate. Try Again: ");
            }
            
        }

        public static int AskMonth()
        {
            while (true)
            {
                Console.WriteLine("Born Month: ");
                int month = Convert.ToInt32(Console.ReadLine());
                if (month < 13 && month > 0)
                {
                    return month;
                }
                Console.WriteLine("Not Appropriate. Try Again: ");
            }
        }

        public static int AskDay(int month, int year)
        {
            while (true)
            {
                Console.WriteLine("Born day: ");
                int day = Convert.ToInt32(Console.ReadLine());
                if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                {
                    if (day > 0 && day < 32)
                    {
                        return day;
                    }
                    Console.WriteLine("Not Appropriate. Try Again: ");
                }

                if (month == 4 || month == 6 || month == 9 || month == 11)
                {
                    if (day > 0 && day < 31)
                    {
                        return day;
                    }
                    Console.WriteLine("Not Appropriate. Try Again: ");
                }

                if (month == 2)
                {
                    if (year % 4 == 0)
                    {
                        if (day > 0 && day < 30)
                        {
                            return day;
                        }
                        Console.WriteLine("Not Appropriate. Try Again: ");
                    }
                    else
                    {
                        if (day > 0 && day < 29)
                        {
                            return day;
                        }
                        Console.WriteLine("Not Appropriate. Try Again: ");
                    }
                }
            }
        }

        public static int AskHour()
        {
            while (true)
            {
                Console.WriteLine("Born time (hour, 0~23): ");
                int hour = Convert.ToInt32(Console.ReadLine());
                if (hour >= 0 && hour < 24)
                {
                    return hour;
                }
                Console.WriteLine("Not Appropriate. Try Again: ");
            }
        }

        public static int AskTime()
        {
            while (true)
            {
                Console.WriteLine("Born time (minutes): ");
                int min = Convert.ToInt32(Console.ReadLine());
                if (min >= 0 && min < 60)
                {
                    return min;
                }
                Console.WriteLine("Not Appropriate. Try Again: ");
            }
        }
        
        public static int CalculateYear(int bornYear, int bornMonth, int bornDay, int startDay)
        {
            int x = bornYear - 3;
            x %= 60;

            if(bornMonth == 1)
            {
                x -= 1;
            }
            else if(bornMonth == 2 && bornDay < startDay)
            {
                x -= 1;
            }

            return x;
        }

        public static int CalculateMonth(int bornYear, int bornMonth, int bornDay, int startDay)
        {
            int result = 0;
            int dragonMonth = 5;
            int cycleYear = (bornYear - 3) % 60;
            int branchMonth = 0;
            
            dragonMonth += (12 * (cycleYear - 1));
            dragonMonth %= 60;

            if(bornDay >= startDay)
            {
                branchMonth = bornMonth + 1;
            }
            else
            {
                branchMonth = bornMonth;
            }

            result = dragonMonth + (branchMonth - 5);

            return result;
        }

        public static int CalculateDay(int bornYear, int bornMonth, int bornDay, int bornHour, int bornMin, int deltaMonth)
        {
            int fixedYear = bornYear % 80;
            int fDay = ((fixedYear / 4) * 21) + ((fixedYear % 4) * 5 + 1);
            if (bornMonth % 2 == 0) // 짝수월생 +30일 추가
            {
                deltaMonth += 30;
            }
            //+++++++++++일주 숫자++++++++++//
            int result = (fDay + bornDay + deltaMonth + 54) % 60; //생일주 60갑자
            if(bornHour ==23 && bornMin >= 33)
            {
                result += 1;
            }
            return result;
        }

        public static int CalculateTime(int cycleDay, int bornHour, int bornMin)
        {
            int result = 0;
            int dragon = 5;
            int branch = 0;

            dragon += (12 * (cycleDay - 1));
            

            if (bornHour % 2 != 0)
            {
                bornHour += 1;
                if(bornMin < 33)
                {
                    branch = bornHour / 2;
                }
                else
                {
                    branch = (bornHour / 2) + 1;
                }
            }
            else
            {
                branch = (bornHour / 2) + 1;
            }

            if (branch > 12)
            {
                branch = 1;
            }
            result = dragon + (branch - 5);

            result %= 60;
            return result;
        }

        public static int Num4Branch(int cycleNum)
        {
            int branchNum = (cycleNum % 12);
            if (branchNum == 0)
            {
                branchNum = 12;
            }
            
            int result = branchNum;
            return result;
        }

        public static int Num4Stem(int cycleNum)
        {
            int stemNum = cycleNum - (cycleNum / 10) * 10;
            if (stemNum == 0)
            {
                stemNum = 10;
            }
            return stemNum;
        }

        public static int Branch2Stem(int branchNum)
        {
            int result = 0;
            switch (branchNum) 
            {
                case 1:
                    result = 10;
                    break;
                case 2:
                    result = 6;
                    break;
                case 3:
                    result = 1;
                    break;
                case 4:
                    result = 2;
                    break;
                case 5:
                    result = 5;
                    break;
                case 6:
                    result = 3;
                    break;
                case 7:
                    result = 4;
                    break;
                case 8:
                    result = 6;
                    break;
                case 9:
                    result = 7;
                    break;
                case 10:
                    result = 8;
                    break;
                case 11:
                    result = 5;
                    break;
                case 12:
                    result = 9;
                    break;
            }
            return result;
        }

        public static int Stem2Five(int everyStem)
        {
            int result = 0;
            if (everyStem % 2 == 0)
            {
                everyStem -= 1;
            }
            switch (everyStem)
            {
                case 1: // wood
                    result = 0;
                    break;
                case 3: // fire
                    result = 1;
                    break;
                case 5: // earth
                    result = 2;
                    break;
                case 7: // metal
                    result = 3;
                    break;
                case 9: // water
                    result = 4;
                    break;
            }

            return result;
        }

        public static int Five4Color(int five)
        {
            int result = 0;
            switch (five) 
            {
                case 0: //wood, blue
                    result = 10;
                    break;
                case 1: //fire, red
                    result = 12;
                    break;
                case 2: //earth, yellow
                    result = 14;
                    break;
                case 3: //metal, white
                    result = 15;
                    break;
                case 4: //water, blue (instead of black)
                    result = 3;
                    break;
            }
            return result;
        }


        public static bool Quit()
        {
            while (true)
            {
                Console.WriteLine("Do you want to quit?: (1) Yes (2) No");
                int x = Convert.ToInt32(Console.ReadLine());
                bool quitThis = false;
                if (x == 1)
                {
                    quitThis = true;
                    return quitThis;
                }
                else if (x == 2)
                {
                    quitThis = false;
                    return quitThis;
                }
                else
                {
                    Console.WriteLine("Not Appropriate. Try Again: ");
                }
            }
        }

        static void Main(string[] args)
        {
            // 변수값
            bool quitProgram = false;

            //절기별 시작 일자 리스트
            System.Collections.Generic.List<int> start = new System.Collections.Generic.List<int>(new int[] { 5, 4, 6, 6, 5, 6, 7, 7, 8, 8, 7, 7 });
            //월차 조정일 리스트
            System.Collections.Generic.List<int> controlMonth = new System.Collections.Generic.List<int>(new int [] {0, 1, -1, 0, 0, 1, 1, 2, 3, 3, 4, 4});

            while (true)
            {
                //사주 60갑자 리스트
                System.Collections.Generic.List<int> sajuNum = new System.Collections.Generic.List<int>();
                //천간 십성 리스트
                System.Collections.Generic.List<int> stemStarNum = new System.Collections.Generic.List<int>();
                //지지 십성 리스트
                System.Collections.Generic.List<int> branchStarNum = new System.Collections.Generic.List<int>();
                //천간 오행 리스트
                System.Collections.Generic.List<int> stemFive = new System.Collections.Generic.List<int>();
                //지지 오행 리스트
                System.Collections.Generic.List<int> branchFive = new System.Collections.Generic.List<int>();

                if (quitProgram == true)
                {
                    Environment.Exit(0);
                    break;
                }

                // 프로그램 시작
                birthSex = AskSex();
                Console.WriteLine("============================================");

                // 생년?
                birthYear = AskYear();
                Console.WriteLine("============================================");

                // 생월?
                birthMonth = AskMonth();
                Console.WriteLine("============================================");

                // 생일?
                birthDay = AskDay(birthMonth, birthYear);
                Console.WriteLine("============================================");

                // 생시 (시간)?
                birthHour = AskHour();
                Console.WriteLine("============================================");

                // 생시 (분)?
                birthMin = AskTime();
                Console.WriteLine("============================================");


                // 계산
                originYear = CalculateYear(birthYear, birthMonth, birthDay, start[birthMonth-1]);

                originMonth = CalculateMonth(birthYear, birthMonth, birthDay, start[birthMonth - 1]);

                originDay = CalculateDay(birthYear, birthMonth, birthDay, birthHour, birthMin, controlMonth[birthMonth-1]);

                originTime = CalculateTime(originDay, birthHour, birthMin);

                sajuNum.Add(originTime);
                sajuNum.Add(originDay);
                sajuNum.Add(originMonth);
                sajuNum.Add(originYear);
                
                Console.WriteLine();

                // 생년월일시 출력
                Console.WriteLine((Sex)Convert.ToInt32(birthSex) + "命 " + birthYear + "년 양력 " + birthMonth + "월 " + birthDay + "일 " + birthHour + "시 " + birthMin + "분 " + "\n");
                Console.WriteLine("=================<사주팔자>=================\n");
                Console.WriteLine("시 " + "일 " + "월 " + "년 " + "\n");

                //2021-08-16
                int body = 0;
                int use = 0;

                //사주팔자 천간 부분 출력
                for (int i = 0; i < 4; i++)
                {
                    stemFive.Add(Stem2Five(Num4Stem(sajuNum[i])));
                    Console.ForegroundColor = (ConsoleColor)Five4Color(stemFive[i]);
                    Console.Write((Stem)Convert.ToInt32(Num4Stem(sajuNum[i])) + " ");
                    body = Num4Stem(sajuNum[1]);
                }
                Console.ForegroundColor = (ConsoleColor)15;
                Console.WriteLine();

                //사주팔자 지지 부분 출력
                for (int i = 0; i < 4; i++)
                {
                    branchFive.Add(Stem2Five(Branch2Stem(Num4Branch(sajuNum[i]))));
                    Console.ForegroundColor = (ConsoleColor)Five4Color(branchFive[i]);
                    Console.Write((Branch)Convert.ToInt32(Num4Branch(sajuNum[i])) + " ");
                    use = Num4Branch(sajuNum[2]);
                }
                Console.ForegroundColor = (ConsoleColor)15;
                Console.WriteLine("\n");
                
                //천간 순서 숫자 리스트
                stemStarNum.Add(Num4Stem(sajuNum[0]));
                stemStarNum.Add(Num4Stem(sajuNum[1]));
                stemStarNum.Add(Num4Stem(sajuNum[2]));
                stemStarNum.Add(Num4Stem(sajuNum[3]));

                //지장간 정기 (천간) 순서 숫자 리스트
                branchStarNum.Add(Branch2Stem(Num4Branch(sajuNum[0])));
                branchStarNum.Add(Branch2Stem(Num4Branch(sajuNum[1])));
                branchStarNum.Add(Branch2Stem(Num4Branch(sajuNum[2])));
                branchStarNum.Add(Branch2Stem(Num4Branch(sajuNum[3])));


                Console.WriteLine("=================<십성판단>=================\n");
                foreach (int x in stemStarNum)
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine();
                foreach (int x in branchStarNum)
                {
                    Console.Write(x + " ");
                }
                Console.WriteLine("\n");

                Console.WriteLine("시 " + "일 " + "월 " + "년 " + "\n");

                foreach (int x in stemStarNum)
                {
                    Console.Write((Stem)Convert.ToInt32(Num4Stem(x)) + " ");
                }
                Console.WriteLine();
                foreach (int x in branchStarNum)
                {
                    Console.Write((Stem)Convert.ToInt32(Num4Stem(x)) + " ");
                }
                
                Console.WriteLine("\n");
                Console.WriteLine("============================================\n");

                // 천간 부분 십성 출력
                for (int x=0; x<4; x++)
                {
                    int eachStar = 0;
                    
                    // 일간은 0으로 처리 (我), 나머지는 1을 더한 후 아신과 차이값을 저장
                    if (x != 1)
                    {
                        eachStar= (stemStarNum[x] - stemStarNum[1]) + 1;

                        if (stemStarNum[1]%2 == 0 && eachStar%2==0)
                        {
                            //음간의 경우 짝수 번째 십성은 양간 십성 순서에 +2만큼 증가
                            eachStar += 2;
                        } //0이하 값이 나오면 다시 10을 더해 줌
                        if (eachStar <= 0) eachStar += 10;
                    }
                    // 오행 색상으로 배치 
                    Console.ForegroundColor = (ConsoleColor)Five4Color(stemFive[x]);
                    // 십성 순서 숫자로부터 십성 열거형 출력
                    Console.Write((Stars)Convert.ToInt32(eachStar) + " ");
                    Console.Write(eachStar + " ");
                }
                Console.ForegroundColor = (ConsoleColor)15;
                Console.WriteLine();

                //2021-08-16
                int soul = 0;

                // 지지 부분 십성 출력
                for(int x=0; x<4; x++)
                {
                    int eachStar = 0;
                    
                    //1을 더한 후 아신과 차이값을 저장
                    eachStar = (branchStarNum[x] - stemStarNum[1]) + 1;
                    if (stemStarNum[1] % 2 == 0 && eachStar % 2 == 0)
                    {
                        //음간의 경우 짝수 번째 십성은 양간 십성 순서에 +2만큼 증가
                        eachStar += 2;
                    } //0이하 값이 나오면 다시 10을 더해 줌
                    if (eachStar <= 0) eachStar += 10;
                    if (x == 2) soul = eachStar;
                    // 오행 색상으로 배치 
                    Console.ForegroundColor = (ConsoleColor)Five4Color(branchFive[x]);
                    // 십성 순서 숫자로부터 십성 열거형 출력
                    Console.Write((Stars)Convert.ToInt32(eachStar) + " ");
                    Console.Write(eachStar + " ");
                }
                Console.ForegroundColor = (ConsoleColor)15;
                Console.WriteLine("\n");
                // 2021-08-16
                Console.WriteLine("============================================\n");
                Console.Write("격: ");
                Console.Write((Stars)Convert.ToInt32(soul) + " 格\n");
                Console.Write((Branch)Convert.ToInt32(use) + "월의 " + (Stem)Convert.ToInt32(body) + (Five)Convert.ToInt32(stemFive[1]) + "\n");
                
                Console.WriteLine("============================================\n");
                
                //프로그램 종료
                quitProgram = Quit();
                
            }

            
        }
    }
}