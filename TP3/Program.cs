using System;
using System.Linq;
using System.Threading;

namespace TP3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            #region Exercice 1
            Console.WriteLine("Exercice 1 :");
            var collections = new MovieCollection().Movies;
            
            //Part 1
            var CountAll = collections.Count();
            Console.WriteLine("Il y a "+CountAll+" films en tout");
            
            //Part 2
            var CountAllE = collections.Count(e => e.Title.Contains('e'));
            Console.WriteLine("Il y a "+CountAllE+" films qui contiennent un E dans le titre");
            
            //Part 3
            int CountAllF=0;
            foreach (var movie in collections)
            {
                CountAllF += movie.Title.Count(f => f  == 'f');
            }
            Console.WriteLine("Il y a "+CountAllF+" de f dans le nom des films");
            
            //Part 4
            var HigerBudget = from movie in collections orderby movie.Budget descending select movie.Title;
            Console.WriteLine("Le film ayant eu le plus gros budget est "+HigerBudget.First());
            
            //Part 5
            var LowestBoxOffice = from movie in collections orderby movie.BoxOffice ascending select movie.Title;
            Console.WriteLine("Le film ayant rapporté le moins au BoxOffice est "+LowestBoxOffice.First());
            
            //Part 6
            var MovieByRevAlph = from movie in collections orderby movie.Title descending select movie.Title;
            Console.WriteLine("Les 11 films ayant le nom le plus loins dans l'alphabet sont : ");
            foreach (var movie in MovieByRevAlph.Take(11))
            {
                Console.WriteLine(movie);
            }
            
            //Part 7
            var OldMovies = from movie in collections where (movie.ReleaseDate.Year <= 1980) select movie.Title;
            Console.WriteLine("Il y a "+OldMovies.Count()+" qui sont sortis avant 1980 ");

            //Part 8
            var AvgVowel = (from movie in collections where "aeiou".IndexOf(movie.Title.ToLower()[0]) >= 0
                                select movie.RunningTime).Average();
            Console.WriteLine("La moyenne de durée de films commençant par une voyelle est : "+AvgVowel);
            
            //Part 9
            var MovieName = from movie in collections
                where (movie.Title.ToUpper().Contains('H') || movie.Title.ToUpper().Contains('W')) && !(movie.Title.ToUpper().Contains('I') || movie.Title.ToUpper().Contains('T')) 
                select movie.Title;
            foreach (var movie in MovieName)
            {
                Console.WriteLine(movie);
            }
            
            //Part 10
            var BudgentMoy = (from movie in collections select movie.Budget).Average();
            var BoxOfficeMoy = (from movie in collections select movie.BoxOffice).Average();
            Console.WriteLine("La moyenne de tous les budgets est de "+BudgentMoy+" Et la moyenne de tous les gains au BoxOffice est de "+ BoxOfficeMoy +".");
            
            Console.WriteLine("//////////////////////////////////////////");


            #endregion

            #region Exercice 2
            Console.WriteLine("Exercice 2");
            NewThread();
            #endregion

        }

        #region Fonctions Exo 2
        public static void NewThread()
        {
            Thread thread1 = new Thread(new ThreadStart(T1));
            Thread thread2 = new Thread(new ThreadStart(T2));
            Thread thread3 = new Thread(new ThreadStart(T3));
            
            thread1.Start();
            thread2.Start();
            thread3.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            
        }
        
        public static void T1()
        {
            var startTime = DateTime.UtcNow;
            while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(10))
            {
                Afficher('_');
                Thread.Sleep(50);
            }
        }
        
        public static void T2()
        {
            var startTime = DateTime.UtcNow;
            while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(11))
            {
                Afficher('*');
                Thread.Sleep(40);
            }
        }
        
        public static void T3()
        {
            var startTime = DateTime.UtcNow;
            while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(9))
            {
                Afficher('°');
                Thread.Sleep(20);
            }
        }
        
        private static readonly Mutex mutex = new Mutex();

        public static void Afficher(char c)
        {
            mutex.WaitOne();
            try
            {
                Console.Write(c);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }


        #endregion
       
    }
}