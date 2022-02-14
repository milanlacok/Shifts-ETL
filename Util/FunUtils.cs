using System;
using System.Collections.Generic;
using System.Linq;

namespace Shifts_ETL.Util
{
    public static class FunUtils
    {
        public static void Logo()
        {
            Console.WriteLine(@"   ____  _     _  __ _         _____ _____ _     ");
            Console.WriteLine(@"  / ___|| |__ (_)/ _| |_ ___  | ____|_   _| |    ");
            Console.WriteLine(@"  \___ \| '_ \| | |_| __/ __| |  _|   | | | |    ");
            Console.WriteLine(@"   ___) | | | | |  _| |_\__ \ | |___  | | | |___ ");
            Console.WriteLine(@"  |____/|_| |_|_|_|  \__|___/ |_____| |_| |_____|");
            Console.WriteLine(@"                                                 ");
        }

        public static void Credits()
        {
            Console.WriteLine();
            Console.WriteLine(@"  _         _ __  __     _  _          ___ ");
            Console.WriteLine(@" | |__ _  _(_)  \/  |___| \| |___  ___| _ )");
            Console.WriteLine(@" | '_ \ || |_| |\/| / -_) .` / _ \/ _ \ _ \");
            Console.WriteLine(@" |_.__/\_, (_)_|  |_\___|_|\_\___/\___/___/");
            Console.WriteLine(@"       |__/                                ");
            Console.WriteLine();
        }

        public static void Boom()
        {
            Console.WriteLine();
            Console.WriteLine("     ,;,'.`,''.`.':.    ");
            Console.WriteLine("    .'.` ; ;. `'` .``.  ");
            Console.WriteLine("     ; ;`  ` ` ;` ``:   ");
            Console.WriteLine("     ':,`:`.`:~..`.;'   ");
            Console.WriteLine("          :.:.|         ");
            Console.WriteLine("         _:..:|_        ");
            Console.WriteLine("        `-|___:-'       ");
            Console.WriteLine("          : .:|         ");
            Console.WriteLine("    __.=~'=___=`~=.__   ");
            Console.WriteLine("        `~~~~~~~'       ");
            Console.WriteLine();
        }

        public static bool In<T>(this T source, params T[] list)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (list == null)
                throw new ArgumentNullException("list");

            return In(source, list.AsEnumerable());
        }

        public static bool In<T>(this T source, IEnumerable<T> list)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (list == null)
                throw new ArgumentNullException("list");

            return list.Contains(source);
        }
    }
}
