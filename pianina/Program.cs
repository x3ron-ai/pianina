namespace pianina
{
    internal class Program
    {
        public static long lastTimePressed;
        public static ConsoleKey last_pressed = ConsoleKey.Enter;
        public static bool closeKeyLong = false;
        public static int touchings = 0;
        static void GetKeyLongPressing()
        {
            while (true)
            {
                if (closeKeyLong)
                {
                    closeKeyLong = false;
                    break;
                }
                Thread.Sleep(100);
                if (((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() - lastTimePressed >= 500)
                {
                    Console.Beep(37, 1);
                    last_pressed = ConsoleKey.Enter;
                    break;
                }
               
            }
        }
        static void EndlessPress(int freq)
        {
            Console.Beep(freq, 123123123);
        }    
        static void Main(string[] args)
        {
            int[][] octavesArray = new int[7][];

            octavesArray[0] = new int[] { 65,69,73,78,82,87,92,98,104,110,116,123};
            octavesArray[1] = new int[] { 130,138,147,155,165,175,185,196,208,220,233,247};
            octavesArray[2] = new int[] { 262,277,294,311,330,349,370,392,415,440,466,494};
            octavesArray[3] = new int[] { 523,554,587,622,659,698,740,784,831,880,932,988};
            octavesArray[4] = new int[] { 1047,1109,1175,1245,1319,1397,1480,1568,1661,1760,1865,1976};
            octavesArray[5] = new int[] { 2093,2217,2349,2489,2637,2794,2960,3136,3322,3520,3729,3951};
            octavesArray[6] = new int[] { 4186,4435,4699,4978,5274,5588,5920,6272,6645,7040,7459,7902};
            Thread lp;
            Console.WriteLine("Try to play Bury The Light on it!\n" +
                "Управление:\nA-C\nW-C#\nS-D\nE-Eb\nD-E\nF-F\nT-F#\nG-G\nY-G#\nH-A\nU-Bb\nJ-B");
            int octave = 1;
            int freq = 0;
            int kb;
            while (true)
            {
                kb = -1;
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Try to play Bury The Light on it!\n"+
                    "Управление:\nA-C\nW-C#\nS-D\nE-Eb\nD-E\nF-F\nT-F#\nG-G\nY-G#\nH-A\nU-Bb\nJ-B");

                switch (key.Key)
                {
                    case (ConsoleKey.F1):
                        octave = 0;
                        break;
                    case (ConsoleKey.F2):
                        octave = 1;
                        break;
                    case (ConsoleKey.F3):
                        octave = 2;
                        break;
                    case (ConsoleKey.F4):
                        octave = 3;
                        break;
                    case (ConsoleKey.F5):
                        octave = 4;
                        break;
                    case (ConsoleKey.F6):
                        octave = 5;
                        break;
                    case (ConsoleKey.F7):
                        octave = 6;
                        break;
                    case (ConsoleKey.A):
                        kb = 0;
                        break;
                    case (ConsoleKey.W):
                        kb = 1;
                        break;
                    case (ConsoleKey.S):
                        kb = 2;
                        break;
                    case (ConsoleKey.E):
                        kb = 3;
                        break;
                    case (ConsoleKey.D):
                        kb = 4;
                        break;
                    case (ConsoleKey.F):
                        kb = 5;
                        break;
                    case (ConsoleKey.T):
                        kb = 6;
                        break;
                    case (ConsoleKey.G):
                        kb = 7;
                        break;
                    case (ConsoleKey.Y):
                        kb = 8;
                        break;
                    case (ConsoleKey.H):
                        kb = 9;
                        break;
                    case (ConsoleKey.U):
                        kb = 10;
                        break;
                    case (ConsoleKey.J):
                        kb = 11;
                        break;
                    default:
                        continue;
                }
                if (kb == -1) continue;
                else freq = octavesArray[octave][kb];
                lastTimePressed = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                if (last_pressed == key.Key && lastTimePressed - ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() <= 500)
                {
                    touchings += 1;
                    closeKeyLong = false;
                    lastTimePressed = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                    continue;
                }
                else
                {
                    if (last_pressed != key.Key && last_pressed != ConsoleKey.Enter || lastTimePressed - ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds() > 500)
                        {
                            touchings = 1;
                            closeKeyLong = true;
                            Console.Beep(37, 1);
                        }
                        touchings = 1;
                    lastTimePressed = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();
                    Thread t = new Thread(delegate () { EndlessPress(freq); });
                    t.Start();
                    lp = new Thread(delegate () { GetKeyLongPressing(); });
                    lp.Start();
                }
                last_pressed = key.Key;
            }
        }
    }
}